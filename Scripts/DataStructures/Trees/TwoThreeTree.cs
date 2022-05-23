using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.DataStructures
{
    public class TwoThreeTree<T>
    {
        /*internal class Node
        {
            public bool IsLeaf
            {
                get { return values[0] == null && values[1] == null && values[2] == null; }
            }

            public T[] values = new T[3];
            public Node[] childs = new Node[3];

            public Node(T item)
            {
                values[1] = item;
            }

            public bool Contains(T item)
            {
                return values[0].Equals(item) || values[1].Equals(item) || values[2].Equals(item);
            }
        }

        public int Count
        {
            get { return count; }
        }

        private int count = 0;
        private Node head = null;
        
        public void Add(T item)
        {
            if (head == null)
            {
                head = new Node(item);
                return;
            }



            if (candidate->isleaf())
            {
                candidate->store(key);
            }
            else
            {
                if (*candidate->firstkey > *key)
                {
                    insert(candidate->less, key); //Insert to left subtree.
                }
                else if (*candidate->firstkey <= *key && candidate->is2node())
                {
                    insert(candidate->btwn, key); //Insert to mid subtree.
                }
                else
                {
                    if (*candidate->secondkey > *key)
                    {
                        insert(candidate->btwn, key); //Insert to mid subtree.
                    }
                    else
                    {
                        insert(candidate->great, key); //Insert to right subtree.
                    }
                }
            }
            split(candidate); //This function is to balance the tree incase of an overflow.
        }*/
    }
}
