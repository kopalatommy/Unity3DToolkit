using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.DataStructures
{
    public class SkipListNode<T>
    {
        public SkipListNode<T> down = null;
        public SkipListNode<T> next = null;

        public int Key 
        {
            get { return key; }
            set { key = value; }
        }
        public T Value 
        { 
            get { return value; }
            set { this.value = value; }
        }
        public int Height { 
            get { return height; }
            set { height = value; }
        }

        private int key;
        private T value;
        private int height;

        public SkipListNode(int key, T value, SkipListNode<T> next, SkipListNode<T> down)
        {
            this.key = key;
            this.value = value;
            this.next = next;
            this.down = down;
        }
    }

    // Uses thw GetHashCode function as a key
    public class SkipList<T> : IEnumerable // : ICollection<T>
    {
        private int height = 1;
        SkipListNode<T> head = null;
        int count = 0;

        System.Random random = new System.Random();

        SkipListEnumerator<T> enumerator = null;

        public int Height { get { return height; } }

        public SkipListNode<T> Head { get { return head; } }

        public int Count { get { return count; } }

        public SkipList()
        {
            enumerator = new SkipListEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            enumerator.Reset();
            return enumerator;
        }

        public void Insert(T item)
        {
            int key = item.GetHashCode();
            if(head == null)
            {
                height = count = 1;
                head = new SkipListNode<T>(key, item, null, null);
                head.Height = height;
            }
            else
            {
                int height = DetermineNodeHeight();
                SkipListNode<T> cur = head;
  

                // Go to the new node's height
                while(cur.Height > height)
                {
                    cur = cur.down;
                }

                // This builds down, needs to keep track of the previously created node
                // to maintan the chain
                SkipListNode<T> created = null;
                while(true)
                {
                    // Get the node that is before the new nodes location
                    while (cur.next != null && cur.next.Key < key)
                    {
                        cur = cur.next;
                    }
                    cur.next = new SkipListNode<T>(key, item, cur.next, null);
                    cur.next.Height = cur.Height;
                    if (created == null)
                        created = cur.next;
                    else
                    {
                        created.down = cur.next;
                        created = created.down;
                    }
                    if (cur.down != null)
                        cur = cur.down;
                    else
                        break;
                }
                count++;
            }
        }

        protected int DetermineNodeHeight()
        {
            int maxNodeHeight = height + 1;
            int nodeHeight = 1;
            while (random.NextDouble() < 0.5 && nodeHeight < maxNodeHeight)
            {
                nodeHeight++;
            }

            if (nodeHeight > height)
            {
                height = nodeHeight;
                if(head.Height < height)
                {
                    SkipListNode<T> n = new SkipListNode<T>(head.Key, head.Value, null, head);
                    n.Height = height;
                    head = n;
                }
            }

            return nodeHeight;
        }

        public SkipListNode<T> Search(int key)
        {
            SkipListNode<T> cur = head;

            while(cur != null)
            {
                while (cur.next != null && cur.next.Key <= key)
                {
                    cur = cur.next;
                    if (cur.Key == key)
                        return cur;
                }

                if (cur.down != null)
                    cur = cur.down;
                else
                    break;
            }

            return cur;
        }

        public bool Contains(T item)
        {
            SkipListNode<T> node = Search(item.GetHashCode());

            return node != null && node.Value.Equals(item);
        }

        public bool Remove(T item)
        {
            int key = item.GetHashCode();
            //if(head.Value.Equals(item))
            if(head.Key == key)
            {
                if(count == 1)
                    head = null;
                else
                {
                    // Get the next item in the list
                    SkipListNode<T> cur = head;
                    while (cur.down != null)
                        cur = cur.down;
                    T repl = cur.next.Value;
                    key = cur.next.Key;

                    cur = head;
                    while(cur != null)
                    {
                        cur.Value = repl;
                        cur.Key = key;
                        // If the next item contains the item, then 
                        if (cur.next != null && cur.next.Value.Equals(item))
                            cur.next = cur.next.next;
                        cur = cur.down;
                    }
                }
                count--;
                return true;
            }
            else
            {
                bool removedItem = false;
                SkipListNode<T> cur = head;

                while (cur != null)
                {
                    while(cur.next != null)
                    {
                        // If the item doesn't match, then move down the layer
                        if (cur.next.Key < key)
                        {
                            cur = cur.next;
                        }
                        // Check if the next item matches the one to remove
                        else if (cur.next.Key == key)
                        {
                            cur.next = cur.next.next;
                            removedItem = true;
                            break;
                        }
                        else
                            break;
                    }
                    cur = cur.down;
                }
                if(removedItem)
                    count--;
                return removedItem;
            }
        }

        public void Clear()
        {
            head = null;
            count = 0;
            height = 1;
        }

        public override string ToString()
        {
            string ret = "{ ";

            SkipListNode<T> cur = head;
            while (cur.down != null)
                cur = cur.down;

            while (cur != null)
            {
                ret += cur.Value.ToString();
                if (cur.next != null)
                    ret += ", ";
                cur = cur.next;
            }
            return ret + " } Count: " + count;
        }

        public string DumpList()
        {
            string ret = "Height: " + height + "\n";

            SkipListNode<T> cur = head;

            while(cur != null)
            {
                ret += cur.Height + " | ";
                SkipListNode<T> lev = cur;
                while (lev != null)
                {
                    ret += lev.Value + ", ";
                    lev = lev.next;
                }
                ret = ret.Remove(ret.Length - 2, 2) + "|\n";
                cur = cur.down;
            }

            int h = 0;
            cur = head;
            while(cur != null)
            {
                h++;
                cur = cur.down;
            }
            ret += "H: " + h + " Count: " + count;

            return ret;
        }

        public string DumpTop()
        {
            SkipListNode<T> cur = head;
            string ret = ">";

            while(cur != null)
            {
                while (cur.next != null)
                {
                    ret += " " + cur.Value + " ";
                    cur = cur.next;
                    if (cur.next != null)
                        ret += '>';
                }
                cur = cur.down;
                if(cur != null)
                    ret += "* " + cur.Value + " ";
            }

            return ret;
        }
    }

    public class SkipListEnumerator<T> : IEnumerator
    {
        public T Current
        {
            get
            {
                return cur.Value;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return cur.Value;
            }
        }

        SkipList<T> list = null;
        SkipListNode<T> cur = null;

        public SkipListEnumerator(SkipList<T> list)
        {
            this.list = list;
            Reset();
        }

        public bool MoveNext()
        {
            if(cur == null)
            {
                cur = list.Head;
                while (cur != null && cur.down != null)
                    cur = cur.down;
                return true;
            }
            else if(cur.next != null)
            {
                cur = cur.next;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            cur = null;
        }
    }
}
