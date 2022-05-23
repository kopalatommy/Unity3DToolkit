using System.Collections.Generic;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class SkipDictTester : TesterBase
    {
        class IntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x < y)
                    return -1;
                else if (x == y)
                    return 0;
                else
                    return 1;
            }
        }

        public SkipDictTester()
        {
            testName = "Skip Dict";
            testType = typeof(SkipDictTester);
        }

        [RunTest(true)]
        private bool InsertTest1()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                list.Insert(i, i + 10);

            int ind = 10;
            foreach (int i in list)
            {
                if (i != ind)
                {
                    Debug.Log("InsertTest1: " + i + " != " + ind);

                    return false;
                }
                ind++;
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        private bool InsertTest2()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                if (i != 5)
                    list.Insert(i, i + 10);

            list.Insert(5, 15);

            int ind = 10;
            foreach (int i in list)
            {
                if (i != ind)
                {
                    Debug.Log("InsertTest2: " + i + " != " + ind);

                    return false;
                }
                ind++;
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        private bool InsertTest3()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                if (i != 5)
                    list.Insert(i, i + 10);

            list.Insert(5, 15);

            SkipList<int> keys = list.Keys;

            int ind = 10;
            foreach (int i in keys)
            {
                if (list.TryGet(i, out int t) && t != (i + 10))
                {
                    Debug.Log("InsertTest3: " + i + " != " + (t + 10));
                    Debug.Log(list);
                    return false;
                }
                ind++;
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        private bool ContainsTest1()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                list.Insert(i, i + 10);

            for (int i = 0; i < 10; i++)
                if (!list.Contains(i))
                {
                    Debug.Log("ContainsTest1: Does not contain: " + i);
                    SkipDictNode<int, int> found = list.ContainsTest(i);
                    if (found == null)
                        Debug.Log("Found null node");
                    else
                    {
                        Debug.Log("Found: " + found);
                        Debug.Log("Match: " + (found.key == i) + " : " + 
                            (found.key.Equals(i)));
                        Debug.Log("Checking: " + i + " : " + (found.key));

                        string successes = "[ ";
                        for (int j = 0; j < 10; j++)
                            successes += j + ":" + (list.Contains(j)) + ", ";
                        successes = successes.Remove(successes.Length - 2, 2);
                        Debug.Log("ContainsTest1: " + successes + " ]");

                        string vals = "[ ";
                        for (int j = 0; j < 10; j++)
                        {
                            found = list.ContainsTest(j);
                            vals += found.ToString() + ", ";
                        }
                        vals = vals.Remove(vals.Length - 2, 2);
                        Debug.Log("ContainsTest1: " + vals + " ]");
                        Debug.Log(list.ToString());

                        SkipDictNode<int, int> test = new SkipDictNode<int, int>(1,2,null,null);
                        Debug.Log(test);
                    }
                    /*Debug.Log(list.DumpList());
                    Debug.Log("Search: " + list.Search(i).Value);
                    Debug.Log("Down: " + (list.Search(i).down != null));
                    Debug.Log("Next: " + list.Search(i).next.Value);*/
                    return false;
                }

            return true;
        }

        [RunTest(true)]
        private bool ContainsTest2()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                list.Insert(i, i + 10);

            return list.Contains(11) == false;
        }

        [RunTest(true)]
        private bool RemoveTest1()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                list.Insert(i, i + 10);

            list.Remove(0);

            /*if (verbose)
                Debug.Log(list.DumpList());*/

            if (!list.Contains(0) && list.Count == 9)
            {
                SkipList<int> keys = list.Keys;
                int ind = 11;
                foreach (int i in keys)
                {
                    if (list.TryGet(i, out int t) && t != (i + 10))
                    {
                        Debug.Log("InsertTest3: " + (i + 10) + " != " + (t));

                        return false;
                    }
                    ind++;
                }
                return true;
            }
            else
                return false;
        }

        [RunTest(true)]
        private bool RemoveTest2()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                list.Insert(i, i + 10);

            for (int i = 0; i < 10; i++)
                if (!list.Remove(i))
                    return false;

            /*if (verbose)
                Debug.Log(list.DumpList());*/

            return list.Count == 0;
        }

        [RunTest(true)]
        private bool RemoveTest4()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                list.Insert(i, i + 10);

            for (int i = 9; i >= 0; i--)
                if (!list.Remove(i))
                    return false;

            /*if (verbose)
                Debug.Log(list.DumpList());*/

            return list.Count == 0;
        }

        [RunTest(true)]
        private bool EnumeratorTest1()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < 10; i++)
                list.Insert(i, i + 10);

            int ind = 10;
            foreach (int i in list)
            {
                if (i != ind)
                {
                    Debug.Log("EnumeratorTest1: " + (i + 10) + " != " + ind);
                    Debug.Log(list);
                    return false;
                }
                ind++;
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        private bool LargeScaleTest1()
        {
            SkipDict<int, int> list = new SkipDict<int, int>(new IntComparer());

            for (int i = 0; i < short.MaxValue; i++)
                list.Insert(i, i);

            int ind = 0;
            foreach (int i in list)
            {
                if (i != ind)
                {
                    Debug.Log("EnumeratorTest1: " + i + " != " + ind);

                    return false;
                }
                ind++;
            }

            return list.Count == short.MaxValue;
        }
    }
}
