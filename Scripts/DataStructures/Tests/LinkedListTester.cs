using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Toolkit.Test;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;

namespace Toolkit.DataStructures.Tests
{
    public class LinkedListTester : TesterBase
    {
        public LinkedListTester()
        {
            testName = "Linked List";
            testType = typeof(LinkedListTester);
        }

        #region GetTests

        // This tests wether the list can access all added items and returns
        // the proper items
        [RunTestAttribute(true)]
        private bool GetTest1()
        {
            LinkedList<int> list = new LinkedList<int>();
            bool succeeded = true;

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
                        Debug.Log("Failed on " + i);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    succeeded = false;
                    Debug.Log("Failed on " + i);
                    Debug.Log("LinkedList get test 1 encountered an index out of bounds");
                }
            }
            return succeeded;
        }

        // This tests the error cases. This makes sure an error is returned if
        // trying to access an invalid index
        [RunTestAttribute(true)]
        private bool GetTest2()
        {
            LinkedList<int> list = new LinkedList<int>();

            try
            {
                list.Get(0);
                Debug.Log("LinkedList get test 2 failed at 1. Accessed item in empty list");
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
                Debug.Log("LinkedList get test 2 failed at 3. Returned item with negatve index");
                return false;
            }
            catch (IndexOutOfRangeException)
            {

            }

            return true;
        }

        #endregion

        #region SetTests

        [RunTestAttribute(true)]
        private bool SetTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            linkedList.Set(5, -1);

            if (linkedList.Get(5) == -1)
                return true;
            else
                return false;
        }

        [RunTestAttribute(true)]
        private bool SetTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            try
            {
                linkedList.Set(11, -1);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        #endregion

        #region AddTests

        [RunTestAttribute(true)]
        private bool AddTest1()
        {
            LinkedList<int> listA = new LinkedList<int>();
            LinkedList<int> listB = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                listA.Append(i);

            for (int i = 10; i < 20; i++)
                listB.Append(i);

            LinkedList<int> listC = listA + listB;

            /*Debug.Log(listA.ToString());
            Debug.Log(listB.ToString());
            Debug.Log(listC.ToString());*/

            for (int i = 0; i < 20; i++)
            {
                if (listC.Get(i) != i)
                {
                    Debug.Log("AddTest1 failed: item at " + i + " != " + i + ": " + listC.Get(i));
                    return false;
                }
            }

            if (listC.Count != (listA.Count + listB.Count))
            {
                Debug.Log("Add test 1 failed: combined list count does not equal source lists count");
                Debug.Log(listC.Count + " != " + (listA.Count + listB.Count));

                return false;
            }
            else
                return true;
        }

        [RunTestAttribute(true)]
        private bool AddTest2()
        {
            LinkedList<int> listA = new LinkedList<int>();
            LinkedList<int> listB = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                listA.Append(i);

            for (int i = 10; i < 20; i++)
                listB.Append(i);

            listA += listB;

            for (int i = 0; i < 20; i++)
            {
                if (listA.Get(i) != i)
                    return false;
            }

            if (listA.Count != 20)
            {
                Debug.Log("Add test 2 failed: " + listA.Count + " != 20");
                return false;
            }
            else
                return true;
        }

        #endregion

        #region InsertTests

        [RunTestAttribute(true)]
        private bool InsertTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                if (i != 4)
                    linkedList.Append(i);

            linkedList.Insert(4, 4);

            //Debug.Log(linkedList.ToString());

            for (int i = 0; i < 10; i++)
                if (linkedList.Get(i) != i)
                    return false;

            return linkedList.Count == 10;
        }

        [RunTestAttribute(true)]
        private bool InsertTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            try
            {
                linkedList.Insert(5, 5);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        #endregion

        #region EnumTests

        [RunTestAttribute(true)]
        public bool EnumTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();
            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            //Debug.Log(linkedList.ToString());

            int count = 0;
            foreach (int i in linkedList)
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

        [RunTestAttribute(true)]
        private bool TakeFirstTest1()
        {
            LinkedList<int> list = new LinkedList<int>();

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

        [RunTestAttribute(true)]
        private bool TakeFirstTest2()
        {
            LinkedList<int> list = new LinkedList<int>();

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

        [RunTestAttribute(true)]
        private bool TakeFirstTest3()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            linkedList.TakeFirst();

            for (int i = 1; i < 10; i++)
                if (linkedList.Get(i - 1) != i)
                    return false;

            return linkedList.Count == 9;
        }

        #endregion

        #region TakeAtTests

        [RunTestAttribute(true)]
        private bool TakeAtTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            return linkedList.TakeAt(5) == 5;
        }

        [RunTestAttribute(true)]
        private bool TakeAtTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            linkedList.TakeAt(5);

            for (int i = 1; i < 9; i++)
            {
                if (i < 5)
                {
                    if (linkedList.Get(i) != i)
                    {
                        return false;
                    }
                }
                else
                {
                    if (linkedList.Get(i) != (i + 1))
                        return false;
                }
            }
            return true;
        }

        [RunTestAttribute(true)]
        private bool TakeAtTest3()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            try
            {
                linkedList.TakeAt(100);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        #endregion

        #region ContainsTests

        [RunTestAttribute(true)]
        private bool ContainsTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            return linkedList.Contains(1);
        }

        [RunTestAttribute(true)]
        private bool ContainsTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            return linkedList.Contains(11) == false;
        }

        [RunTestAttribute(true)]
        private bool ContainsTest3()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            return linkedList.Contains(11) == false;
        }

        #endregion

        #region RemoveTests

        [RunTestAttribute(true)]
        private bool RemoveTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            if (linkedList.Remove(5))
            {
                return linkedList.Contains(5) == false && linkedList.Count == 9;
            }
            else
            {
                return false;
            }
        }

        [RunTestAttribute(true)]
        private bool RemoveTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            if (linkedList.Remove(11) == false)
            {
                return linkedList.Count == 10;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region RemoveAtTests

        [RunTestAttribute(true)]
        private bool RemoveAtTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            linkedList.RemoveAt(3);

            return linkedList.Count == 9 && linkedList.Contains(3) == false;
        }

        [RunTestAttribute(true)]
        private bool RemoveAtTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            try
            {
                linkedList.RemoveAt(3);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        [RunTestAttribute(true)]
        private bool RemoveAtTest3()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            linkedList.RemoveAt(3);

            for (int i = 0; i < 3; i++)
                if (linkedList.Get(i) != i)
                    return false;

            for (int i = 4; i < 10; i++)
            {
                if (linkedList.Get(i - 1) != i)
                    return false;
            }

            return linkedList.Count == 9;
        }

        #endregion

        #region RemoveRangeTests

        [RunTestAttribute(true)]
        private bool RemoveRangeTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            //Debug.Log(linkedList.ToString());

            linkedList.RemoveRange(3, 4);

            //Debug.Log(linkedList.ToString());

            for (int i = 0; i < 3; i++)
                if (linkedList.Get(i) != i)
                    return false;

            for (int i = 7; i < 10; i++)
                if (linkedList.Get(i - 4) != i)
                    return false;

            return linkedList.Count == 6;
        }

        [RunTestAttribute(true)]
        private bool RemoveRangeTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            try
            {
                linkedList.RemoveRange(3, 10);
                return false;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }

        #endregion

        #region RemoveAllTests

        [RunTestAttribute(true)]
        private bool RemoveAllTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(5);

            linkedList.RemoveAll(5);

            return linkedList.Count == 0;
        }

        [RunTestAttribute(true)]
        private bool RemoveAllTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    linkedList.Append(5);
                else
                    linkedList.Append(0);

            linkedList.RemoveAll(5);

            return linkedList.Count == 5;
        }

        [RunTestAttribute(true)]
        private bool RemoveAllTest3()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                if (i % 2 == 0)
                    linkedList.Append(5);
                else
                    linkedList.Append(0);

            linkedList.RemoveAll(7);

            return linkedList.Count == 10;
        }

        #endregion

        #region IndexOfTests

        [RunTestAttribute(true)]
        private bool IndexOfTest1()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            return linkedList.IndexOf(5) == 5;
        }

        [RunTestAttribute(true)]
        private bool IndexOfTest2()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < 10; i++)
                linkedList.Append(i);

            return linkedList.IndexOf(-1) == -1;
        }

        #endregion

        #region LargeScaleTests

        /// <summary>
        /// This tests wether the list can handle a large number of items
        /// without failing.
        /// </summary>
        /// <returns>True if no error</returns>
        /// [TestAttribute(true)]
        [RunTestAttribute(true)]
        private bool LargeScaleTest1()
        {
            LinkedList<int> lst = new LinkedList<int>();

            for (int i = 0; i < short.MaxValue; i++)
                lst.Append(i);

            lst.Remove(lst.Count - 1);

            return true;
        }

        #endregion //LargeScaleTests
    }
}
