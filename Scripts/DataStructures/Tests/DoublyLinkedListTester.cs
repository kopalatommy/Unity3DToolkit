using System;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class DoublyLinkedListTester : TesterBase
    {
        public DoublyLinkedListTester()
        {
            testName = "Doubly Linked List";
            testType = typeof(DoublyLinkedListTester);
        }

        #region GetTests

        // This tests wether the list can access all added items and returns
        // the proper items
        [RunTest(false)]
        private bool GetTest1()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();
            bool succeeded = true;

            for (int i = 0; i < 10; i++)
            {
                list.AppendBack(i);
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
                    Debug.Log("DoublyLinkedList get test 1 encountered an index out of bounds");
                }
            }
            return succeeded;
        }

        // This tests the error cases. This makes sure an error is returned if
        // trying to access an invalid index
        [RunTest(false)]
        private bool GetTest2()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            try
            {
                list.Get(0);
                Debug.Log("DoublyLinkedList get test 2 failed at 1. Accessed item in empty list");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

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
                Debug.Log("DoublyLinkedList get test 2 failed at 3. Returned item with negatve index");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            return true;
        }

        #endregion

        #region SetTests

        [RunTest(false)]
        private bool SetTest1()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            DoublyLinkedList.Set(5, -1);

            if (DoublyLinkedList.Get(5) == -1)
                return true;
            else
                return false;
        }

        [RunTest(false)]
        private bool SetTest2()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

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

        [RunTest(false)]
        private bool SetTest3()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(0);

            for (int i = 0; i < 10; i++)
                list.Set(i, i);

            for(int i = 0; i < 10; i++)
                if(list[i] != i)
                    return false;

            return true;
        }

        #endregion

        #region AddTests

        [RunTest(false)]
        private bool AppendFrontTest1()
        {
            DoublyLinkedList<int> listA = new DoublyLinkedList<int>();
            DoublyLinkedList<int> listB = new DoublyLinkedList<int>();

            for (int i = 9; i >= 0; i--)
                listA.AppendFront(i);

            for (int i = 19; i >= 10; i--)
                listB.AppendFront(i);

            DoublyLinkedList<int> listC = (listA + listB) as DoublyLinkedList<int>;

            for (int i = 0; i < 20; i++)
            {
                if (listC.Get(i) != i)
                    return false;
            }

            return (listC.Count == listA.Count + listB.Count);
        }

        [RunTest(false)]
        private bool AppendFrontTest2()
        {
            DoublyLinkedList<int> listA = new DoublyLinkedList<int>();
            DoublyLinkedList<int> listB = new DoublyLinkedList<int>();

            for (int i = 9; i >= 0; i--)
                listA.AppendFront(i);

            for (int i = 19; i >= 10; i--)
                listB.AppendFront(i);

            listA = (listA + listB) as DoublyLinkedList<int>;

            for (int i = 0; i < 20; i++)
            {
                if (listA.Get(i) != i)
                    return false;
            }

            return listA.Count == 20;
        }

        [RunTest(false)]
        private bool AppendBackTest1()
        {
            DoublyLinkedList<int> listA = new DoublyLinkedList<int>();
            DoublyLinkedList<int> listB = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                listA.AppendBack(i);

            for (int i = 10; i < 20; i++)
                listB.AppendBack(i);

            DoublyLinkedList<int> listC = (listA + listB) as DoublyLinkedList<int>;

            for (int i = 0; i < 20; i++)
            {
                if (listC.Get(i) != i)
                    return false;
            }

            return (listC.Count == listA.Count + listB.Count);
        }

        [RunTest(false)]
        private bool AppendBackTest2()
        {
            DoublyLinkedList<int> listA = new DoublyLinkedList<int>();
            DoublyLinkedList<int> listB = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
            {
                listA.AppendBack(i);
            }

            for (int i = 10; i < 20; i++)
            {
                listB.AppendBack(i);
            }

            listA = (listA + listB) as DoublyLinkedList<int>;

            for (int i = 0; i < 20; i++)
            {
                if (listA.Get(i) != i)
                {
                    return false;
                }
            }

            return listA.Count == 20;
        }

        #endregion

        #region InsertTests

        [RunTest(false)]
        private bool InsertTest1()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
            {
                if (i != 4)
                {
                    list.AppendBack(i);
                }
            }

            list.Insert(4, 4);

            for (int i = 0; i < 10; i++)
            {
                if (list.Get(i) != i)
                {
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(false)]
        private bool InsertTest2()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
            {
                if (i != 7)
                {
                    list.AppendBack(i);
                }
            }

            list.Insert(7, 7);

            for (int i = 0; i < 10; i++)
            {
                if (list.Get(i) != i)
                {
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(false)]
        private bool InsertTest3()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            try
            {
                DoublyLinkedList.Insert(5, 5);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        #endregion

        #region EnumTests

        [RunTest(false)]
        public bool EnumTest1()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                DoublyLinkedList.AppendBack(i);
            }

            int count = 0;
            foreach (int i in DoublyLinkedList)
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

        [RunTest(false)]
        private bool TakeFirstTest1()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

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

        [RunTest(false)]
        private bool TakeFirstTest2()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

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

        [RunTest(false)]
        private bool TakeFirstTest3()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
            {
                DoublyLinkedList.AppendBack(i);
            }

            DoublyLinkedList.TakeFirst();

            for (int i = 1; i < 10; i++)
            {
                if (DoublyLinkedList.Get(i - 1) != i)
                {
                    return false;
                }
            }

            return DoublyLinkedList.Count == 9;
        }

        #endregion

        #region TakeAtTests

        [RunTest(false)]
        private bool TakeAtTest1()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            return DoublyLinkedList.TakeAt(5) == 5;
        }

        [RunTest(false)]
        private bool TakeAtTest2()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            int it = list.TakeAt(5);
            if(it != 5)
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
            return true;
        }

        [RunTest(false)]
        private bool TakeAtTest3()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            try
            {
                DoublyLinkedList.TakeAt(100);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTest(false)]
        private bool TakeAtTest4()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            for(int i = 8; i > 1; i--)
            {
                if(list.TakeAt(i) != i)
                    return false;
            }

            return list.Count == 3;
        }

        #endregion

        #region ContainsTests

        [RunTest(false)]
        private bool ContainsTest1()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            return DoublyLinkedList.Contains(1);
        }

        [RunTest(false)]
        private bool ContainsTest2()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            return DoublyLinkedList.Contains(11) == false;
        }

        [RunTest(false)]
        private bool ContainsTest3()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            return DoublyLinkedList.Contains(11) == false;
        }

        #endregion

        #region RemoveTests

        [RunTest(false)]
        private bool RemoveTest1()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            if (list.Remove(5))
            {
                return list.Contains(5) == false && list.Count == 9;
            }
            else
            {
                Debug.Log("Failed to remove item");
                return false;
            }
        }

        [RunTest(false)]
        private bool RemoveTest2()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            if (DoublyLinkedList.Remove(11) == false)
            {
                return DoublyLinkedList.Count == 10;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region RemoveAtTests

        [RunTest(false)]
        private bool RemoveAtTest1()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            list.RemoveAt(3);

            return list.Count == 9 && list.Contains(3) == false;
        }

        [RunTest(false)]
        private bool RemoveAtTest2()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

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

        [RunTest(false)]
        private bool RemoveAtTest3()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            list.RemoveAt(3);

            for (int i = 0; i < 3; i++)
                if (list.Get(i) != i)
                    return false;

            for (int i = 4; i < 10; i++)
            {
                if (list.Get(i - 1) != i)
                    return false;
            }

            return list.Count == 9;
        }

        [RunTest(false)]
        private bool RemoveAtTest4()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            list.RemoveAt(7);

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

        #endregion

        #region RemoveRangeTests

        [RunTest(false)]
        private bool RemoveRangeTest1()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            list.RemoveRange(3, 4);

            for (int i = 0; i < 3; i++)
                if (list.Get(i) != i)
                {
                    Debug.Log(list[i] + " != " + i);
                    return false;
                }

            for (int i = 7; i < 10; i++)
                if (list.Get(i - 4) != i)
                {
                    Debug.Log((i - 4) + " : " + list[i - 4] + " != " + i);
                    return false;
                }

            return list.Count == 6;
        }

        [RunTest(false)]
        private bool RemoveRangeTest2()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            try
            {
                DoublyLinkedList.RemoveRange(3, 10);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        #endregion

        #region RemoveAllTests

        [RunTest(false)]
        private bool RemoveAllTest1()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(5);

            DoublyLinkedList.RemoveAll(5);

            return DoublyLinkedList.Count == 0;
        }

        [RunTest(false)]
        private bool RemoveAllTest2()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    DoublyLinkedList.AppendBack(5);
                else
                    DoublyLinkedList.AppendBack(0);

            DoublyLinkedList.RemoveAll(5);

            return DoublyLinkedList.Count == 5;
        }

        [RunTest(false)]
        private bool RemoveAllTest3()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    DoublyLinkedList.AppendBack(5);
                else
                    DoublyLinkedList.AppendBack(0);

            DoublyLinkedList.RemoveAll(7);

            return DoublyLinkedList.Count == 10;
        }

        #endregion

        #region RemoveFirstTests

        [RunTest(false)]
        public bool RemoveFirstTest1()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            for(int i = 0; i < 9; i++)
            {
                list.RemoveFirst();
                if(list[0] != (i + 1))
                {
                    Debug.Log(list[0] + " != " + (i + 1));
                    return false;
                }
            }

            return list.Count == 1;
        }

        [RunTest(false)]
        public bool RemoveFirstTest2()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

            for (int i = 0; i < 10; i++)
                list.RemoveFirst();

            try
            {
                list.RemoveFirst();
                return false;
            }
            catch(IndexOutOfRangeException)
            {
                return true;
            }
        }

        #endregion

        #region RemoveFirstTests

        [RunTest(false)]
        public bool RemoveLastTest1()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

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

        [RunTest(false)]
        public bool RemoveLastTest2()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                list.AppendBack(i);

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

        [RunTest(false)]
        private bool IndexOfTest1()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            return DoublyLinkedList.IndexOf(5) == 5;
        }

        [RunTest(false)]
        private bool IndexOfTest2()
        {
            DoublyLinkedList<int> DoublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 10; i++)
                DoublyLinkedList.AppendBack(i);

            return DoublyLinkedList.IndexOf(-1) == -1;
        }

        #endregion

        #region LargeScaleTests

        /// <summary>
        /// This tests wether the list can handle a large number of items
        /// without failing.
        /// </summary>
        /// <returns>True if no error</returns>
        [RunTest(false)]
        private bool LargeScaleTest1()
        {
            DoublyLinkedList<int> lst = new DoublyLinkedList<int>();

            for (int i = 0; i < short.MaxValue; i++)
                lst.AppendBack(i);

            lst.Remove(lst.Count - 1);

            return true;
        }

        #endregion //LargeScaleTests
    }
}
