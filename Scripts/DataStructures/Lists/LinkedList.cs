using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.DataStructures
{
    // This class holds the elements for the linked list. This
    // is incharge of holding a refernce to the next item in the
    // linked list and the item at the location
    [Serializable]
    public class LinkedListNode<T>
    {
        public T item;
        public LinkedListNode<T> next = null;

        public LinkedListNode(T item, LinkedListNode<T> next)
        {
            this.item = item;
            this.next = next;
        }
    }

    // A linked list is a collection that stores data in a chain of elements. The 
    // class holds an refernce to the first element in the list. Each element in 
    // the list tracks a reference to the next element
    [Serializable]
    public class LinkedList<T> : ICollection, IEnumerable
    {
        // The first element in the list
        private LinkedListNode<T> head = null;
        // The number of item in the list
        private int count = 0;

        // Getter function for count
        public int Count
        {
            get
            {
                return count;
            }
        }

        public T this[int index]
        {
            get { return Get(index); }
            set { Set(index, value); }
        }

        public bool IsSynchronized { get { return true; } }

        public object SyncRoot { get { return this; } }

        // Default constructor
        public LinkedList()
        {

        }

        // Copy constructor
        public LinkedList(LinkedList<T> other)
        {
            Append(other);
        }

        // Return an enumerator for the list
        public IEnumerator GetEnumerator()
        {
            return new LLEnum<T>(head);
        }

        // Copies the data in the list to an array object
        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            else if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            else if (array.Length < count)
                throw new ArgumentException("length");

            foreach(T item in this)
                array.SetValue(item, index++);
        }

        /// <summary>
        /// Returns a new list that contains the items of both lists
        /// </summary>
        /// <param name="self">First list</param>
        /// <param name="other">Second list</param>
        /// <returns>New list containing the elements from both lists</returns>
        public static LinkedList<T> operator +(LinkedList<T> self, LinkedList<T> other)
        {
            return new LinkedList<T>(self).Append(other);
        }

        /// <summary>
        /// Returns the item at the specified index
        /// </summary>
        /// <param name="index">Index of access</param>
        /// <returns>Item at index</returns>
        public T Get(int index)
        {
            if(index >= Count || index < 0)
                throw new IndexOutOfRangeException();

            return GoTo(head, index).item;
        }

        /// <summary>
        /// Sets the item of the node at the specified index
        /// </summary>
        /// <param name="index">Index to change</param>
        /// <param name="item">New item</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void Set(int index, T item)
        {
            if(index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            GoTo(head, index).item = item;
        }

        /// <summary>
        /// Adds the item to the end of the list
        /// </summary>
        /// <param name="item">Item to append</param>
        public void Append(T item)
        {
            if (head == null)
                head = new LinkedListNode<T>(item, null);
            else
                GoTo(head, Count - 1).next = new LinkedListNode<T>(item, null);
            count++;
        }

        /// <summary>
        /// Appends the items from the other list
        /// </summary>
        /// <param name="other">The list to add</param>
        /// <returns>Reference to this list</returns>
        public LinkedList<T> Append(LinkedList<T> other)
        {
            if (head == null)
            {
                LinkedListNode<T> oth = other.head;
                head = new LinkedListNode<T>(oth.item, null);
                LinkedListNode<T> cur = head;
                oth = oth.next;
                count++;

                while(oth != null)
                {
                    cur.next = new LinkedListNode<T>(oth.item, null);
                    cur = cur.next;
                    oth = oth.next;
                    count++;
                }

                //AppendRecurse(null, other.head);
            }
            else
            {
                LinkedListNode<T> oth = other.head;
                LinkedListNode<T> cur = GoTo(head, count - 1);

                while (oth != null)
                {
                    cur.next = new LinkedListNode<T>(oth.item, null);
                    cur = cur.next;
                    oth = oth.next;
                    count++;
                }
            }
            return this;
        }

        /// <summary>
        /// Adds an item to the list at the speified index
        /// </summary>
        /// <param name="index">Index to add the itme at</param>
        /// <param name="item">The item to add</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void Insert(int index, T item)
        {
            if(index == 0)
            {
                LinkedListNode<T> n = new LinkedListNode<T>(item, head);
                head = n;
            }
            else
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException();
                
                LinkedListNode<T> cur = GoTo(head, index - 1);
                cur.next = new LinkedListNode<T>(item, cur.next);
            }
            count++;
        }

        /// <summary>
        /// Removes the first item in the list and returns it
        /// </summary>
        /// <returns>The first item in the list</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public T TakeFirst()
        {
            if(count == 0)
                throw new IndexOutOfRangeException();
            else
            {
                T item = head.item;
                head = head.next;
                count--;
                return item;
            }
        }

        /// <summary>
        /// Removes and returns the item at the specified index
        /// </summary>
        /// <param name="index">Index of the item to take</param>
        /// <returns>The item at the index</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public T TakeAt(int index)
        {
            if(index > count)
                throw new IndexOutOfRangeException();
            else
            {
                if (index == 0)
                    return TakeFirst();
                else
                {
                    LinkedListNode<T> ind = GoTo(head, index - 1);
                    T item = ind.next.item;
                    count--;
                    ind.next = ind.next.next;
                    return item;
                }
            }
        }

        /// <summary>
        /// Removes the first instance of the item from the list
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if the item was removed</returns>
        public bool Remove(T item)
        {
            if (head == null)
                return false;
            else
            {
                LinkedListNode<T> cur = head;

                if(cur.next.item.Equals(item))
                {
                    count--;
                    head = cur.next;
                    cur = cur.next;
                    return true;
                }
                else
                {
                    while (cur.next != null)
                    {
                        if (cur.next.item.Equals(item))
                        {
                            cur.next = cur.next.next;
                            count--;
                            return true;
                        }
                        cur = cur.next;
                    }
                }
                return false;
                //return RemoveRecurse(item, head);
            }
        }

        /// <summary>
        /// Removes the item at the given index
        /// </summary>
        /// <param name="index">Index of item to remove</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();
            else if (index == 0)
            {
                if(count == 1)
                    head = null;
                else
                    head = head.next;
            }
            else
            {
                LinkedListNode<T> cur = index == 1 ? head : GoTo(head, index - 1);

                if (cur.next.next == null)
                    cur.next = null;
                else
                    cur.next = cur.next.next;
            }
            count--;
        }

        /// <summary>
        /// Removes the items for the given range
        /// </summary>
        /// <param name="start">Index of first item to remove</param>
        /// <param name="length">Number of items in range</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void RemoveRange(int start, int length)
        {
            if(start + length < count)
            {
                if (start == 0)
                    head = GoTo(head, length);
                else
                    GoTo(head, start - 1).next = GoTo(head, start + length);
                count -= length;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Searches through list and removes all mathcing items
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void RemoveAll(T item)
        {
            while (head != null && head.item.Equals(item))
            {
                head = head.next;
                count--;
            }

            if(count > 0)
            {
                LinkedListNode<T> cur = head;
                while (cur.next != null)
                {
                    if (cur.next.item.Equals(item))
                    {
                        cur.next = cur.next.next;
                        count--;
                    }
                    cur = cur.next;
                }
            }
        }

        /// <summary>
        /// Checks if the list contains the item
        /// </summary>
        /// <param name="item">Item to check for</param>
        /// <returns>True if the item if found</returns>
        public bool Contains(T item)
        {
            LinkedListNode<T> cur = head;
            while(cur != null)
            {
                if (cur.item.Equals(item))
                    return true;
                else
                    cur = cur.next;
            }
            return false;

            //return ContainsRecurse(item, head);
        }

        /// <summary>
        /// Returns the index of the given item
        /// </summary>
        /// <param name="item">Item to get</param>
        /// <returns>Index of item in the list</returns>
        public int IndexOf(T item)
        {
            LinkedListNode<T> cur = head;
            int ind = 0;
            while (cur != null)
            {
                if (cur.item.Equals(item))
                    return ind;
                else
                    cur = cur.next;
                ind++;
            }
            return -1;

            //return IndexOfRecurse(item, head, 0);
        }

        /// <summary>
        /// Recursively moves down the chain of items
        /// </summary>
        /// <param name="cur">Current node</param>
        /// <param name="index">Remaining length</param>
        /// <returns>Node at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        private LinkedListNode<T> GoTo(LinkedListNode<T> cur, int index)
        {
            if (index == 0)
                return cur;
            else if (cur.next == null)
                throw new IndexOutOfRangeException();
            else if (index >= count)
                throw new ArgumentException("Index > Count");
            /*else
                return GoTo(cur.next, index - 1);*/
            else
            {
                try
                {
                    while (index-- > 0)
                        cur = cur.next;
                }
                catch (NullReferenceException e)
                {
                    Debug.Log("GoTo encountered NullReferenceException at " + (index + 1));
                    Debug.Log("List count: " + count);
                    throw e;
                }
                return cur;
            }
        }

        /// <summary>
        /// Get a string representation of the list
        /// </summary>
        /// <returns>String representation of the list</returns>
        public override string ToString()
        {
            string ret = "{ ";

            LinkedListNode<T> cur = head;
            while(cur != null)
            {
                ret += cur.item.ToString() + ", ";
                cur = cur.next;
            }
            return ret + " } Count: " + count;
        }
    }

    /// <summary>
    /// Enumerator for the LinkedList
    /// </summary>
    public class LLEnum<T> : IEnumerator
    {
        public LinkedListNode<T> currentNode = null;
        public LinkedListNode<T> head = null;
        private bool starting = true;

        public LLEnum(LinkedListNode<T> head)
        {
            currentNode = this.head = head;
            starting = true;
        }

        public bool MoveNext()
        {
            if(currentNode != null)
            {
                if (starting)
                    starting = false;
                else
                    currentNode = currentNode.next;
                return currentNode.next != null;
            }
            else
                return false;
        }

        public void Reset()
        {
            currentNode = head;
            starting = true;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return currentNode.item;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
