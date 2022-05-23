using System;
using Toolkit.DataStructures;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class BTreeV2Tester : TesterBase
    {
        public BTreeV2Tester()
        {
            testName = "B Tree V2";
            testType = typeof(BTreeV2Tester);
        }

        [RunTest(true)]
        public bool InsertTest1()
        {
            BTreeV2<int> tree = new BTreeV2<int>(3);

            for (int i = 99; i >= 0; i--)
            {
                tree.Insert(i);
            }

            for (int i = 0; i < 100; i++)
                if (!tree.Contains(i))
                {
                    Debug.Log("B Tree does not contain: " + i);
                    return false;
                }

            if (tree.Count != 100)
            {
                Debug.Log("B Tree insertion test 1 failed: Count != 100: " + tree.Count);
                return false;
            }
            return true;
        }

        [RunTest(true)]
        public bool ToCollectionTest1()
        {
            BTreeV2<int> tree = new BTreeV2<int>(3);
            System.Collections.Generic.ICollection<int> dest = new Toolkit.DataStructures.ArrayList<int>();

            for (int i = 100; i >= 0; i--)
            {
                tree.Insert(i);
            }

            tree.ToCollection(ref dest);

            ArrayList<int> temp = dest as ArrayList<int>;

            for (int i = 0; i < 101; i++)
                if (temp[i] != i)
                {
                    Debug.Log("ToCollectionTest1 failed: List(" + temp[i] + ") != " + i);
                    return false;
                }

            return temp.Count == tree.Count;
        }

        [RunTest(true)]
        public bool RemoveTest1()
        {
            BTreeV2<int> tree = new BTreeV2<int>(3);

            System.Random rand = new System.Random();
            DoublyLinkedList<int> source = new DoublyLinkedList<int>();
            for (int i = 0; i < 100; i++)
                source.AppendBack(i);

            for (int i = 99; i >= 0; i--)
                tree.Insert(source.TakeAt(rand.Next(0, i)));

            for (int i = 0; i < 100; i++)
                source.AppendBack(i);

            for (int i = 99; i >= 0; i--)
            {
                int ind = source.TakeAt(rand.Next(0, i));
                //Debug.Log("Removing: " + ind);
                if (!tree.Remove(ind))
                {
                    Debug.Log("Failed to remove: " + ind);
                    Debug.Log(tree);
                    return false;
                }
                else if (tree.Contains(ind))
                {
                    Debug.Log("Tree still contains: " + ind);
                    Debug.Log(tree);
                    return false;
                }
                //Debug.Log(tree);
            }

            if (tree.Count != 0)
            {
                Debug.Log("BTreeV2 RemoveTest 1 failed. Count != 0: " + tree.Count);
                return false;
            }
            else
                return true;
        }

        [RunTest(true)]
        public bool RemoveTest2()
        {
            BTreeV2<int> tree = new BTreeV2<int>(3);

            tree.Insert(0);
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);

            //Debug.Log(tree.TreeString());

            if (!tree.Remove(0))
            {
                Debug.Log("Failed to remove 0");
                return false;
            }

            //Debug.Log(tree);

            if (!tree.Remove(1))
            {
                Debug.Log("Failed to remove 1");
                return false;
            }

            //Debug.Log(tree);

            if (!tree.Remove(2))
            {
                Debug.Log("Failed to remove 2");
                return false;
            }

            //Debug.Log(tree);

            if (!tree.Remove(3))
            {
                Debug.Log("Failed to remove 3");
                return false;
            }

            //Debug.Log(tree);

            if (!tree.Remove(4))
            {
                Debug.Log("Failed to remove 4");
                return false;
            }

            //Debug.Log(tree);

            if (!tree.Remove(5))
            {
                Debug.Log("Failed to remove 5");
                return false;
            }

            //Debug.Log(tree);

            if (!tree.Remove(6))
            {
                Debug.Log("Failed to remove 6");
                return false;
            }

            //Debug.Log(tree);

            if (tree.Count != 0)
            {
                Debug.Log("Remove Test 2 failed: Count != 2: " + tree.Count);
                return false;
            }
            return true;
        }

        [RunTest(true)]
        public bool RemoveTest3()
        {
            BTreeV2<int> tree = new BTreeV2<int>(3);

            for (int i = 0; i < 100; i++)
                tree.Insert(i);

            for (int i = 0; i < 100; i++)
            {
                if (!tree.Remove(i))
                {
                    Debug.Log("Failed to remove " + i);
                    Debug.Log(tree);
                    return false;
                }
            }

            if (tree.Count != 0)
            {
                Debug.Log("RemoveTest3 failed: Count != 0: " + tree.Count);
                return false;
            }

            return true;
        }

        [RunTest(true)]
        public bool RemoveTest4()
        {
            BTreeV2<int> tree = new BTreeV2<int>(3);

            for (int i = 0; i < 100; i++)
                tree.Insert(i);

            for (int i = 99; i >= 0; i--)
            {
                if (!tree.Remove(i))
                {
                    Debug.Log("Failed to remove " + i);
                    Debug.Log(tree);
                    return false;
                }
            }

            if (tree.Count != 0)
            {
                Debug.Log("RemoveTest4 failed: Count != 0: " + tree.Count);
                return false;
            }

            return true;
        }

        [RunTest(true)]
        public bool RemoveTest5()
        {
            BTreeV2<int> tree = new BTreeV2<int>(3);

            for (int i = 99; i >= 0; i--)
                tree.Insert(i);

            //Debug.Log(tree.TreeString());

            for (int i = 0; i < 100; i++)
            {
                //Debug.Log("Removing: " + i);
                //Debug.Log(tree);
                if (!tree.Remove(i))
                {
                    Debug.Log("Failed to remove " + i);
                    Debug.Log(tree);
                    return false;
                }
            }

            if (tree.Count != 0)
            {
                Debug.Log("RemoveTest5 failed: Count != 0: " + tree.Count);
                return false;
            }

            return true;
        }

        [RunTest(true)]
        public bool GenTest1()
        {
            BTreeV2<int> tree = new BTreeV2<int>(3);

            DoublyLinkedList<int> source = new DoublyLinkedList<int>();

            /*for(int i = 0; i < 20; i++)
                source.AppendBack(i);*/
            System.Random rand = new System.Random();

            /*for (int i = 19; i >= 0; i--)
            {
                tree.Insert(source.TakeAt(rand.Next(0, i)));
            }*/
            /*for(int i = 19; i >= 0; i--)
                tree.Insert(i);
            Debug.Log(tree.TreeString());

            for (int i = 19; i >= 0; i--)
            {
                if(!tree.Remove(i))
                {
                    Debug.Log("Failed to remove: " + i);
                    return false;
                }
                Debug.Log(tree.TreeString());
            }*/


            /*for(int j = 0; j < 100; j++)
            {
                tree = new BTreeV2<int>(3);
                for (int i = 0; i < 100 + j; i++)
                    source.AppendBack(i);
                for (int i = 100 + j - 1; i >= 0; i--)
                    tree.Insert(source.TakeAt(rand.Next(0, i)));
                for (int i = 0; i < 100 + j; i++)
                    source.AppendBack(i);
                for (int i = 100 + j - 1; i >= 0; i--)
                {
                    int rem = source.TakeAt(rand.Next(0, i));
                    string prev = tree.TreeString();
                    try
                    {
                        if (!tree.Remove(rem))
                        {
                            Debug.Log("Failed to remove " + rem + " on interation " + j);
                            Debug.Log(prev);
                            Debug.Log(tree.TreeString());
                            return false;
                        }
                    }
                    catch
                    {
                        Debug.Log("Encountered exception when removing " + rem + " on interation " + j);
                        Debug.Log(prev);
                        Debug.Log(tree.TreeString());
                        return false;
                    }
                }
            }*/

            tree = new BTreeV2<int>(3);
            /*for (int i = 0; i < 20; i++)
                tree.Insert(i);*/


            /*Debug.Log("Build in sequential order, remove in random");
            for(int i = 0; i < 1; i++)
            {
                tree = new BTreeV2<int>(3);

                for(int j = 0; j < 100; j++)
                {
                    tree.Insert(j);
                    source.AppendFront(j);
                }

                // Remove in random order
                for (int j = 99; j >= 0; j--)
                {
                    Debug.Log("Removing: " + i);
                    Debug.Log(tree.TreeString());
                    if (!tree.Remove(source.TakeAt(rand.Next(0, j))))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }*/

            //Debug.Log("Build in sequential order, remove in sequential");
            for (int i = 0; i < 100; i++)
            {
                tree = new BTreeV2<int>(3);

                for (int j = 0; j < 100; j++)
                {
                    tree.Insert(j);
                    source.AppendFront(j);
                }

                //Debug.Log(tree.TreeString());

                // Remove in random order
                for (int j = 0; j < 100; j++)
                {
                    /*Debug.Log("Removing: " + i);
                    Debug.Log(tree.TreeString());*/
                    if (!tree.Remove(j))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }

            //Debug.Log("Build in sequential order, remove in reverse");
            for (int i = 0; i < 100; i++)
            {
                tree = new BTreeV2<int>(3);

                for (int j = 0; j < 100; j++)
                {
                    tree.Insert(j);
                    source.AppendFront(j);
                }

                //Debug.Log(tree.TreeString());

                // Remove in random order
                for (int j = 99; j >= 0; j--)
                {
                    if (!tree.Remove(j))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }

            //Debug.Log("Build in reverse order, remove in random");
            for (int i = 0; i < 100; i++)
            {
                tree = new BTreeV2<int>(3);

                for (int j = 99; j >= 0; j--)
                {
                    tree.Insert(j);
                    source.AppendFront(j);
                }

                //Debug.Log(tree.TreeString());

                // Remove in random order
                for (int j = 99; j >= 0; j--)
                {
                    if (!tree.Remove(source.TakeAt(rand.Next(0, j))))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }

            //Debug.Log("Build in reverse order, remove in reverse");
            for (int i = 0; i < 100; i++)
            {
                tree = new BTreeV2<int>(3);

                for (int j = 99; j >= 0; j--)
                {
                    tree.Insert(j);
                    source.AppendFront(j);
                }

                //Debug.Log(tree.TreeString());

                // Remove in random order
                for (int j = 99; j >= 0; j--)
                {
                    if (!tree.Remove(j))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }

            //Debug.Log("Build in reverse order, remove in sequential");
            for (int i = 0; i < 100; i++)
            {
                tree = new BTreeV2<int>(3);

                for (int j = 99; j >= 0; j--)
                {
                    tree.Insert(j);
                    source.AppendFront(j);
                }

                //Debug.Log(tree.TreeString());

                // Remove in random order
                for (int j = 0; j < 100; j++)
                {
                    if (!tree.Remove(j))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }

            //Debug.Log("Build in random order, remove in random");
            for (int i = 0; i < 100; i++)
            {
                tree = new BTreeV2<int>(3);

                for (int j = 99; j >= 0; j--)
                {
                    source.AppendFront(j);
                }
                for (int j = 99; j >= 0; j--)
                {
                    tree.Insert(source.TakeAt(rand.Next(0, j)));
                }

                //Debug.Log(tree.TreeString());

                // Remove in random order
                for (int j = 99; j >= 0; j--)
                {
                    if (!tree.Remove(source.TakeAt(rand.Next(0, j))))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }

            //Debug.Log("Build in random order, remove in reverse");
            for (int i = 0; i < 100; i++)
            {
                tree = new BTreeV2<int>(3);

                for (int j = 99; j >= 0; j--)
                {
                    source.AppendFront(j);
                }
                for (int j = 99; j >= 0; j--)
                {
                    tree.Insert(source.TakeAt(rand.Next(0, j)));
                }

                //Debug.Log(tree.TreeString());

                // Remove in random order
                for (int j = 99; j >= 0; j--)
                {
                    if (!tree.Remove(j))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }

            //Debug.Log("Build in random order, remove in sequential");
            for (int i = 0; i < 100; i++)
            {
                tree = new BTreeV2<int>(3);

                for (int j = 99; j >= 0; j--)
                {
                    source.AppendFront(j);
                }
                for (int j = 99; j >= 0; j--)
                {
                    tree.Insert(source.TakeAt(rand.Next(0, j)));
                }

                //Debug.Log(tree.TreeString());

                // Remove in random order
                for (int j = 0; j < 100; j++)
                {
                    if (!tree.Remove(j))
                    {
                        Debug.Log("Failed on iter " + i);
                        return false;
                    }
                }
            }

            /*if (!tree.Remove(5))
            {
                Debug.Log("Failed to remove 5");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(12))
            {
                Debug.Log("Failed to remove 12");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(18))
            {
                Debug.Log("Failed to remove 18");
                return false;
            }
            Debug.Log(tree.TreeString());*/

            //for(int i = 19; i >= 0; i--)
            /*for (int i = 0; i < 20; i++)
            {
                if (!tree.Remove(i))
                {
                    Debug.Log("Failed to remove " + i);
                    return false;
                }
                Debug.Log(tree.TreeString());
            }*/

            /*if(!tree.Remove(19))
            {
                Debug.Log("Failed to remove 19");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(18))
            {
                Debug.Log("Failed to remove 18");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(17))
            {
                Debug.Log("Failed to remove 17");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(16))
            {
                Debug.Log("Failed to remove 16");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(15))
            {
                Debug.Log("Failed to remove 15");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(14))
            {
                Debug.Log("Failed to remove 14");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(13))
            {
                Debug.Log("Failed to remove 13");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(12))
            {
                Debug.Log("Failed to remove 12");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(11))
            {
                Debug.Log("Failed to remove 11");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(10))
            {
                Debug.Log("Failed to remove 10");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(9))
            {
                Debug.Log("Failed to remove 9");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(8))
            {
                Debug.Log("Failed to remove 8");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(7))
            {
                Debug.Log("Failed to remove 7");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(6))
            {
                Debug.Log("Failed to remove 6");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(5))
            {
                Debug.Log("Failed to remove 5");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(4))
            {
                Debug.Log("Failed to remove 4");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(3))
            {
                Debug.Log("Failed to remove 3");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(2))
            {
                Debug.Log("Failed to remove 2");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(1))
            {
                Debug.Log("Failed to remove 1");
                return false;
            }
            Debug.Log(tree.TreeString());

            if (!tree.Remove(0))
            {
                Debug.Log("Failed to remove 0");
                return false;
            }
            Debug.Log(tree.TreeString());*/

            /*tree = new BTreeV2<int>(3);
            for (int i = 0; i < 20; i++)
                tree.Insert(i);

            Debug.Log(tree.TreeString());

            tree = new BTreeV2<int>(3);
            for (int i = 19; i >= 0; i--)
                tree.Insert(i);

            Debug.Log(tree.TreeString());*/

            return true;
        }

        [RunTest(true)]
        public bool GenTest2()
        {
            System.Random rand = new System.Random();

            DoublyLinkedList<int> source = new DoublyLinkedList<int>();

            for (int k = 0; k < 1000; k++)
            {
                BTreeV2<int> tree = new BTreeV2<int>(rand.Next(3, 10));
                for (int i = 0; i < 100; i++)
                    source.AppendBack(i);
                for (int i = 99; i >= 0; i--)
                    tree.Insert(source.TakeAt(rand.Next(0, i)));

                for (int i = 0; i < 100; i++)
                    source.AppendBack(i);
                for (int i = 99; i >= 0; i--)
                    tree.Remove(source.TakeAt(rand.Next(0, i)));
            }

            return true;
        }
    }
}
