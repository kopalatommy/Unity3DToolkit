using System;
using System.Collections;
using System.Collections.Generic;

namespace Toolkit.DataStructures
{
    [Serializable]
    public class DoublyLinkedList<T> : ICollection, IEnumerable, IEnumerable<T>, IListExtented<T>
    {
        /// <summary>
        /// Node class for the doubly linked list. Keeps track of the item and 
        /// the surrounding nodes
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        public class Node
        {
            public T item;

            public Node Next
            {
                get
                {
                    return next;
                }
                set
                {
                    if(value != null && value.prev != this)
                    {
                        value.prev = this;
                    }
                    next = value;
                }
            }
            protected Node next = null;

            public Node Prev
            {
                get
                {
                    return prev;
                }
                set
                {
                    if(value != null && value.next != this)
                    {
                        value.next = this;
                    }
                    prev = value;
                }
            }
            protected Node prev;

            /*public Node next
            {
                get { return n; }
                set 
                {
                    if(value != null && value.prev != this)
                    {
                        value.prev = this;
                    }
                    n = value;
                }
            }

            public Node prev
            {
                get { return p; }
                set
                {
                    if (value != null && value.next != this)
                    {
                        value.next = this;
                    }
                    p = value;
                }
            }

            protected Node p;
            protected Node n;*/

            /*public Node next = null;
            public Node prev = null;*/

            public Node(T item, Node prev, Node next)
            {
                this.item = item;
                this.Prev = prev;
                this.Next = next;
            }
        }

        protected Node head = null;
        protected Node tail = null;
        protected int count = 0;
        //protected DoublyLinkedListEnumurator<T> enumerator = new DoublyLinkedListEnumurator<T>(null);

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

        public bool IsReadOnly => throw new NotImplementedException();

        public DoublyLinkedList()
        {

        }

        public DoublyLinkedList(DoublyLinkedList<T> other)
        {
            AppendBack(other);
        }

        public IEnumerator GetEnumerator()
        {
            return new DoublyLinkedListEnumurator<T>(head);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new DoublyLinkedListEnumurator<T>(head);
            //return enumerator as IEnumerator<T>;
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            else if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            else if (array.Length < count)
            {
                throw new ArgumentException("length");
            }

            foreach (T item in this)
            {
                array.SetValue(item, index++);
            }
        }

        public static IListExtented<T> operator +(DoublyLinkedList<T> self, IListExtented<T> other)
        {
            IListExtented<T> toRet = new DoublyLinkedList<T>(self);
            toRet.Add(other);
            return toRet;
        }

        /// <summary>
        /// Returns the item at the specified index
        /// </summary>
        /// <param name="index">Index of access</param>
        /// <returns>Item at index</returns>
        public T Get(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (index > count / 2)
            {
                return GoToReverse(tail, count - index - 1).item;
            }
            else
            {
                return GoToForward(head, index).item;
            }
        }

        /// <summary>
        /// Sets the item of the node at the specified index
        /// </summary>
        /// <param name="index">Index to change</param>
        /// <param name="item">New item</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void Set(int index, T item)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index > count / 2)
            {
                GoToReverse(tail, count - index - 1).item = item;
            }
            else
            {
                GoToForward(head, index).item = item;
            }
        }

        public virtual void Add(T item)
        {
            AppendFront(item);
        }

        public virtual void AppendFront(T item)
        {
            if (head == null)
            {
                head = tail = new Node(item, null, null);
            }
            else
            {
                head.Prev = new Node(item, null, head);
                head = head.Prev;
            }
            count++;
        }

        public virtual void Add(IEnumerable<T> other)
        {
            /*UnityEngine.Debug.Log("Doubly linked list add IEnumerable");
            UnityEngine.Debug.Log("Self is null: " + (this == null));
            UnityEngine.Debug.Log("Other is null: " + (other == null));
            UnityEngine.Debug.Log("Other enum is null: " + (other.GetEnumerator() == null));
            foreach (T item in other)
            {
                UnityEngine.Debug.Log(item);
                tail = tail.Next = new Node(item, tail, null);
                count++;
            }*/
            AppendBack(other);
        }

        public virtual void AppendBack(T item)
        {
            if (head == null)
            {
                head = tail = new Node(item, null, null);
            }
            else
            {
                tail.Next = new Node(item, tail, null);
                tail = tail.Next;
            }
            count++;
        }

        public virtual void AppendBack(IEnumerable<T> other)
        {
            UnityEngine.Debug.Log("This type: " + GetType());
            foreach (T item in other)
            {
                UnityEngine.Debug.Log(item);
                tail = tail.Next = new Node(item, tail, null);
                count++;
            }
        }

