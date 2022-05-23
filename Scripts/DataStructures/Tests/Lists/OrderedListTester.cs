using System;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class OrderedListTester : TesterBase
    {
        public OrderedListTester()
        {
            testName = "Ordered List";
            testType = typeof(OrderedListTester);
        }

        [RunTest(true)]
        public bool AddTest1()
        {
            OrderedList<int> list = new OrderedList<int>();

            for(int i = 0; i < 10; i++)
            {
                list.Add(i);
            }

            for(int i = 0; i < 10; i++)
            {
                if(list[i] != (9 - i))
                {
                    Debug.Log("Ordered list add test 1 failed");
                    Debug.Log(list);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool AddTest2()
        {
            OrderedList<int> list = new OrderedList<int>();

            for (int i = 9; i >= 0; i--)
            {
                list.Add(i);
            }

            for (int i = 0; i < 10; i++)
            {
                if (list[i] != (9 - i))
                {
                    Debug.Log("Ordered list add test 2 failed");
                    Debug.Log(list);
                    return false;
                }
            }

            if(list.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log("List count is not correct");
                return false;
            }
        }

        [RunTest(true)]
        public bool AddTest3()
        {
            OrderedList<int> list = new OrderedList<int>();

            list.Add(0);
            list.Add(9);
            list.Add(1);
            list.Add(8);
            list.Add(2);
            list.Add(7);
            list.Add(3);
            list.Add(6);
            list.Add(4);
            list.Add(5);

            for (int i = 0; i < 10; i++)
            {
                if (list[i] != (9-i))
                {
                    Debug.Log("Ordered list add test 3 failed");
                    Debug.Log(list);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool AddTest4()
        {
            OrderedList<int> list = new OrderedList<int>();

            list.Add(5);
            list.Add(4);
            list.Add(6);
            list.Add(3);
            list.Add(7);
            list.Add(2);
            list.Add(8);
            list.Add(1);
            list.Add(9);
            list.Add(0);

            for (int i = 0; i < 10; i++)
            {
                if (list[i] != (9 - i))
                {
                    Debug.Log("Ordered list add test 3 failed");
                    Debug.Log(list);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool AppendBackTest1()
        {
            OrderedList<int> list = new OrderedList<int>();

            for (int i = 0; i < 10; i++)
            {
                list.AppendBack(i);
            }

            for (int i = 0; i < 10; i++)
            {
                if (list[i] != (9 - i))
                {
                    Debug.Log("Ordered list add test 1 failed");
                    Debug.Log(list);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool AppendBackTest2()
        {
            OrderedList<int> list = new OrderedList<int>();

            for (int i = 9; i >= 0; i--)
            {
                list.AppendBack(i);
            }

            for (int i = 0; i < 10; i++)
            {
                if (list[i] != (9 - i))
                {
                    Debug.Log("Ordered list add test 2 failed");
                    Debug.Log(list);
                    return false;
                }
            }

            if (list.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log("List count is not correct");
                return false;
            }
        }

        [RunTest(true)]
        public bool AppendBackTest3()
        {
            OrderedList<int> list = new OrderedList<int>();

            list.AppendBack(0);
            list.AppendBack(9);
            list.AppendBack(1);
            list.AppendBack(8);
            list.AppendBack(2);
            list.AppendBack(7);
            list.AppendBack(3);
            list.AppendBack(6);
            list.AppendBack(4);
            list.AppendBack(5);

            for (int i = 0; i < 10; i++)
            {
                if (list[i] != (9 - i))
                {
                    Debug.Log("Ordered list add test 3 failed");
                    Debug.Log(list);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool AppendBackTest4()
        {
            OrderedList<int> list = new OrderedList<int>();

            list.AppendBack(5);
            list.AppendBack(4);
            list.AppendBack(6);
            list.AppendBack(3);
            list.AppendBack(7);
            list.AppendBack(2);
            list.AppendBack(8);
            list.AppendBack(1);
            list.AppendBack(9);
            list.AppendBack(0);

            for (int i = 0; i < 10; i++)
            {
                if (list[i] != (9 - i))
                {
                    Debug.Log("Ordered list add test 3 failed");
                    Debug.Log(list);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool AppendBackTest5()
        {
            ArrayList<int> source = new ArrayList<int>(10);

            for(int i = 0; i < 10; i++)
            {
                source.Add(i);
            }

            OrderedList<int> dest = new OrderedList<int>();

            dest.AppendBack(source);

            for(int i = 9; i >= 0; i--)
            {
                if (dest[9-i] != i)
                {
                    Debug.Log("AppendBackTest5 failed: " + i + " != " + dest[9-i]);
                    return false;
                }
            }

            return dest.Count == 10;
        }

        [RunTest(true)]
        public bool InsertTest1()
        {
            OrderedList<int> list = new OrderedList<int>();

            list.AppendBack(0);
            for(int i = 1; i < 10; i++)
            {
                list.Insert(list.Count - 1, i);
            }

            for (int i = 9; i >= 0; i--)
            {
                if (list[9 - i] != i)
                {
                    Debug.Log("Insert test 1 failed: " + i + " != " + list[9 - i]);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool InsertTest2()
        {
            OrderedList<int> list = new OrderedList<int>();

            list.AppendBack(0);
            for (int i = 9; i >= 1; i--)
            {
                list.Insert(0, i);
            }

            for (int i = 9; i >= 0; i--)
            {
                if (list[9 - i] != i)
                {
                    Debug.Log("Insert test 2 failed: " + i + " != " + list[9 - i]);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        public bool InsertTest3()
        {
            ArrayList<int> source = new ArrayList<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            OrderedList<int> list = new OrderedList<int>();

            System.Random rand = new System.Random();
            while (source.Count > 0)
            {
                list.AppendBack(source.TakeAt(rand.Next(0, source.Count)));
            }

            for (int i = 9; i >= 0; i--)
            {
                if (list[9 - i] != i)
                {
                    Debug.Log("Insert test 3 failed: " + i + " != " + list[9 - i]);
                    return false;
                }
            }

            return list.Count == 10;
        }

        [RunTest(true)]
        private bool AppendFrontTest1()
        {
            OrderedList<int> listA = new OrderedList<int>();
            OrderedList<int> listB = new OrderedList<int>();

            for (int i = 9; i >= 0; i--)
            {
                listA.AppendFront(i);
            }

            for (int i = 19; i >= 10; i--)
            {
                listB.AppendFront(i);
            }

            OrderedList<int> listC = (listA + listB) as OrderedList<int>;

            for (int i = 0; i < 20; i++)
            {
                if (listC.Get(i) != (19-i))
                {
                    return false;
                }
            }

            return (listC.Count == listA.Count + listB.Count);
        }

        [RunTest(true)]
        private bool AppendFrontTest2()
        {
            OrderedList<int> listA = new OrderedList<int>();
            OrderedList<int> listB = new OrderedList<int>();

            for (int i = 9; i >= 0; i--)
            {
                listA.AppendFront(i);
            }

            for (int i = 19; i >= 10; i--)
            {
                listB.AppendFront(i);
            }

            listA = (listA + listB);

            for (int i = 0; i < 20; i++)
            {
                if (listA.Get(i) != (19-i))
                {
                    return false;
                }
            }

            return listA.Count == 20;
        }

        [RunTest(true)]
        private bool AppendFrontTest3()
        {
            OrderedList<int> listA = new OrderedList<int>();
            OrderedList<int> listB = new OrderedList<int>();

            for (int i = 9; i >= 0; i--)
            {
                listA.AppendFront(i);
            }

            for (int i = 19; i >= 10; i--)
            {
                listB.AppendFront(i);
            }

            listB = (listB + listA);

            for (int i = 0; i < 20; i++)
            {
                if (listB.Get(i) != (19-i))
                {
                    return false;
                }
            }

            return listB.Count == 20;
        }

        [RunTest(true)]
        public bool AddTest5()
        {
            OrderedList<int> listA = new OrderedList<int>();
            OrderedList<int> listB = new OrderedList<int>();

            for (int i = 9; i >= 0; i--)
            {
                listA.AppendFront(i);
            }

            for (int i = 19; i >= 10; i--)
            {
                listB.AppendFront(i);
            }

            listB = (listB + listA) as OrderedList<int>;

            for(int i = 20; i < 30; i++)
            {
                listB.AppendFront(i);
            }

            for (int i = 0; i < 30; i++)
            {
                if (listB.Get(i) != (29-i))
                {
                    Debug.Log("Ordered list add test 5 failed: " + listB[i] + " != " + i);
                    return false;
                }
            }

            return listB.Count == 30;
        }

        [RunTest(true)]
        public bool AddTest6()
        {
            OrderedList<int> listA = new OrderedList<int>();
            OrderedList<int> listB = new OrderedList<int>();

            for (int i = 9; i >= 0; i--)
            {
                listA.AppendFront(i);
            }

            for (int i = 19; i >= 10; i--)
            {
                listB.AppendFront(i);
            }

            listB = (listB + listA);

            for (int i = 29; i >= 20; i--)
            {
                listB.AppendFront(i);
            }

            for (int i = 0; i < 30; i++)
            {
                if (listB.Get(i) != (29-i))
                {
                    Debug.Log("Ordered list add faild: " + i + " != " + listB[i]);
                    return false;
                }
            }

            return listB.Count == 30;
        }

        [RunTest(true)]
        public bool EnumTest1()
        {
            OrderedList<int> list = new OrderedList<int>();

            for(int i = 0; i < 10; i++)
            {
                list.Add(i);
                //Debug.Log("Start of loop: " + i);
                int count = i;
                foreach(int val in list)
                {
                    if(val != count)
                    {
                        Debug.Log("OrderedList enum test 1 failed on loop " + i + " check " + count + ": " + val + " != " + count);
                        return false;
                    }
                    count--;
                }
            }

            return true;
        }
    }
}
