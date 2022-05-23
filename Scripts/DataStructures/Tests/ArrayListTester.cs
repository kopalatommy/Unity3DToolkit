using System;
using Toolkit.DataStructures;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class ArrayListTester : TesterBase
    {
        public ArrayListTester()
        {
            testName = "Array List";
            testType = typeof(ArrayListTester);
        }

        // This tests wether the list can access all added items and returns
        // the proper items
        [RunTest(true)]
        private bool GetTest1()
        {
            ArrayList<int> list = new ArrayList<int>();
            bool succeeded = true;

            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }

            if (list.Count != 10)
            {
                Debug.Log("GetTest1 failed on test 1. Count: " + list.Count + " != 10");
                return false;
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (list.Get(i) != i)
                    {
                        succeeded = false;
                        Debug.Log("GetTest1 failed on test 2: " + i + " != " + list[i]);
                        Debug.Log(list.ToString());
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    succeeded = false;
                    Debug.Log("GetTest1 failed on step 3. Threw IndexOutOfRangeException");
                    Debug.Log("Failed on " + i);
                    Debug.Log(ex.Message);
                }
            }
            return succeeded;
        }

        // This tests the error cases. This makes sure an error is returned if
        // trying to access an invalid index
        [RunTest(true)]
        private bool GetTest2()
        {
            ArrayList<int> list = new ArrayList<int>();

            try
            {
                list.Get(0);
                Debug.Log("ArrayList get test 2 failed at 1. Accessed item in empty list");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            for (int i = 0; i < 10; i++)
                list.Add(i);

            try
            {
                list.Get(100);
            }
            catch (IndexOutOfRangeException)
            {

            }

            try
            {
                list.Get(-1);
                Debug.Log("ArrayList get test 2 failed at 3. Returned item with negatve index");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            return true;
        }

        [RunTest(true)]
        private bool SetTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            try
            {
                list.Set(5, -1);
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log("SetTest1 failed at test 1. Threw IndexOutOfRangeException");
            }

            if (list.Get(5) == -1)
                return true;
            else
                return false;
        }

        [RunTest(true)]
        private bool SetTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            try
            {
                list.Set(11, -1);
                Debug.Log("SetTest2 failed to throw IndexOutOfRangeException");
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool SetTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for(int i = 0; i < 10; i++)
            {
                list.Add(0);
                list.Set(i, i);
            }

            for(int i = 0; i < 10; i++)
                if(list[i] != i)
                {
                    Debug.Log("SetTest3 failed: " + i + " != " + list[i]);
                    return false;
                }

            if (list.Count != 10)
            {
                Debug.Log("SetTest3 failed: Count != 10: " + list.Count);
                return false;
            }

            return true;
        }

        [RunTest(true)]
        private bool AppendListTest1()
        {
            ArrayList<int> listA = new ArrayList<int>(10);
            ArrayList<int> listB = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                listA.Add(i);

            for (int i = 10; i < 20; i++)
                listB.Add(i);

            ArrayList<int> listC = listA + listB;

            for (int i = 0; i < 20; i++)
            {
                if (listC.Get(i) != i)
                {
                    Debug.Log(listC.ToString());
                    Debug.Log(listC.Get(i) + " != " + i);
                    return false;
                }
            }

            return (listC.Count == 20);
        }

        [RunTest(true)]
        private bool AppendListTest2()
        {
            ArrayList<int> listA = new ArrayList<int>(10);
            ArrayList<int> listB = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                listA.Add(i);

            for (int i = 10; i < 20; i++)
                listB.Add(i);

            listA += listB;

            for (int i = 0; i < 20; i++)
            {
                if (listA.Get(i) != i)
                {
                    Debug.Log(listA);
                    Debug.Log(listA.Get(i) + " != " + i);
                    return false;
                }
            }

            return listA.Count == 20;
        }

        [RunTest(true)]
        private bool InsertTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                if (i != 4)
                    list.Add(i);

            list.Insert(4, 4);

            for (int i = 0; i < 10; i++)
                if (list.Get(i) != i)
                {
                    Debug.Log("InsertTest1: " + list.Get(i) + " != " + i);
                    Debug.Log(list.ToString());
                    return false;
                }

            return list.Count == 10;
        }

        [RunTest(true)]
        private bool InsertTest2()
        {
            ArrayList<int> list = new ArrayList<int>(100);

            for (int i = 0; i < 10; i++)
                if (i != 7)
                    list.Add(i);

            list.Insert(7, 7);

            for (int i = 0; i < 10; i++)
                if (list.Get(i) != i)
                    if (list.Get(i) != i)
                    {
                        Debug.Log("InsertTest2: " + list.Get(i) + " != " + i);
                        Debug.Log(list.ToString());
                        return false;
                    }

            return list.Count == 10;
        }

        [RunTest(true)]
        private bool InsertTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            try
            {
                list.Insert(5, 5);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool InsertTest4()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            try
            {
                list.Insert(-1, 5);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool InsertTest5()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            list.Add(0);
            list.Add(2);
            list.Add(4);
            list.Add(6);
            list.Add(8);

            list.Insert(1, 1);
            list.Insert(3, 3);
            list.Insert(5, 5);
            list.Insert(7, 7);
            //list.Insert(9, 9);

            for (int i = 0; i < 9; i++)
                if (list.Get(i) != i)
                    if (list.Get(i) != i)
                    {
                        Debug.Log("InsertTest2: " + list.Get(i) + " != " + i);
                        Debug.Log(list.ToString());
                        return false;
                    }

            return list.Count == 9;
        }

        [RunTest(true)]
        public bool EnumTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);
            for (int i = 0; i < 10; i++)
                list.Add(i);

            int count = 0;
            foreach (int i in list)
            {
                if (i != count)
                {
                    Debug.Log(i + " != " + count);
                    return false;
                }
                count++;
            }
            return true;
        }

        [RunTest(true)]
        private bool TakeFirstTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            int count = 0;
            int len = 10;
            while (list.Count > 0 && len > 0)
            {
                len--;
                if (list.TakeFirst() != count)
                    return false;
                count++;
            }

            return list.Count == 0;
        }

        [RunTest(true)]
        private bool TakeFirstTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            try
            {
                list.TakeFirst();
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool TakeFirstTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            list.TakeFirst();

            for (int i = 1; i < 10; i++)
                if (list.Get(i - 1) != i)
                    return false;

            return list.Count == 9;
        }

        [RunTest(true)]
        private bool TakeAtTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            return list.TakeAt(5) == 5;
        }

        [RunTest(true)]
        private bool TakeAtTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            int it = list.TakeAt(5);
            if (it != 5)
            {
                Debug.Log("Removed wrong item: " + it);
                return false;
            }

            for (int i = 1; i < 9; i++)
            {
                if (i < 5)
                {
                    if (list.Get(i) != i)
                    {
                        Debug.Log("Failed on " + i + " = " + list[i]);
                        return false;
                    }
                }
                else
                {
                    if (list.Get(i) != (i + 1))
                    {
                        Debug.Log("Failed on " + i + " = " + list[i]);
                        Debug.Log(list.ToString());
                        return false;
                    }
                }
            }
            return list.Count == 9;
        }

        [RunTest(true)]
        private bool TakeAtTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            try
            {
                list.TakeAt(100);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool TakeAtTest4()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            try
            {
                list.TakeAt(-1);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool TakeAtTest5()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            for (int i = 8; i > 1; i--)
            {
                if (list.TakeAt(i) != i)
                    return false;
            }

            return list.Count == 3;
        }

        [RunTest(true)]
        private bool ContainsTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            return list.Contains(1);
        }

        [RunTest(true)]
        private bool ContainsTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            return list.Contains(11) == false;
        }

        [RunTest(true)]
        private bool ContainsTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            return list.Contains(11) == false;
        }

        [RunTest(true)]
        private bool RemoveFirstItemTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            if (list.RemoveFirst(5))
            {
                if (list.Contains(5) || list.Count != 9)
                {
                    Debug.Log(list.ToString());

                    return false;
                }
                else
                    return true;
            }
            else
            {
                Debug.Log("Failed to remove item");
                return false;
            }
        }

        [RunTest(true)]
        private bool RemoveFirstItemTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            if (list.RemoveFirst(11) == false)
            {
                return list.Count == 10;
            }
            else
            {
                return false;
            }
        }

        [RunTest(true)]
        private bool RemoveFirstItemTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    list.Add(i);
                else
                    list.Add(5);

            if (list.RemoveFirst(5))
            {
                if (list.FirstIndexOf(5) == 1 || list.Count != 9)
                {
                    Debug.Log(list.ToString());

                    return false;
                }
                else
                    return true;
            }
            else
            {
                Debug.Log("Failed to remove item");
                return false;
            }
        }

        [RunTest(true)]
        private bool RemoveLastItemTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            if (list.RemoveLast(5))
            {
                if (list.Contains(5) || list.Count != 9)
                {
                    Debug.Log(list.ToString());

                    return false;
                }
                else
                    return true;
            }
            else
            {
                Debug.Log("Failed to remove item");
                return false;
            }
        }

        [RunTest(true)]
        private bool RemoveLastItemTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            if (list.RemoveLast(11) == false)
            {
                return list.Count == 10;
            }
            else
            {
                return false;
            }
        }

        [RunTest(true)]
        private bool RemoveLastItemTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    list.Add(i);
                else
                    list.Add(5);

            if (list.RemoveLast(5))
            {
                if (list.LastIndexOf(5) == 9 || list.Count != 9)
                {
                    Debug.Log(list.ToString());

                    return false;
                }
                else
                    return true;
            }
            else
            {
                Debug.Log("Failed to remove item");
                return false;
            }
        }

        [RunTest(true)]
        private bool RemoveAtTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            list.RemoveAt(3);

            return list.Count == 9 && list.Contains(3) == false;
        }

        [RunTest(true)]
        private bool RemoveAtTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            try
            {
                list.RemoveAt(3);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool RemoveAtTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            //Debug.Log(list.ToString());

            /*Debug.Log("Head: " + list.Head);
            Debug.Log("Tail: " + list.Tail);*/

            list.RemoveAt(3);

            //Debug.Log(list.ToString());

            /*Debug.Log("Head: " + list.Head);
            Debug.Log("Tail: " + list.Tail);*/

            for (int i = 0; i < 3; i++)
                if (list.Get(i) != i)
                {
                    Debug.Log(list.ToString());
                    Debug.Log("RemoveAtTest3 failed at check 1. " + i + " != " + list.Get(i));
                    return false;
                }

            for (int i = 4; i < 10; i++)
            {
                if (list.Get(i - 1) != i)
                {
                    Debug.Log("RemoveAtTest3 failed at check 2. " + i + " != " + list.Get(i - 1));
                    return false;
                }
            }

            return list.Count == 9;
        }

        [RunTest(true)]
        private bool RemoveAtTest4()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            /*Debug.Log(list.ToString());
            Debug.Log("Head: " + list.Head);
            Debug.Log("Tail: " + list.Tail);*/

            list.RemoveAt(7);

            /*Debug.Log(list.ToString());
            Debug.Log("Head: " + list.Head);
            Debug.Log("Tail: " + list.Tail);*/

            for (int i = 0; i < 7; i++)
                if (list.Get(i) != i)
                    return false;

            for (int i = 8; i < 10; i++)
            {
                if (list.Get(i - 1) != i)
                    return false;
            }

            return list.Count == 9;
        }

        [RunTest(true)]
        private bool RemoveAtTest5()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 16; i++)
                list.Add(i);

            /*Debug.Log("Head: " + list.Head);
            Debug.Log("Tail: " + list.Tail);
            Debug.Log("Count: " + list.Count);
            Debug.Log(list.ToString());*/

            list.RemoveAt(7);

            /*Debug.Log(list.ToString());
            Debug.Log("Head: " + list.Head);
            Debug.Log("Tail: " + list.Tail);*/

            for(int i = 0; i < 7; i++)
                if(list[i] != i)
                {
                    Debug.Log("RemoveAtTest5: " + i + " != " + list.Get(i));
                    return false;
                }

            for (int i = 8; i < 16; i++)
                if (list[i - 1] != i)
                {
                    Debug.Log("RemoveAtTest5: " + i + " != " + list.Get(i));
                    return false;
                }

            return list.Count == 15;
        }

        [RunTest(true)]
        private bool RemoveRangeTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            list.RemoveRange(3, 4);

            for (int i = 0; i < 3; i++)
                if (list.Get(i) != i)
                {
                    Debug.Log(list[i] + " != " + i);
                    Debug.Log(list.ToString());
                    return false;
                }

            for (int i = 7; i < 10; i++)
                if (list.Get(i - 4) != i)
                {
                    Debug.Log((i - 4) + " : " + list[i - 4] + " != " + i);
                    Debug.Log(list.ToString());
                    return false;
                }

            return list.Count == 6;
        }

        [RunTest(true)]
        private bool RemoveRangeTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            try
            {
                list.RemoveRange(3, 10);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool RemoveRangeTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            try
            {
                list.RemoveRange(-1, 10);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool RemoveAllTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(5);

            list.RemoveAll(5);

            if (list.Count == 0)
                return true;
            else
            {
                Debug.Log(list.ToString());

                return false;
            }
        }

        [RunTest(true)]
        private bool RemoveAllTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 12; i++)
                if (i % 2 == 0)
                    list.Add(5);
                else
                    list.Add(0);

            //Debug.Log("RemoveAllTest2: " + list.ToString());

            list.RemoveAll(5);

            //Debug.Log("RemoveAllTest2: " + list.ToString());

            if (list.Count != 6)
            {
                Debug.Log(list.ToString());

                return false;
            }
            else
                return true;
        }

        [RunTest(true)]
        private bool RemoveAllTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    list.Add(5);
                else
                    list.Add(0);

            list.RemoveAll(7);

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool RemoveFirstTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            for (int i = 0; i < 9; i++)
            {
                list.RemoveFirst();
                if (list[0] != (i + 1))
                {
                    Debug.Log(list[0] + " != " + (i + 1));
                    return false;
                }
            }

            return list.Count == 1;
        }

        [RunTest(true)]
        public bool RemoveFirstTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            for (int i = 0; i < 10; i++)
                list.RemoveFirst();

            try
            {
                list.RemoveFirst();
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        public bool RemoveLastTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            for (int i = 0; i < 9; i++)
            {
                list.RemoveLast();
                if (list[0] != 0)
                {
                    Debug.Log(list[0] + " != 0");
                    return false;
                }
            }

            return list.Count == 1;
        }

        [RunTest(true)]
        public bool RemoveLastTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            for (int i = 0; i < 10; i++)
                list.RemoveLast();

            try
            {
                list.RemoveLast();
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool FirstIndexOfTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            return list.FirstIndexOf(5) == 5;
        }

        [RunTest(true)]
        private bool FirstIndexOfTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            return list.FirstIndexOf(-1) == -1;
        }

        [RunTest(true)]
        private bool FirstIndexOfTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    list.Add(i);
                else
                    list.Add(5);

            return list.FirstIndexOf(5) == 1;
        }

        [RunTest(true)]
        private bool LastIndexOfTest1()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            return list.LastIndexOf(5) == 5;
        }

        [RunTest(true)]
        private bool LastIndexOfTest2()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Add(i);

            return list.LastIndexOf(-1) == -1;
        }

        [RunTest(true)]
        private bool LastIndexOfTest3()
        {
            ArrayList<int> list = new ArrayList<int>(10);

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    list.Add(i);
                else
                    list.Add(5);

            return list.LastIndexOf(5) == 9;
        }

        /// <summary>
        /// This tests wether the list can handle a large number of items
        /// without failing.
        /// </summary>
        /// <returns>True if no error</returns>
        [RunTest(true)]
        private bool LargeScaleTest1()
        {
            ArrayList<int> lst = new ArrayList<int>(10);

            for (int i = 0; i < short.MaxValue; i++)
                lst.Add(i);

            lst.RemoveAt(lst.Count - 1);

            return lst.Count == (short.MaxValue - 1);
        }

        [RunTest(true)]
        private bool LargeScaleTest2()
        {
            ArrayList<int> lst = new ArrayList<int>(short.MaxValue);

            for (int i = 0; i < short.MaxValue; i++)
                lst.Add(i);

            lst.RemoveAt(lst.Count - 1);

            return lst.Count == (short.MaxValue - 1);
        }
    }
}
