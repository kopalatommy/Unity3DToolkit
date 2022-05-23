using System;
using Toolkit.DataStructures;

namespace Toolkit.Tools.MemoryManagement
{
    public class ObjectPool<T> : Stack<T>, IObjectPool<T> where T : class
    {
        public ObjectPool()
        {

        }

        public ObjectPool(int initialCount)
        {
            Populate(initialCount);
        }

        public void Populate(int count)
        {
            while(count-- > 0)
            {
                // Create instance of generic type
                Push((T)Activator.CreateInstance(typeof(T)));
            }
        }

        public override T Pop()
        {
            if(Count > 0)
            {
                return base.Pop();
            }
            else
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
        }

        public override void Push(T item)
        {
            base.Push(item);
        }
    }
}
