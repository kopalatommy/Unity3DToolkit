using System;
using System.Collections.Generic;

namespace Toolkit.DataStructures
{
    public class Queue<T> : DoublyLinkedList<T>
    {
        public T Dequeue()
        {
            return TakeFirst();
        }

        public T Peek()
        {
            return Get(0);
        }

        public void Enqueue(T item)
        {
            AppendBack(item);
        }
    }
}
