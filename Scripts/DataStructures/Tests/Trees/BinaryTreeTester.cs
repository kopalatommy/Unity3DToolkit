using System;
using Toolkit.DataStructures;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    class IntComparer : System.Collections.Generic.IComparer<int>
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

    public class BinaryTreeTester : TesterBase
    {
        public BinaryTreeTester()
        {
            testName = "Binary Tree";
            testType = typeof(BinaryTreeTester);
        }

        [RunTest(false)]
        private bool AddTest1()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new IntComparer());

            tree.Insert(5);
            for(int i = 0; i < 5; i++)
                tree.Insert(i);
            for(int i = 6; i < 10; i++)
                tree.Insert(i);

            for(int i = 0; i < 10; i++)
                if(!tree.Contains(i))
                {
                    Debug.Log("Binary tree does not contain: " + i);
                    return false;
                }

            return tree.Count == 10;
        }

        [RunTest(true)]
        private bool RemoveTest1()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new IntComparer());

            tree.Insert(5);
            for (int i = 0; i < 5; i++)
                tree.Insert(i);
            for (int i = 6; i < 10; i++)
                tree.Insert(i);

            //Debug.Log(tree.ToString());

            if (!tree.Remove(5))
            {
                Debug.Log("Binary tree failed to remove 5");
                return false;
            }

            //Debug.Log(tree.ToString());

            for (int i = 0; i < 10; i++)
                if (i != 5 && !tree.Contains(i))
                {
                    Debug.Log("RemoveTest1: " + tree.ToString());
                    Debug.Log(tree.TreeString());
                    Debug.Log("Binary tree does not contain: " + i);
                    return false;
                }

            return tree.Count == 9;
        }

        // Test removing a few elements from the tree
        [RunTest(true)]
        private bool RemoveTest2()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new IntComparer());

            tree.Insert(5);
            for (int i = 0; i < 5; i++)
                tree.Insert(i);
            for (int i = 6; i < 10; i++)
                tree.Insert(i);

            //Debug.Log("Initial: " + tree.ToString());

            if (!tree.Remove(5))
            {
                Debug.Log("Binary tree failed to remove 5");
                return false;
            }

            //Debug.Log("After 5: " + tree.ToString());
            //Debug.Log(tree.TreeString());

            if (!tree.Remove(3))
            {
                Debug.Log("Binary tree failed to remove 3");
                return false;
            }

            //Debug.Log("After 3: " + tree.ToString());
            //Debug.Log(tree.TreeString());

            if (!tree.Remove(8))
            {
                Debug.Log("Binary tree failed to remove 8\n" + tree.TreeString());
                return false;
            }

            //Debug.Log("After 8: " + tree.ToString());

            for (int i = 0; i < 10; i++)
                if (i != 5 && i != 8 && i != 3 && !tree.Contains(i))
                {
                    Debug.Log("Binary tree does not contain: " + i);
                    return false;
                }

            return tree.Count == 7;
        }

        [RunTest(false)]
        private bool RemoveTest3()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new IntComparer());

            tree.Insert(5);
            for (int i = 0; i < 5; i++)
                tree.Insert(i);
            for (int i = 6; i < 10; i++)
                tree.Insert(i);

            Debug.Log(tree.TreeString());

            for(int i = 0; i < 10; i++)
                if(!tree.Remove(i))
                {
                    Debug.Log("RemoveTest3: " + tree.ToString());
                    Debug.Log("Binary failed to remove: " + i);
                    return false;
                }

            return tree.Count == 0;
        }

        // Test removing all elements in the tree
        [RunTest(true)]
        private bool RemoveTest4()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new IntComparer());

            tree.Insert(5);
            for (int i = 0; i < 5; i++)
                tree.Insert(i);
            for (int i = 6; i < 10; i++)
                tree.Insert(i);

            for (int i = 9; i >= 0; i--)
                if (!tree.Remove(i))
                {
                    Debug.Log("Binary failed to remove: " + i);
                    Debug.Log(tree.TreeString());
                    return false;
                }

            return tree.Count == 0;
        }

        // Test removing the last element in the tree
        [RunTest(true)]
        private bool RemoveTest5()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new IntComparer());

            tree.Insert(5);
            for (int i = 0; i < 5; i++)
                tree.Insert(i);
            for (int i = 6; i < 10; i++)
                tree.Insert(i);

            if(!tree.Remove(4))
            {
                Debug.Log("RemoveTest5: Failed to remove 4");
                Debug.Log(tree.TreeString());
                Debug.Log(tree);
                return false;
            }

            for (int i = 9; i >= 0; i--)
                if (i != 4 && !tree.Contains(i))
                {
                    Debug.Log("Binary does not contain: " + i);
                    Debug.Log(tree.TreeString());
                    return false;
                }

            return tree.Count == 9;
        }

        // Test removing a node in the middle of the tree
        [RunTest(true)]
        private bool RemoveTest6()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new IntComparer());

            tree.Insert(5);
            for (int i = 0; i < 5; i++)
                tree.Insert(i);
            for (int i = 6; i < 10; i++)
                tree.Insert(i);

            if (!tree.Remove(2))
            {
                Debug.Log("RemoveTest5: Failed to remove 4");
                Debug.Log(tree.TreeString());
                Debug.Log(tree);
                return false;
            }

            for (int i = 9; i >= 0; i--)
                if (i != 2 && !tree.Contains(i))
                {
                    Debug.Log("Binary does not contain: " + i);
                    Debug.Log(tree.TreeString());
                    return false;
                }

            return tree.Count == 9;
        }

        // Test removing the head of the tree
        [RunTest(true)]
        private bool RemoveTest7()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new IntComparer());

            tree.Insert(5);
            for (int i = 0; i < 5; i++)
                tree.Insert(i);
            for (int i = 6; i < 10; i++)
                tree.Insert(i);

            if (!tree.Remove(5))
            {
                Debug.Log("RemoveTest5: Failed to remove 4");
                Debug.Log(tree.TreeString());
                Debug.Log(tree);
                return false;
            }

            for (int i = 9; i >= 0; i--)
                if (i != 5 && !tree.Contains(i))
                {
                    Debug.Log("Binary does not contain: " + i);
                    Debug.Log(tree.TreeString());
                    return false;
                }

            return tree.Count == 9;
        }
    }
}
