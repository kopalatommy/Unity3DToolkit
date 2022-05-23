using System.Collections;
using System.Collections.Generic;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class MaxHeapTester : TesterBase
    {
        public MaxHeapTester()
        {
            testName = "Max Heap";
            testType = typeof(MaxHeapTester);
        }

        [RunTest(false)]
        public bool Test1()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>(10);

            for(int i = 0; i < 10; i++)
            {
                maxHeap.Insert(i);
                //Debug.Log(i + "\n" + maxHeap.Count + "\n" + maxHeap.printHeap() + "\n" + maxHeap);
                Debug.Log("Added: " + i + "\nCount: " + maxHeap.Count + "\n" + maxHeap);
            }

            return false;
        }


        [RunTest(true)]
        public bool InsertTest1()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>(10);

            if (verbose)
                Debug.Log("Starting max heap InsertTest1");

            for(int i = 0; i < 10; i++)
                if(!maxHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }

            for(int i = 0; i < 10; i++)
                if (!maxHeap.Contains(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(maxHeap);
                    return false;
                }

            if(maxHeap.Count != 10)
            {
                Debug.Log("Max heap count != expected: " + maxHeap.Count + " != 10");
                return false;
            }
            return true;
        }

        [RunTest(true)]
        public bool InsertTest2()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>(10);

            if (verbose)
                Debug.Log("Starting max heap InsertTest1");

            for (int i = 9; i >= 0; i--)
                if (!maxHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }

            for (int i = 0; i < 10; i++)
                if (!maxHeap.Contains(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(maxHeap);
                    return false;
                }

            if (maxHeap.Count != 10)
            {
                Debug.Log("Max heap count != expected: " + maxHeap.Count + " != 10");
                return false;
            }
            return true;
        }

        [RunTest(true)]
        public bool RemoveTest1()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>(10);

            if (verbose)
                Debug.Log("Starting max heap InsertTest1");

            for (int i = 0; i < 10; i++)
                if (!maxHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }

            for (int i = 0; i < 10; i++)
                if (!maxHeap.Contains(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(maxHeap);
                    return false;
                }

            for (int i = 0; i < 10; i++)
            {
                //Debug.Log(maxHeap);
                if (!maxHeap.Remove(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(maxHeap);
                    //Debug.Log(maxHeap);
                    return false;
                }
            }

            if (maxHeap.Count != 0)
            {
                Debug.Log("Max heap count != expected: " + maxHeap.Count + " != 10");
                return false;
            }
            return true;
        }

        [RunTest(true)]
        public bool RemoveTest2()
        {
            MaxHeap<int> maxHeap = new MaxHeap<int>(10);

            if (verbose)
                Debug.Log("Starting max heap InsertTest1");

            for (int i = 0; i < 10; i++)
                if (!maxHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }
            //Debug.Log(maxHeap);

            for (int i = 0; i < 10; i++)
            {
                if (!maxHeap.Remove(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(maxHeap);
                    return false;
                }
                if(maxHeap.Count != 9)
                {
                    Debug.Log("Heap count != 9: " + maxHeap.Count);
                    return false;
                }
                if (!maxHeap.Insert(i))
                {
                    Debug.Log("Max heap failed to insert: " + i);
                    return false;
                }
                if (maxHeap.Count != 10)
                {
                    Debug.Log("Heap count != 9: " + maxHeap.Count);
                    return false;
                }
                if (!maxHeap.Contains(i))
                {
                    Debug.Log("Max heap failed to find: " + i);
                    Debug.Log(maxHeap);
                    return false;
                }
            }
            
            if (maxHeap.Count != 10)
            {
                Debug.Log("Max heap count != expected: " + maxHeap.Count + " != 10");
                return false;
            }
            return true;
        }
    }
}