        /*public DoublyLinkedList<T> AppendBack(IEnumerable<T> other)
        {
            UnityEngine.Debug.Log(other.GetType());
            Add(other);
            return this;
        }*/

        public virtual void Insert(int index, T item)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("index");
            }

            if(index == 0)
            {
                AppendFront(item);
            }
            else if(index == count)
            {
                AppendBack(item);
            }
            else
            {
                Node cur;
                if(index < count / 2)
                {
                    cur = GoToForward(head, index - 1);
                }
                else
                {
                    cur = GoToReverse(tail, count - index);
                }
                Node n = new Node(item, cur, cur.Next);
                cur.Next = cur.Next.Prev = n;
                count++;
            }
        }

        public virtual void Insert(int index, IEnumerable<T> other, int length)
        {
            if (index == 0)
            {
                Add(other);
            }
            else if (index == count)
            {
                AppendBack(other);
            }
            else
            {
                Node cur;
                if (index < count / 2)
                {
                    cur = GoToForward(head, index - 1);
                }
                else
                {
                    cur = GoToReverse(tail, count - index - 1);
                }
                foreach(T item in other)
                {
                    cur = cur.Next = cur.Next.Prev = new Node(item, cur, cur.Next);
                    count++;
                }
            }
        }

        public T TakeFirst()
        {
            if (count == 0)
            {
                throw new IndexOutOfRangeException("Trying to take from empty list");
            }
            else
            {
                T item = head.item;
                head = head.Next;

                if (count == 1)
                {
                    tail = null;
                }

                count--;
                return item;
            }
        }

        public T TakeAt(int index)
        {
            if (index > count)
            {
                throw new IndexOutOfRangeException("Index out of bounds");
            }
            else
            {
                if (index == 0)
                {
                    return TakeFirst();
                }
                else if(index + 1 == count)
                {
                    return TakeLast();
                }
                else
                {
                    Node ind = index < count / 2 ? GoToForward(head, index - 1) : GoToReverse(tail, count - index);
                    T item = ind.Next.item;
                    count--;
                    ind.Next.Next.Prev = ind;
                    ind.Next = ind.Next.Next;
                    return item;
                }
            }
        }

        public T TakeLast()
        {
            if(count == 0)
            {
                throw new Exception("Index out of bounds");
            }
            else
            {
                T item = tail.item;

                if (count == 1)
                {
                    tail = head = null;
                }
                else
                {
                    tail.Prev = null;
                }
                return item;
            }
        }

        public bool Remove(T item)
        {
            if (count > 0)
            {
                Node cur = head;

                while(cur != null)
                {
                    if(cur.item.Equals(item))
                    {
                        if (cur.Prev != null)
                        {
                            cur.Prev.Next = cur.Next;
                            // If the next node is null, then set head to the current node
                            if (cur.Prev.Next != null)
                            {
                                cur.Prev.Next.Prev = cur.Prev;
                            }
                            else
                            {
                                tail = cur;
                            }
                        }
                        else
                        {
                            head = cur.Next;
                            if (head == null)
                            {
                                tail = null;
                            }
                        }
                        count--;
                        return true;
                    }
                    cur = cur.Next;
                }
            }
            return false;
        }

        public void RemoveFirst()
        {
            if(head == null)
            {
                throw new IndexOutOfRangeException("Index out of bounds");
            }
            else if(count == 1)
            {
                head = tail = null;
                count = 0;
            }
            else
            {
                head = head.Next;
                head.Prev = null;
                count--;
            }
        }

        public void RemoveLast()
        {
            if (tail == null)
            {
                throw new IndexOutOfRangeException("Index out of bounds");
            }
            else if (count == 1)
            {
                head = tail = null;
                count = 0;
            }
            else
            {
                tail = tail.Prev;
                tail.Next = null;
                count--;
            }
        }

        public void RemoveAt(int index)
        {
            if (index > count)
            {
                throw new IndexOutOfRangeException("Index out of bounds");
            }
            else
            {
                if (index == 0)
                {
                    RemoveFirst();
                }    
                else if (index + 1 == count)
                {
                    RemoveLast();
                }
                else
                {
                    Node ind = index < count / 2 ? GoToForward(head, index - 1) : GoToReverse(tail, count - index);
                    count--;
                    ind.Next.Next.Prev = ind;
                    ind.Next = ind.Next.Next;
                }
            }
        }

        public void RemoveRange(int start, int length)
        {
            if (start + length < count)
            {
                Node s = GoToForward(head, start - 1);
                s.Next = GoToReverse(tail, count - start - length - 1);
                if (s.Next == null)
                {
                    tail = s;
                }
                count -= length;
            }
            else
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
        }

        public void RemoveAll(T item)
        {
            if (count == 0)
            {
                throw new Exception("Index out of bounds");
            }
            else
            {
                Node cur = head;

                while (cur != null)
                {
                    if (cur.item.Equals(item))
                    {
                        if (cur.Prev != null)
                        {
                            cur.Prev.Next = cur;
                            // If the next node is null, then set head to the current node
                            if (cur.Prev.Next != null)
                            {
                                cur.Prev.Next.Prev = cur;
                            }
                            else
                            {
                                tail = cur;
                                count--;
                                break;
                            }
                        }
                        else
                        {
                            head = cur.Next;
                            if (head == null)
                            {
                                tail = null;
                                count--;
                                break;
                            }
                        }
                        count--;
                    }
                    cur = cur.Next;
                }
            }
        }

        public bool Contains(T item)
        {
            foreach(T it in this)
            {
                if (it.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public int FirstIndexOf(T item)
        {
            Node cur = head;
            int ind = 0;
            while(cur != null)
            {
                if(cur.item.Equals(item))
                {
                    return ind;
                }
                else
                {
                    ind++;
                    cur = cur.Next;
                }
            }
            return -1;
        }

        public int LastIndexOf(T item)
        {
            Node cur = tail;
            int ind = 0;
            while (cur != null)
            {
                if (cur.item.Equals(item))
                {
                    return ind;
                }
                else
                {
                    ind++;
                    cur = cur.Prev;
                }
            }
            return -1;
        }

        public int IndexOf(T item)
        {
            Node cur = head;
            for(int i = 0; i < count; i++)
            {
                if(cur.item.Equals(item))
                {
                    return i;
                }
                cur = cur.Next;
            }

            return -1;
        }

        protected Node GoToForward(Node cur, int index)
        {
            if (index == 0)
            {
                return cur;
            }
            else if (cur.Next == null)
            {
                throw new Exception("Index out of bounds LinkedList::GoTo");
            }
            else
            {
                while (index-- > 0)
                {
                    cur = cur.Next;
                }
                return cur;
            }
        }

        protected Node GoToReverse(Node cur, int index)
        {
            if (index == 0)
            {
                return cur;
            }
            else if (cur.Prev == null)
            {
                throw new Exception("Index out of bounds DoublyLinkedList::GoToReverse");
            }
            else
            {
                while (index-- > 0)
                {
                    cur = cur.Prev;
                }
                return cur;
            }
        }

        public override string ToString()
        {
            string ret = "{ ";

            Node cur = head;
            while (cur != null)
            {
                ret += cur.item.ToString();
                if (cur.Next != null)
                {
                    ret += ", ";
                }
                cur = cur.Next;
            }
            return ret + " } Count: " + count + "\nH: " + head.item;
        }

        public T[] ToArray()
        {
            T[] array = new T[count];
            Node cur = head;
            int index = 0;
            while(cur != null)
            {
                array[index++] = cur.item;
                cur = cur.Next;
            }
            return array;
        }

        public void Clear()
        {
            head = tail = null;
            count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            else if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            else if (array.Length < count)
            {
                throw new ArgumentException("length");
            }

            foreach (T item in this)
            {
                array.SetValue(item, arrayIndex++);
            }
        }
    }

    public class DoublyLinkedListEnumurator<T> : IEnumerator, IEnumerator<T>
    {
        public DoublyLinkedList<T>.Node currentNode = null;
        public DoublyLinkedList<T>.Node head = null;
        protected bool starting = true;

        public DoublyLinkedListEnumurator(DoublyLinkedList<T>.Node head)
        {
            currentNode = this.head = head;
            starting = true;
        }

        public void UpdateHead(DoublyLinkedList<T>.Node nHead)
        {
            currentNode = head = nHead;
        }

        public bool MoveNext()
        {
            if (currentNode != null)
            {
                if (starting)
                {
                    starting = false;
                    return true;
                }
                else if(currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            currentNode = head;
            starting = true;
        }

        public void Dispose()
        {
            currentNode = null;
            head = null;
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

    public class DoublyLinkedListReverseEnumurator<T> : IEnumerator
    {
        public DoublyLinkedList<T>.Node currentNode = null;
        public DoublyLinkedList<T>.Node tail = null;
        protected bool starting = true;

        public DoublyLinkedListReverseEnumurator(DoublyLinkedList<T>.Node tail)
        {
            currentNode = this.tail = tail;
            starting = true;
        }

        public void UpdateTail(DoublyLinkedList<T>.Node nTail)
        {
            tail = nTail;
        }

        public bool MoveNext()
        {
            if (currentNode != null)
            {
                if (starting)
                    starting = false;
                else
                    currentNode = currentNode.Prev;
                return currentNode.Prev != null;
            }
            else
                return false;
        }

        public void Reset()
        {
            currentNode = tail;
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
