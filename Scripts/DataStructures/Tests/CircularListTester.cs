using System;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class CircularListTester : TesterBase
    {
        public CircularListTester()
        {
            testName = "Circular List";
            testType = typeof(CircularListTester);
        }

        #region GetTests

        // This tests wether the list can access all added items and returns
        // the proper items
        [RunTest(true)]
        private bool GetTest1()
        {
            CircularList<int> list = new CircularList<int>(10);
            bool succeeded = true;

            for (int i = 0; i < 10; i++)
            {
                list.Append(i);
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
            CircularList<int> list = new CircularList<int>(10);

            try
            {
                list.Get(0);
                Debug.Log("CircularList get test 2 failed at 1. Accessed item in empty list");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
                Debug.Log("CircularList get test 2 failed at 3. Returned item with negatve index");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            return true;
        }

        // This tests wether the list can access all added items and returns
        // the proper items
        [RunTest(true)]
        private bool GetTest3()
        {
            CircularList<int> list = new CircularList<int>();
            bool succeeded = true;
            list.ResizeBuffer(10);

            for (int i = 0; i < 10; i++)
            {
                list.Append(i);
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (list.Get(i) != i)
                    {
                        succeeded = false;
                        Debug.Log("Failed on " + i + " = " + list[i]);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    succeeded = false;
                    Debug.Log("Failed on " + i);
                    Debug.Log("CircularList get test 1 encountered an index out of bounds");
                }
            }
            return succeeded;
        }

        // This tests the error cases. This makes sure an error is returned if
        // trying to access an invalid index
        [RunTest(true)]
        private bool GetTest4()
        {
            CircularList<int> list = new CircularList<int>();
            list.ResizeBuffer(10);

            try
            {
                list.Get(0);
                Debug.Log("CircularList get test 2 failed at 1. Accessed item in empty list");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
                Debug.Log("CircularList get test 2 failed at 3. Returned item with negatve index");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            return true;
        }

        #endregion

        #region ResizeTests

        // This tests wether the list can access all added items and returns
        // the proper items
        [RunTest(true)]
        private bool ResizeTest1()
        {
            CircularList<int> list = new CircularList<int>();
            bool succeeded = true;

            list.ResizeBuffer(100);

            for (int i = 0; i < 10; i++)
            {
                list.Append(i);
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    if (list.Get(i) != i)
                    {
                        succeeded = false;
                        Debug.Log("Failed on " + i + " = " + list[i]);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    succeeded = false;
                    Debug.Log("Failed on " + i);
                    Debug.Log("CircularList get test 1 encountered an index out of bounds");
                }
            }
            return succeeded;
        }

        // This tests the error cases. This makes sure an error is returned if
        // trying to access an invalid index
        [RunTest(true)]
        private bool ResizeTest2()
        {
            CircularList<int> list = new CircularList<int>();

            try
            {
                list.Get(0);
                Debug.Log("CircularList get test 2 failed at 1. Accessed item in empty list");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            list.ResizeBuffer(100);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
                Debug.Log("CircularList get test 2 failed at 3. Returned item with negatve index");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            return true;
        }

        #endregion // Resize Tests

        #region SetTests

        [RunTest(true)]
        private bool SetTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            try
            {
                list.Set(5, -1);
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log("SetTest1 failed at test 1. Threw IndexOutOfRangeException");
                Debug.Log("");
            }

            if (list.Get(5) == -1)
                return true;
            else
                return false;
        }

        [RunTest(true)]
        private bool SetTest2()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            try
            {
                list.Set(11, -1);
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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(0);

            for (int i = 0; i < 10; i++)
                list.Set(i, i);

            for (int i = 0; i < 10; i++)
                if (list[i] != i)
                    return false;

            return true;
        }

        #endregion

        #region AddTests

        [RunTest(true)]
        private bool AppendListTest1()
        {
            CircularList<int> listA = new CircularList<int>(10);
            CircularList<int> listB = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                listA.Append(i);

            for (int i = 10; i < 20; i++)
                listB.Append(i);

            CircularList<int> listC = listA + listB;

            for (int i = 10; i < 20; i++)
            {
                if (listC.Get(i - 10) != i)
                {
                    Debug.Log(listC.ToString());
                    Debug.Log(listC.Get(i - 10) + " != " + i);
                    return false;
                }
            }

            return (listC.Count == 10);
        }

        [RunTest(true)]
        private bool AppendListTest2()
        {
            CircularList<int> listA = new CircularList<int>(10);
            CircularList<int> listB = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                listA.Append(i);

            for (int i = 10; i < 20; i++)
                listB.Append(i);

            listA += listB;

            for (int i = 10; i < 20; i++)
            {
                if (listA.Get(i - 10) != i)
                {
                    Debug.Log(listA);
                    Debug.Log(listA.Get(i - 10) + " != " + i);
                    return false;
                }
            }

            return listA.Count == 10;
        }

        #endregion

        #region InsertTests

        [RunTest(true)]
        private bool InsertTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                if (i != 4)
                    list.Append(i);

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
            CircularList<int> list = new CircularList<int>(100);

            for (int i = 0; i < 10; i++)
                if (i != 7)
                    list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

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

        #endregion

        #region EnumTests

        [RunTest(true)]
        public bool EnumTest1()
        {
            CircularList<int> list = new CircularList<int>(10);
            for (int i = 0; i < 10; i++)
                list.Append(i);

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

        #endregion

        #region TakeFirstTests

        [RunTest(true)]
        private bool TakeFirstTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            list.TakeFirst();

            for (int i = 1; i < 10; i++)
                if (list.Get(i - 1) != i)
                    return false;

            return list.Count == 9;
        }

        #endregion

        #region TakeAtTests

        [RunTest(true)]
        private bool TakeAtTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            return list.TakeAt(5) == 5;
        }

        [RunTest(true)]
        private bool TakeAtTest2()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            for (int i = 8; i > 1; i--)
            {
                if (list.TakeAt(i) != i)
                    return false;
            }

            return list.Count == 3;
        }

        #endregion

        #region ContainsTests

        [RunTest(true)]
        private bool ContainsTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            return list.Contains(1);
        }

        [RunTest(true)]
        private bool ContainsTest2()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            return list.Contains(11) == false;
        }

        [RunTest(true)]
        private bool ContainsTest3()
        {
            CircularList<int> list = new CircularList<int>(10);

            return list.Contains(11) == false;
        }

        #endregion

        #region RemoveTests

        [RunTest(true)]
        private bool RemoveTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            if (list.Remove(5))
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
        private bool RemoveTest2()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            if (list.Remove(11) == false)
            {
                return list.Count == 10;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region RemoveAtTests

        [RunTest(true)]
        private bool RemoveAtTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            list.RemoveAt(3);

            return list.Count == 9 && list.Contains(3) == false;
        }

        [RunTest(true)]
        private bool RemoveAtTest2()
        {
            CircularList<int> list = new CircularList<int>(10);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 16; i++)
                list.Append(i);

            /*Debug.Log("Head: " + list.Head);
            Debug.Log("Tail: " + list.Tail);
            Debug.Log("Count: " + list.Count);
            Debug.Log(list.ToString());*/

            list.RemoveAt(7);

            /*Debug.Log(list.ToString());
            Debug.Log("Head: " + list.Head);
            Debug.Log("Tail: " + list.Tail);*/

            for (int i = 6; i < 13; i++)
                if (list.Get(i - 6) != i)
                {
                    Debug.Log("RemoveAtTest5: " + i + " != " + list.Get(i - 7));
                    return false;
                }

            for (int i = 14; i < 16; i++)
            {
                if (list.Get(i - 7) != i)
                {
                    Debug.Log("RemoveAtTest5: " + i + " != " + list.Get(i - 7));
                    return false;
                }
            }

            return list.Count == 9;
        }

        #endregion

        #region RemoveRangeTests

        [RunTest(true)]
        private bool RemoveRangeTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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

        #endregion

        #region RemoveAllTests

        [RunTest(true)]
        private bool RemoveAllTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(5);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 12; i++)
                if (i % 2 == 0)
                    list.Append(5);
                else
                    list.Append(0);

            list.RemoveAll(5);

            if (list.Count != 5)
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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    list.Append(5);
                else
                    list.Append(0);

            list.RemoveAll(7);

            return list.Count == 10;
        }

        #endregion

        #region RemoveFirstTests

        [RunTest(true)]
        public bool RemoveFirstTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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

        #endregion

        #region RemoveLastTests

        [RunTest(true)]
        public bool RemoveLastTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

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

        #endregion

        #region IndexOfTests

        [RunTest(true)]
        private bool IndexOfTest1()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            return list.IndexOf(5) == 5;
        }

        [RunTest(true)]
        private bool IndexOfTest2()
        {
            CircularList<int> list = new CircularList<int>(10);

            for (int i = 0; i < 10; i++)
                list.Append(i);

            return list.IndexOf(-1) == -1;
        }

        #endregion

        #region LargeScaleTests

        /// <summary>
        /// This tests wether the list can handle a large number of items
        /// without failing.
        /// </summary>
        /// <returns>True if no error</returns>
        [RunTest(true)]
        private bool LargeScaleTest1()
        {
            CircularList<int> lst = new CircularList<int>(10);

            for (int i = 0; i < short.MaxValue; i++)
                lst.Append(i);

            lst.RemoveAt(lst.Count - 1);

            return lst.Count == 9;
        }

        [RunTest(true)]
        private bool LargeScaleTest2()
        {
            CircularList<int> lst = new CircularList<int>(short.MaxValue);

            for (int i = 0; i < short.MaxValue; i++)
                lst.Append(i);

            lst.RemoveAt(lst.Count - 1);

            return lst.Count == (short.MaxValue - 1);
        }

        #endregion //LargeScaleTests
    }
}
