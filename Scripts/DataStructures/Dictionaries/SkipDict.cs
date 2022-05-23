using System.Collections;
using System.Collections.Generic;

namespace Toolkit.DataStructures
{
    public class SkipDictNode<TKey, TValue>
    {
        public TKey Key
        {
            get { return key; }
            set { key = value; }
        }
        public TValue Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public SkipDictNode<TKey, TValue> down = null;
        public SkipDictNode<TKey, TValue> next = null;

        public TKey key;
        public TValue value;

        int height;

        public SkipDictNode(TKey key, TValue value, SkipDictNode<TKey,TValue> next, SkipDictNode<TKey, TValue> down)
        {
            this.key = key;
            this.value = value;
            this.next = next;
            this.down = down;
        }

        public override string ToString()
        {
            /*return "SkipDictNode<" + typeof(TKey).ToString() + ", " +
                typeof(TValue).ToString() + ">(" + key.ToString() + ", " +
                    value.ToString() + ")";*/
            return "(" + key.ToString() + ", " +
                    value.ToString() + ")"; ;
        }
    }

    public class SkipDict<TKey, TValue> : IEnumerable
    {
        public int Count { get { return count; } }

        public SkipList<TKey> Keys
        {
            get { return keys; }
        }

        public SkipDictNode<TKey, TValue> Head
        {
            get { return head; }
        }

        // Number of layers in the dictionary
        private int height = 1;
        // Head node of the list
        private SkipDictNode<TKey, TValue> head = null;
        // Number of items in the dictionary
        private int count = 0;
        // Used when generaing new layers
        System.Random random = new System.Random();
        // Used to sort the items in the dictionary
        IComparer<TKey> keyComparer = null;
        // Enumerator for the class
        SkipDictEnumerator<TKey, TValue> enumerator = null;
        // Collection of all keys in the dictionary
        SkipList<TKey> keys = new SkipList<TKey>();

        // Contructor, requries the keyComparer value to properly function
        public SkipDict(IComparer<TKey> comparer)
        {
            enumerator = new SkipDictEnumerator<TKey, TValue>(this);

            keyComparer = comparer;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            enumerator.Reset();
            return enumerator;
        }

        // Adds a key value pair to the dictionary. Does not allow for duplicate keys
        public void Insert(TKey key, TValue item)
        {
            if(head == null)
            {
                height = count = 1;
                head = new SkipDictNode<TKey, TValue>(key, item, null, null);
                head.Height = height;
                keys.Insert(key);
            }
            else
            {
                // Get the height of the node
                int nodeHeight = DetermineNodeHeight();
                // Get a reference of the head to move to the location of the node
                SkipDictNode<TKey, TValue> cur = head;

                // Drop to the new node's height
                while (cur.Height > height)
                    cur = cur.down;

                // This builds down, needs to keep track of the previously created node
                // to maintan the chain
                SkipDictNode<TKey, TValue> created = null;
                while(true)
                {
                    // Move horizontally until the new location is found
                    while (cur.next != null && keyComparer.Compare(cur.next.key, key) <= 0)
                        cur = cur.next;

                    // Insert a new node after the obtained node
                    cur.next = new SkipDictNode<TKey, TValue>(key, item, cur.next, null);
                    cur.next.Height = cur.Height;
                    // Hold a refernce to the created node to update the down value
                    if (created == null)
                        created = cur.next;
                    else
                    {
                        // Update the previously created node
                        created.down = cur.next;
                        created = created.down;
                    }
                    if (cur.down != null)
                        cur = cur.down;
                    else
                        break;
                }
                keys.Insert(key);
                count++;
            }
        }

        private int DetermineNodeHeight()
        {
            // Max layer the node can reach
            int maxNodeHeight = height + 1;
            // Current height to place the node
            int nodeHeight = 1;

            // Calculate the new height by generating a random double, if less than 1/2 add a
            // layer
            while (random.NextDouble() < 0.5 && nodeHeight < maxNodeHeight)
                nodeHeight++;

            // If a new layer is being added, then the height of the head needs to be
            // increased
            if(nodeHeight > height)
            {
                // Update the dictionaries height value
                height = nodeHeight;

                SkipDictNode<TKey, TValue> nHead = 
                    new SkipDictNode<TKey, TValue>(head.Key, head.value, null, head);
                head = nHead;
            }

            return nodeHeight;
        }

