using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkit.Tools.MemoryManagement
{
    public interface IObjectPool<T>
    {
        /// <summary>
        /// Add the specified number of items to the pool
        /// </summary>
        /// <param name="count"></param>
        public void Populate(int count);

        /// <summary>
        /// Get a instance of the type from the pool
        /// </summary>
        /// <returns></returns>
        public T Pop();

        // Returns an item to the object pool
        public void Push(T item);
    }
}
