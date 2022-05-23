using System.Collections;
using UnityEngine;

namespace Toolkit.Test
{
    public class Benchmark : MonoBehaviour
    {
        private void Start()
        {
            BenchmarkLinkedList();
        }

        public void BenchmarkLinkedList()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            System.Collections.Generic.LinkedList<int> list = new System.Collections.Generic.LinkedList<int>();

            for(int i = 0; i < 10000; i++)
                list.AddLast(i);

            Debug.Log($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }    
    }
}