        // Searches the dictionary for a node containing the key
        public bool Contains(TKey key)
        {
            SkipDictNode<TKey, TValue> found = Search(key);

            return found != null && found.Key.Equals(key);
        }

        public SkipDictNode<TKey, TValue> ContainsTest(TKey key)
        {
            SkipDictNode<TKey, TValue> found = Search(key);

            return found;
        }

        public TValue Get(TKey key)
        {
            SkipDictNode<TKey, TValue> found = Search(key);

            if(found != null && found.Key.Equals(key))
                return found.Value;
            else
                return default(TValue);
        }

        public bool TryGet(TKey key, out TValue value)
        {
            SkipDictNode<TKey, TValue> found = Search(key);

            if(found != null && found.Key.Equals(key))
            {
                value = found.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }

        // Searches the dictionary for the provided key
        private SkipDictNode<TKey, TValue> Search(TKey key)
        {
            SkipDictNode<TKey, TValue> cur = head;

            // Check the head first b/c the loop checks the 
            // next node's key

            int c = keyComparer.Compare(head.key, key);

            while (cur != null)
            {
                while (cur.next != null)
                {
                    c = keyComparer.Compare(cur.next.key, key);
                    if (c < 0)
                        cur = cur.next;
                    else if (c == 0)
                        return cur.next;
                    else
                        break;
                }

                if (cur.down != null)
                    cur = cur.down;
                else
                    break;
            }

            return cur;
        }

        public bool Remove(TKey key)
        {
            if(keyComparer.Compare(head.key, key) == 0)
            {
                if(count == 1)
                {
                    head = null;
                }
                else
                {
                    SkipDictNode<TKey, TValue> cur = head;
                    while (cur.down != null)
                        cur = cur.down;
                    TValue repl = cur.next.Value;
                    key = cur.next.Key;

                    cur = head;
                    while (cur != null)
                    {
                        cur.Value = repl;
                        cur.Key = key;
                        // If the next item contains the item, then remove it
                        if (cur.next != null && keyComparer.Compare(cur.next.key, key) == 0)
                            cur.next = cur.next.next;
                        cur = cur.down;
                    }
                }
                count--;
                keys.Remove(key);
                return true;
            }
            else
            {
                bool removedItem = false;
                SkipDictNode<TKey, TValue> cur = head;

                while (cur != null)
                {
                    while (cur.next != null)
                    {
                        int c = keyComparer.Compare(cur.next.key, key);
                        // If the item doesn't match, then move down the layer
                        if (c < 0)
                        {
                            cur = cur.next;
                        }
                        // Check if the next item matches the one to remove
                        else if (c == 0)
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
                if (removedItem)
                {
                    count--;
                    keys.Remove(key);
                }
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

            SkipDictNode<TKey, TValue> cur = head;
            while (cur.down != null)
                cur = cur.down;

            while (cur != null)
            {
                ret += '(' + cur.Key.ToString() + " : " + cur.Value.ToString() + ')';
                if (cur.next != null)
                    ret += ", ";
                cur = cur.next;
            }
            return ret + " } Count: " + count;
        }
    }

    public class SkipDictEnumerator<TKey, TValue> : IEnumerator
    {
        public TValue Current
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

        SkipDict<TKey, TValue> dict = null;
        SkipDictNode<TKey, TValue> cur = null;

        public SkipDictEnumerator(SkipDict<TKey, TValue> dict)
        {
            this.dict = dict;
            Reset();
        }

        public bool MoveNext()
        {
            if (cur == null)
            {
                cur = dict.Head;
                while (cur != null && cur.down != null)
                    cur = cur.down;
                return true;
            }
            else if (cur.next != null)
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
