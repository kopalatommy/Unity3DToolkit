using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class MinHeapTester : TesterBase
    {
        public MinHeapTester()
        {
            testName = "Min Heap";
            testType = typeof(MinHeapTester);
        }

        [RunTest(true)]
        public bool InsertTest1()
        {
            MinHeap<int> minHeap = new MinHeap<int>(10);

            if (verbose)
                Debug.Log("Starting min heap InsertTest1");

            for (int i = 0; i < 10; i++)
                if (!minHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }

            for (int i = 0; i < 10; i++)
                if (!minHeap.Contains(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(minHeap);
                    return false;
                }

            if (minHeap.Count != 10)
            {
                Debug.Log("Max heap count != expected: " + minHeap.Count + " != 10");
                return false;
            }
            return true;
        }

        [RunTest(true)]
        public bool InsertTest2()
        {
            MinHeap<int> minHeap = new MinHeap<int>(10);

            if (verbose)
                Debug.Log("Starting min heap InsertTest1");

            for (int i = 9; i >= 0; i--)
                if (!minHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }

            for (int i = 0; i < 10; i++)
                if (!minHeap.Contains(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(minHeap);
                    return false;
                }

            if (minHeap.Count != 10)
            {
                Debug.Log("Max heap count != expected: " + minHeap.Count + " != 10");
                return false;
            }
            return true;
        }

        [RunTest(true)]
        public bool RemoveTest1()
        {
            MinHeap<int> minHeap = new MinHeap<int>(10);

            if (verbose)
                Debug.Log("Starting min heap InsertTest1");

            for (int i = 0; i < 10; i++)
                if (!minHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }

            for (int i = 0; i < 10; i++)
                if (!minHeap.Contains(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(minHeap);
                    return false;
                }

            for (int i = 0; i < 10; i++)
            {
                //Debug.Log(minHeap);
                if (!minHeap.Remove(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(minHeap);
                    //Debug.Log(minHeap);
                    return false;
                }
            }

            if (minHeap.Count != 0)
            {
                Debug.Log("Max heap count != expected: " + minHeap.Count + " != 10");
                return false;
            }
            return true;
        }

        [RunTest(true)]
        public bool RemoveTest2()
        {
            MinHeap<int> minHeap = new MinHeap<int>(10);

            if (verbose)
                Debug.Log("Starting min heap InsertTest1");

            for (int i = 0; i < 10; i++)
                if (!minHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }
            //Debug.Log(minHeap);

            for (int i = 0; i < 10; i++)
            {
                if (!minHeap.Remove(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(minHeap);
                    return false;
                }
                if (minHeap.Count != 9)
                {
                    Debug.Log("Heap count != 9: " + minHeap.Count);
                    return false;
                }
                if (!minHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }
                if (minHeap.Count != 10)
                {
                    Debug.Log("Heap count != 9: " + minHeap.Count);
                    return false;
                }
                if (!minHeap.Contains(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(minHeap);
                    return false;
                }
            }

            if (minHeap.Count != 10)
            {
                Debug.Log("Max heap count != expected: " + minHeap.Count + " != 10");
                return false;
            }
            return true;
        }
    }
}
