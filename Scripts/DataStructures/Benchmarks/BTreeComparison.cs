using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Toolkit.DataStructures.Comparisions
{
    public class BTreeComparison : MonoBehaviour
    {
        private void Start()
        {
            System.Random rand = new System.Random();

            ArrayList<ArrayList<int>> testData = new ArrayList<ArrayList<int>>();
            for (int i = 0; i < 10; i++)
            {
                DoublyLinkedList<int> source = new DoublyLinkedList<int>();
                for (int j = 0; j < 1000000; j++)
                    source.AppendBack(j);

                ArrayList<int> test = new ArrayList<int>();
                for (int j = testData.Count - 1; j >= 0; j--)
                    test.Add(source.TakeAt(rand.Next(0, j)));

                testData.Add(test);
            }

            TestInsertions_V1(testData);
            TestInsertions_V2(testData);

            /*var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            for (int i = 0; i < 1000; i++)
            {
                Console.Write(i);
            }

            watch.Stop();

            Debug.Log($"Execution Time: {watch.ElapsedMilliseconds} ms");*/
        }

        void TestInsertions_V1(ArrayList<ArrayList<int>> source)
        {
            long totalTime = 0;
            ArrayList<long> testTimes = new ArrayList<long>();
            foreach(ArrayList<int> test in source)
            {
                BTree<int> tree = new BTree<int>(3);

                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();

                foreach (int item in test)
                    tree.Insert(item);

                watch.Stop();

                totalTime += watch.ElapsedMilliseconds;
                testTimes.Add(watch.ElapsedMilliseconds);
            }

            string timeStr = string.Empty;
            foreach(long time in testTimes)
                timeStr += time.ToString() + " ";

            Debug.Log("Finished V1 test\nTotal time: " + totalTime + "\nTests: " + timeStr);
        }

        void TestInsertions_V2(ArrayList<ArrayList<int>> source)
        {
            long totalTime = 0;
            ArrayList<long> testTimes = new ArrayList<long>();
            foreach (ArrayList<int> test in source)
            {
                BTreeV2<int> tree = new BTreeV2<int>(3);

                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

                foreach (int item in test)
                    tree.Insert(item);


                watch.Stop();

                totalTime += watch.ElapsedMilliseconds;
                testTimes.Add(watch.ElapsedMilliseconds);
            }

            string timeStr = string.Empty;
            foreach (long time in testTimes)
                timeStr += time.ToString() + " ";

            Debug.Log("Finished V2 test\nTotal time: " + totalTime + "\nTests: " + timeStr);
        }
    }
}
