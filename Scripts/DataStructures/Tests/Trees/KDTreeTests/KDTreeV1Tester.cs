using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{
    public class KDTreeV1Tester : TesterBase
    {
        public KDTreeV1Tester()
        {
            testName = "KD Tree V1";
            testType = typeof(KDTreeV1Tester);
        }

        // Make sure the list count increases as intended when adding items
        [RunTest(true)]
        public bool AddPointTest1()
        {
            KDTree<int> tree = new KDTree<int>();

            for(int i = 0; i < 10; i++)
            {
                tree.AddPoint(Vector3.one * i, i, false);
            }
            tree.Rebuild();

            if(tree.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log("KD Tree V1 add point test 1 failed. Resulting count != 10: " + tree.Count);
                return false;
            }
        }

        // Make sure the list count increases as intended when adding items
        [RunTest(true)]
        public bool AddPointsTest1()
        {
            KDTree<int> tree = new KDTree<int>();

            Vector3[] points = new Vector3[10];
            int[] values = new int[10];
            for (int i = 0; i < 10; i++)
            {
                points[i] = Vector3.one * i;
                values[i] = i;
            }
            tree.AddPoints(points, values);

            if (tree.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log("KD Tree V1 add points test 1 failed. Resulting count != 10: " + tree.Count);
                return false;
            }
        }

        // Make sure the list count increases as intended when adding items
        [RunTest(true)]
        public bool SetPointsTest1()
        {
            KDTree<int> tree = new KDTree<int>();

            Vector3[] points = new Vector3[10];
            int[] values = new int[10];
            for (int i = 0; i < 10; i++)
            {
                points[i] = Vector3.one * i;
                values[i] = i;
            }
            tree.SetPoints(points, values);

            if (tree.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log("KD Tree V1 set points test 1 failed. Resulting count != 10: " + tree.Count);
                return false;
            }
        }

        // Make sure the list count increases as intended when adding items
        [RunTest(true)]
        public bool SetPointsTest2()
        {
            KDTree<int> tree = new KDTree<int>();

            Vector3[] points = new Vector3[10];
            int[] values = new int[10];
            for (int i = 0; i < 10; i++)
            {
                points[i] = Vector3.one * i;
                values[i] = i;
            }
            tree.AddPoints(points, values);
            tree.SetPoints(points, values);

            if (tree.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log("KD Tree V1 set points test 2 failed. Resulting count != 10: " + tree.Count);
                return false;
            }
        }

        [RunTest(true)]
        public bool GetNearestPoint1()
        {
            KDTree<int> tree = new KDTree<int>();

            for (int i = 0; i < 10; i++)
            {
                tree.AddPoint(Vector3.one * i, i, false);
            }
            tree.Rebuild();

            ArrayList<int> closest = new ArrayList<int>();
            for(int i = 0; i < 10; i++)
            {
                tree.ClosestPoint(Vector3.one * i, closest);
                if (closest.Count == 0)
                {
                    Debug.Log(testName + " GetNearestPoint1 failed to return any items");
                    return false;
                }
                else if (closest[0] != i)
                {
                    Debug.Log(closest);
                    Debug.Log(testName + " GetNearestPoint1 returned the wrong item for iter: " + i + ": " + closest[0]);
                    return false;
                }
                closest.Clear();
            }

            if (tree.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log(testName + " GetNearestPoint1 final count != 10: " + tree.Count);
                return false;
            }
        }

        [RunTest(true)]
        public bool GetNearestPoint2()
        {
            KDTree<int> tree = new KDTree<int>();

            for (int i = 9; i >= 0; i--)
            {
                tree.AddPoint(Vector3.one * i, i, false);
            }
            tree.Rebuild();

            ArrayList<int> closest = new ArrayList<int>();
            for (int i = 0; i < 10; i++)
            {
                tree.ClosestPoint(Vector3.one * i, closest);
                if (closest.Count == 0)
                {
                    Debug.Log(testName + " GetNearestPoint2 failed to return any items");
                    return false;
                }
                else if (closest[0] != i)
                {
                    Debug.Log(closest);
                    Debug.Log(testName + " GetNearestPoint2 returned the wrong item for iter: " + i + ": " + closest[0]);
                    return false;
                }
                closest.Clear();
            }

            if (tree.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log(testName + " GetNearestPoint2 final count != 10: " + tree.Count);
                return false;
            }
        }

        [RunTest(true)]
        public bool GetNearestPoint3()
        {
            KDTree<int> tree = new KDTree<int>();

            for (int i = 0; i < 10; i++)
            {
                tree.AddPoint(Vector3.one * i, i, false);
            }
            tree.Rebuild();

            ArrayList<int> closest = new ArrayList<int>();
            for (int i = 9; i >= 0; i--)
            {
                tree.ClosestPoint(Vector3.one * i, closest);
                if (closest.Count == 0)
                {
                    Debug.Log(testName + " GetNearestPoint3 failed to return any items");
                    return false;
                }
                else if (closest[0] != i)
                {
                    Debug.Log(closest);
                    Debug.Log(testName + " GetNearestPoint3 returned the wrong item for iter: " + i + ": " + closest[0]);
                    return false;
                }
                closest.Clear();
            }

            if (tree.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log(testName + " GetNearestPoint3 final count != 10: " + tree.Count);
                return false;
            }
        }

        [RunTest(true)]
        public bool GetNearestPoint4()
        {
            KDTree<int> tree = new KDTree<int>();

            for (int i = 9; i >= 0; i--)
            {
                tree.AddPoint(Vector3.one * i, i, false);
            }
            tree.Rebuild();

            ArrayList<int> closest = new ArrayList<int>();
            for (int i = 9; i >= 0; i--)
            {
                tree.ClosestPoint(Vector3.one * i, closest);
                if (closest.Count == 0)
                {
                    Debug.Log(testName + " GetNearestPoint4 failed to return any items");
                    return false;
                }
                else if (closest[0] != i)
                {
                    Debug.Log(closest);
                    Debug.Log(testName + " GetNearestPoint4 returned the wrong item for iter: " + i + ": " + closest[0]);
                    return false;
                }
                closest.Clear();
            }

            if (tree.Count == 10)
            {
                return true;
            }
            else
            {
                Debug.Log(testName + " GetNearestPoint4 final count != 10: " + tree.Count);
                return false;
            }
        }

        // Random order create
        [RunTest(true)]
        public bool GetNearestPoint5()
        {
            System.Random rand = new System.Random();
            for (int t = 0; t < 10; t++)
            {
                KDTree<int> tree = new KDTree<int>();

                DoublyLinkedList<Pair<Vector3, int>> sources = new DoublyLinkedList<Pair<Vector3, int>>();

                for (int i = 9; i >= 0; i--)
                {
                    sources.Add(new Pair<Vector3, int>(Vector3.one * i, i));
                }

                for (int i = 9; i >= 0; i--)
                {
                    Pair<Vector3, int> cur = sources.TakeAt(rand.Next(0, i));
                    tree.AddPoint(cur.Key, cur.Value, false);
                }
                tree.Rebuild();

                ArrayList<int> closest = new ArrayList<int>();
                for (int i = 9; i >= 0; i--)
                {
                    tree.ClosestPoint(Vector3.one * i, closest);
                    if (closest.Count == 0)
                    {
                        Debug.Log(testName + " GetNearestPoint5 failed to return any items");
                        return false;
                    }
                    else if (closest[0] != i)
                    {
                        Debug.Log(closest);
                        Debug.Log(testName + " GetNearestPoint5 returned the wrong item for iter: " + i + ": " + closest[0]);
                        return false;
                    }
                    closest.Clear();
                }

                if (tree.Count != 10)
                {
                    Debug.Log(testName + " GetNearestPoint5 final count != 10: " + tree.Count);
                    return false;
                }
            }
            return true;
        }

        [RunTest(true)]
        public bool GetIntervalTest1()
        {
            if(verbose)
            {
                Debug.Log("Starting GetIntervalTest1 for " + testName);
            }

            KDTree<int> tree = new KDTree<int>();

            tree.AddPoint(Vector3.zero, 0, false);
            tree.AddPoint(Vector3.one, 1, false);
            tree.Rebuild();

            ArrayList<int> result = new ArrayList<int>();
            tree.Interval(Vector3.zero, Vector3.one, result);

            if(result.Count == 2)
            {
                return true;
            }
            else
            {
                Debug.Log(testName + " GetIntervalTest1 failed to get all items in interval");
                return false;
            }
        }

        [RunTest(true)]
        public bool GetIntervalTest2()
        {
            if (verbose)
            {
                Debug.Log("Starting GetIntervalTest2 for " + testName);
            }

            KDTree<int> tree = new KDTree<int>();

            /*tree.AddPoint(Vector3.zero, 0, false);
            tree.AddPoint(Vector3.one, 1, false);
            tree.AddPoint(Vector3.one * 2, 2, false);
            tree.Rebuild();*/

            Vector3[] points = { Vector3.one * 0, Vector3.one * 1, Vector3.one * 2 };
            int[] vals = { 0, 1, 2};

            tree.SetPoints(points, vals);

            ArrayList<int> result = new ArrayList<int>();
            tree.Interval(Vector3.zero, Vector3.one, result);

            if (result.Count == 2)
            {
                if(!result.Contains(0))
                {
                    Debug.Log(testName + " GetIntervalTest2 failed to get 0");
                    return false;
                }
                else if(!result.Contains(1))
                {
                    Debug.Log(testName + " GetIntervalTest2 failed to get 1");
                    return false;
                }

                return true;
            }
            else
            {
                Debug.Log(testName + " GetIntervalTest2 failed to get 2 items. Count: " + result.Count);
                return false;
            }
        }

        [RunTest(true)]
        public bool GetKNearestTest1()
        {
            if (verbose)
            {
                Debug.Log("Starting GetKNearestTest1 for " + testName);
            }

            KDTree<int> tree = new KDTree<int>();

            for(int i = 0; i < 10; i++)
            {
                tree.AddPoint(Vector3.one * i, i, i == 9);
            }

            ArrayList<int> result = new ArrayList<int>();
            tree.KNearest(Vector3.one * 5, 3, result);

            if(result.Count != 3)
            {
                Debug.Log(testName + " GetKNearestTest1 result length != 3. Count: " + result.Count);
                return false;
            }

            /*for(int i = 0; i < 10; i++)
            {
                //Debug.Log(i + " = " + Vector3.Distance(Vector3.one*5, Vector3.one*i));
                Debug.Log(i + " = " + (Vector3.SqrMagnitude((Vector3.one * 5) - (Vector3.one * i))));
            }*/

            if(!result.Contains(5) || !result.Contains(6) || !result.Contains(4))
            {
                Debug.Log(testName + " GetKNearestTest1 contains wrong items. Count: " + result);
                return false;
            }

            return true;
        }

        [RunTest(true)]
        public bool RadiusTest1()
        {
            if (verbose)
            {
                Debug.Log("Starting RadiusTest1 for " + testName);
            }

            KDTree<int> tree = new KDTree<int>();

            for (int i = 0; i < 10; i++)
            {
                tree.AddPoint(Vector3.one * i, i, i == 9);
            }

            ArrayList<int> result = new ArrayList<int>();
            tree.Radius(Vector3.one * 5, 2, result);

            if (!result.Contains(5) || !result.Contains(6) || !result.Contains(4))
            {
                Debug.Log(testName + " GetKNearestTest1 contains wrong items. Count: " + result);
                return false;
            }

            return result.Count == 3;
        }
    }
}
