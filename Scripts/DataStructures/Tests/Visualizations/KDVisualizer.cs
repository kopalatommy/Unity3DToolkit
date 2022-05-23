using System.Collections;
using Toolkit.DataStructures;
using UnityEngine;

namespace Toolkit.DataStructures.Visualizations
{
    public class KDVisualizer : MonoBehaviour
    {
        //Vector3[] pointCloud;
        KDTree<Vector3> tree;

        GameObject[] cubes;
        Vector3[] locs;

        public GameObject cube = null;
        private void Awake()
        {
            cubes = new GameObject[1000];
            locs = new Vector3[1000];
            for(int i = 0; i < 1000; i++)
            {
                locs[i] = new Vector3(
                    (1f + Random.value * 20f),
                    (1f + Random.value * 20f),
                    (1f + Random.value * 20f)
                    );

                cubes[i] = Instantiate(cube, locs[i], Quaternion.identity);
            }

            tree = new KDTree<Vector3>();
            tree.AddPoints(locs);

            /*pointCloud = new Vector3[20000];

            for (int i = 0; i < pointCloud.Length; i++)
            {
                pointCloud[i] = new Vector3(
                    (1f + Random.value * 0.25f),
                    (1f + Random.value * 0.25f),
                    (1f + Random.value * 0.25f)
                );
            }

            tree = new KDTree<Vector3>();
            tree.AddPoints(pointCloud);*/
        }

        Vector3 LorenzStep(Vector3 p)
        {
            float a = 28f;
            float b = 10f;
            float c = 8 / 3f;

            return new Vector3(
                b * (p.y - p.x),
                p.x * (a - p.z) - p.y,
                p.x * p.y - c * p.z
            );
        }

        void Update()
        {
            for (int i = 0; i < 1000; i++)
            {
                locs[i] += LorenzStep(locs[i]) * Time.deltaTime * 0.1f;
                cubes[i].transform.position = locs[i];
            }

            tree.Rebuild();

            DoublyLinkedList<int> list = new DoublyLinkedList<int>();
            tree.KNearest(Vector3.one * 3, 10, list);
        }

        IEnumerator Visualize()
        {
            while(true)
            {

            }
        }

        /*private void OnDrawGizmos()
        {
            if (tree == null)
            {
                return;
            }

            Vector3 size = 0.2f * Vector3.one;

            Gizmos.color = Color.blue;

            for (int i = 0; i < pointCloud.Length; i++)
            {
                Gizmos.DrawCube(pointCloud[i], size);
            }

            DoublyLinkedList<int> resultIndices = new DoublyLinkedList<int>();

            Color markColor = Color.red;
            markColor.a = 0.5f;
            Gizmos.color = markColor;

            tree.KNearest(Vector3.one, 15, resultIndices);

            *//*switch (QueryType)
            {

                case QType.ClosestPoint:
                    {

                        query.ClosestPoint(tree, transform.position, resultIndices);
                    }
                    break;

                case QType.KNearest:
                    {

                        query.KNearest(tree, transform.position, K, resultIndices);
                    }
                    break;

                case QType.Radius:
                    {

                        query.Radius(tree, transform.position, Radius, resultIndices);

                        Gizmos.DrawWireSphere(transform.position, Radius);
                    }
                    break;

                case QType.Interval:
                    {

                        query.Interval(tree, transform.position - IntervalSize / 2f, transform.position + IntervalSize / 2f, resultIndices);

                        Gizmos.DrawWireCube(transform.position, IntervalSize);
                    }
                    break;

                default:
                    break;
            }*//*

            Debug.Log("Drawing points");
            for (int i = 0; i < resultIndices.Count; i++)
            {
                Gizmos.DrawCube(pointCloud[resultIndices[i]], 2f * size);
            }

            *//*Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position, 4f * size);*/

            /*if (DrawQueryNodes)
            {
                query.DrawLastQuery();
            }*//*
        }*/
    }
}
