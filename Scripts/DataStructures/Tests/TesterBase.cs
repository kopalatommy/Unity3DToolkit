using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace Toolkit.Test
{
    public class TesterBase
    {
        protected Dictionary<int, Thread> activeThreads = new Dictionary<int, Thread>();
        protected Dictionary<int, Timer> activeTimers = new Dictionary<int, Timer>();
        protected Dictionary<int, string> activeNames = new Dictionary<int, string>();

        protected int numberOfTests = 0;
        protected int successes = 0;

        protected string testName = "Base Test";

        protected bool verbose = false;

        protected Type testType = null;

        public TesterBase()
        {
            testType = typeof(TesterBase);
        }

        ~TesterBase()
        {
            foreach (int threadID in activeThreads.Keys)
            {
                activeThreads[threadID].Abort();
                activeTimers[threadID].Dispose();
            }
            activeTimers.Clear();
            activeThreads.Clear();
            activeNames.Clear();
        }

        protected void TimerCallBack(object threadIdArg)
        {
            int threadId = (int)threadIdArg;
            if (activeThreads.ContainsKey(threadId))
            {
                Debug.Log(testName + ": " + activeNames[threadId] + " failed to finish on time");
                activeThreads[threadId].Abort();
                KillTimer(threadId, false);
            }
            else
            {
                CheckIfFinished();
            }
        }

        /// <summary>
        /// Called by a test when finishing
        /// </summary>
        /// <param name="threadIdArg"></param>
        protected void KillTimer(int threadIdArg, bool success)
        {
            lock(activeThreads)
            {
                activeThreads.Remove(threadIdArg);
                activeTimers[threadIdArg].Dispose();
                activeTimers.Remove(threadIdArg);

                if (success)
                {
                    successes++;
                    if (verbose)
                    {
                        Debug.Log(activeNames[threadIdArg] + " succeeded");
                    }
                }
                else
                {
                    Debug.Log(activeNames[threadIdArg] + " failed for " + testName);
                }

                CheckIfFinished();
            }
        }

        protected void CheckIfFinished()
        {
            if (activeTimers.Count == 0)
            {
                Debug.Log(testName + " passed " + successes + " / " + numberOfTests + " tests");
            }
            else if(verbose)
            {
                Debug.Log("Remaining tests: " + activeTimers.Count);
            }
        }

        public virtual void RunTests(bool verbose, int timeoutTime = 2500)
        {
            this.verbose = verbose;

            if(verbose)
            {
                Debug.Log("Running tests for " + testName);
            }

            // Clear any previous test results
            foreach (int threadID in activeThreads.Keys)
            {
                activeThreads[threadID].Abort();
                activeTimers[threadID].Dispose();
            }
            activeTimers.Clear();
            activeThreads.Clear();
            activeNames.Clear();

            // Search through all methods of the test type and get any with the run test attribute and runTest = true
            foreach (MethodInfo method in testType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
            {
                // Check if the method has the RunTestAttribute
                foreach (Attribute a in method.GetCustomAttributes())
                {
                    if (a is RunTestAttribute)
                    {
                        // Check if the test should be run
                        if (((RunTestAttribute)a).RunTest == true)
                        {
                            if(verbose)
                            {
                                Debug.Log("Running test: " + method.Name);
                            }

                            numberOfTests++;

                            TestWorker worker = new TestWorker(method, this, ((RunTestAttribute)a).TestArgs);
                            worker.FinishedCallBack = new KillTimerDelegate(KillTimer);
                            Thread thread = new Thread(worker.DoWork);
                            activeThreads.Add(thread.ManagedThreadId, thread);
                            thread.IsBackground = true;
                            Timer timer = new Timer(TimerCallBack, thread.ManagedThreadId, timeoutTime, timeoutTime);
                            activeTimers.Add(thread.ManagedThreadId, timer);
                            activeNames.Add(thread.ManagedThreadId, method.Name);
                        }
                        else if (verbose)
                        {
                            Debug.Log("Skipping test: " + method.Name);
                        }

                        break;
                    }
                }
            }
            if(verbose)
            {
                Debug.Log("Finished building tests: " + activeThreads.Count);
            }

            // Get a list of all keys before starting the tests because some of the
            // tests may exit quickly and be removed from the dictionary before all
            // are started
            var keys = activeThreads.Keys;
            List<int> ks = new List<int>();
            foreach (int t in keys)
            {
                ks.Add(t);
            }
            foreach (int t in ks)
            {
                activeThreads[t].Start();
            }
            if(verbose)
            {
                Debug.Log("Finished starting all tests");
            }

            //Timer end = new Timer(TimerCallBack, 0, 5000, 5000);
        }
    }
}
