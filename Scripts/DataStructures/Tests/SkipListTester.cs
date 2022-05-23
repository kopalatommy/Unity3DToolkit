using System;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class SkipListTester : TesterBase
    {
        public SkipListTester()
        {
            testName = "Skip List";
            testType = typeof(SkipListTester);
        }

        [RunTest(true)]
        private bool InsertTest1()
        {
            SkipList<int> list = new SkipList<int>();

            for(int i = 0; i < 10; i++)
                list.Insert(i);

            int ind = 0;
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
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < 10; i++)
                if(i != 5)
                    list.Insert(i);

            list.Insert(5);

            int ind = 0;
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
        private bool ContainsTest1()
        {
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < 10; i++)
                list.Insert(i);

            for (int i = 0; i < 10; i++)
                if (!list.Contains(i))
                {
                    Debug.Log("ContainsTest1: Does not contain: " + i);
                    Debug.Log(list.DumpList());
                    Debug.Log("Search: " + list.Search(i).Value);
                    Debug.Log("Down: " + (list.Search(i).down != null));
                    Debug.Log("Next: " + list.Search(i).next.Value);
                    return false;
                }

            return true;
        }

        [RunTest(true)]
        private bool ContainsTest2()
        {
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < 10; i++)
                list.Insert(i);

            return list.Contains(11) == false;
        }

        [RunTest(true)]
        private bool RemoveTest1()
        {
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < 10; i++)
                list.Insert(i);

            list.Remove(0);

            if (verbose)
                Debug.Log(list.DumpList());

            return !list.Contains(0) && list.Count == 9;
        }

        [RunTest(true)]
        private bool RemoveTest2()
        {
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < 10; i++)
                list.Insert(i);

            list.Remove(9);

            if (verbose)
                Debug.Log(list.DumpList());

            return !list.Contains(9) && list.Count == 9;
        }

        [RunTest(true)]
        private bool RemoveTest3()
        {
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < 10; i++)
                list.Insert(i);

            for (int i = 0; i < 10; i++)
                if(!list.Remove(i))
                    return false;

            if(verbose)
                Debug.Log(list.DumpList());

            return list.Count == 0;
        }

        [RunTest(true)]
        private bool RemoveTest4()
        {
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < 10; i++)
                list.Insert(i);

            for (int i = 9; i >= 0; i--)
                if (!list.Remove(i))
                    return false;

            if (verbose)
                Debug.Log(list.DumpList());

            return list.Count == 0;
        }

        [RunTest(true)]
        private bool EnumeratorTest1()
        {
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < 10; i++)
                list.Insert(i);

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

            return list.Count == 10;
        }

        [RunTest(true)]
        private bool LargeScaleTest1()
        {
            SkipList<int> list = new SkipList<int>();

            for (int i = 0; i < short.MaxValue; i++)
                list.Insert(i);

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
