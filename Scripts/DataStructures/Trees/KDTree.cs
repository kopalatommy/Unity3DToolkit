using UnityEngine;
using Toolkit.Tools.MemoryManagement;
using System;

namespace Toolkit.DataStructures
{
    // KD trees provide a way of storing items based on its location
    public class KDTree<T>
    {
        public struct KDBounds
        {
            public Vector3 Size { get { return max - min; } }

            public Bounds Bounds
            {
                get
                {
                    return new Bounds((min + max) / 2, max - min);
                }
            }

            public Vector3 min, max;

            public KDBounds(Vector3 min, Vector3 max)
            {
                this.min = min;
                this.max = max;
            }

            public Vector3 ClosestPoint(Vector3 point)
            {
                if (point.x < min.x)
                {
                    point.x = min.x;
                }
                else if(point.x > max.x)
                {
                    point.x = max.x;
                }

                if (point.y < min.y)
                {
                    point.y = min.y;
                }
                else if (point.y > max.y)
                {
                    point.y = max.y;
                }

                if (point.z < min.z)
                {
                    point.z = min.z;
                }
                else if (point.z > max.z)
                {
                    point.z = max.z;
                }

                return point;
            }
        }

        public class Node
        {
            public int Count { get { return end - start; } }
            public bool IsLeaf { get { return partitionAxis == -1; } }

            public float partitionCoordinate;
            public int partitionAxis = -1;

            public Node negative;
            public Node positive;

            public int start;
            public int end;

            public KDBounds bounds;
        }

        #region Variables

        public int Count { get { return count; } }

        // Array of points that make up the kd tree
        ArrayList<Vector3> points;
        // Index array
        private int[] permutation;
        // Number of points in tree
        private int count;

        private int maxPointsPerLeaf;
        // Pool of nodes
        ObjectPool<Node> pool;

        // Root node of tree
        Node root = null;

        #endregion

        public KDTree(int maxPointsPerLeaf = 32, int initialPoolSize = 64)
        {
            count = 0;
            points = new ArrayList<Vector3>(count);
            permutation = new int[count];
            this.maxPointsPerLeaf = maxPointsPerLeaf;
            pool = new ObjectPool<Node>(64);
        }

        public void AddPoint(Vector3 point, bool build=true)
        {
            points.Add(point);
            Array.Resize(ref permutation, points.Count);
            
            if(build)
            {
                Rebuild();
            }
        }

        public void AddPoints(Vector3[] points, bool build = true)
        {
            this.points.Add(points);
            Array.Resize(ref permutation, this.points.Count);

            if (build)
            {
                Rebuild();
            }
        }

        public void SetPoints(Vector3[] points, bool build = true)
        {
            this.points.Clear();
            this.points.Add(points);
            Array.Resize(ref permutation, this.points.Count);

            if (build)
            {
                Rebuild();
            }
        }

        public void Rebuild()
        {
            for(int i = 0; i < count; i++)
            {
                permutation[i] = i;
            }

            BuildTree();
        }

        private void BuildTree()
        {
            root = pool.Pop();
            root.bounds = MakeBounds();
            root.start = 0;
            root.end = count;

            SplitNode(root);
        }

        KDBounds MakeBounds()
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            int even = count & -1;
            for(int i = 0; i < even; i += 2)
            {
                int j = i + 1;

                if(points[i].x > points[j].x)
                {
                    if(points[i].x > max.x)
                    {
                        max.x = points[i].x;
                    }
                    if(points[j].x < min.x)
                    {
                        min.x = points[j].x;
                    }
                }
                else
                {
                    if(points[i].x < min.x)
                    {
                        min.x = points[i].x;
                    }
                    if(points[j].x > max.x)
                    {
                        max.x = points[j].x;
                    }
                }

                if(points[i].y > points[j].y)
                {
                    if(points[i].y > max.y)
                    {
                        max.y = points[i].y;
                    }
                    if(points[j].y < min.y)
                    {
                        min.y = points[j].y;
                    }
                }
                else
                {
                    if(points[i].y < min.y)
                    {
                        min.y = points[i].y;
                    }
                    if(points[j].y > max.y)
                    {
                        max.y = points[j].y;
                    }
                }

                if(points[i].z > points[j].z)
                {
                    if(points[i].z > max.z)
                    {
                        max.z = points[i].z;
                    }
                    if(points[j].z < min.z)
                    {
                        min.z = points[j].z;
                    }
                }
                else
                {
                    if(points[i].z < min.z)
                    {
                        min.z = points[i].z;
                    }
                    if(points[j].z > max.z)
                    {
                        max.z = points[j].z;
                    }
                }
            }

            // Account for last element when only 1 item
            if(even != count)
            {
                if(min.x > points[even].x)
                {
                    min.x = points[even].x;
                }
                if(max.x < points[even].x)
                {
                    max.x = points[even].x;
                }

                if(min.y > points[even].y)
                {
                    min.y = points[even].y;
                }
                if(max.y < points[even].y)
                {
                    max.y = points[even].y;
                }

                if(min.z > points[even].z)
                {
                    min.z = points[even].z;
                }
                if(max.z < points[even].z)
                {
                    max.z = points[even].z;
                }
            }

            return new KDBounds(min, max);
        }

        private void SplitNode(Node node)
        {
            KDBounds bounds = node.bounds;
            Vector3 nodeBoundsSize = bounds.Size;

            int splitAxis = 0;
            float axisSize = nodeBoundsSize.x;

            if(axisSize < nodeBoundsSize.y)
            {
                splitAxis = 1;
                axisSize = nodeBoundsSize.y;
            }

            if(axisSize < nodeBoundsSize.z)
            {
                splitAxis = 2;
            }

            float boundsStart = bounds.min[splitAxis];
            float boundsEnd = bounds.max[splitAxis];

            float splitPivot = CalculatePivot(node.start, node.end, boundsStart, boundsEnd, splitAxis);

            node.partitionAxis = splitAxis;
            node.partitionCoordinate = splitPivot;

            int splittingIndex = Partition(node.start, node.end, splitPivot, splitAxis);

            Vector3 negMax = bounds.max;
            negMax[splitAxis] = splitPivot;

            Node negNode = pool.Pop();
            if(negNode == null)
            {
                Debug.Log("Neg node is null");
            }
            negNode.bounds = bounds;
            negNode.bounds.max = negMax;
            negNode.start = node.start;
            negNode.end = splittingIndex;
            node.negative = negNode;

            Vector3 posMin = bounds.min;
            posMin[splitAxis] = splitPivot;

            Node posNode = pool.Pop();
            posNode.bounds = bounds;
            posNode.bounds.min = posMin;
            posNode.start = splittingIndex;
            posNode.end = node.end;
            node.positive = posNode;

            if(negNode.Count != 0)
            {
                if(ContinueSplit(negNode))
                {
                    SplitNode(negNode);
                }
                if(ContinueSplit(posNode))
                {
                    SplitNode(posNode);
                }
            }
        }

        private float CalculatePivot(int start, int end, float boundsStart, float boundsEnd, int axis)
        {
            float midPoint = (boundsStart + boundsEnd) / 2;

            bool negative = false;
            bool positive = false;

            for(int i = start; i < end; i++)
            {
                if(points[permutation[i]][axis] < midPoint)
                {
                    negative = true;
                }
                else
                {
                    positive = true;
                }

                if(negative && positive)
                {
                    return midPoint;
                }
            }

            if(negative)
            {
                float negMax = float.MinValue;
                for (int i = start; i < end; i++)
                {
                    if(negMax < points[permutation[i]][axis])
                    {
                        negMax = points[permutation[i]][axis];
                    }
                }
                return negMax;
            }
            else
            {
                float posMin = float.MaxValue;
                for(int i = start; i < end; i++)
                {
                    if(posMin > points[permutation[i]][axis])
                    {
                        posMin = points[permutation[i]][axis];
                    }
                }
                return posMin;
            }
        }

        private int Partition(int start, int end, float partitionPivot, int axis)
        {
            int leftIndex = start - 1;
            int rightIndex = end;

            int temp;
            while (true)
            {
                do
                {
                    leftIndex++;
                }while(leftIndex < rightIndex && points[permutation[rightIndex]][axis] < partitionPivot);

                do
                {
                    rightIndex--;
                }
                while (leftIndex < rightIndex && points[permutation[rightIndex]][axis] >= partitionPivot);

                if(leftIndex < rightIndex)
                {
                    temp = permutation[leftIndex];
                    permutation[leftIndex] = permutation[rightIndex];
                    permutation[rightIndex] = temp;
                }
                else
                {
                    return leftIndex;
                }
            }
        }

        private bool ContinueSplit(Node node)
        {
            return node.Count > maxPointsPerLeaf;
        }

        internal class QueryNode : IComparable
        {
            public Node node;
            public Vector3 closestPoint;
            public float distance;

            public QueryNode()
            {

            }

            public QueryNode(Node node, Vector3 closest, float distance)
            {
                this.node = node;
                closestPoint = closest;
                this.distance = distance;
            }

            public void Populate(Node node, Vector3 closest, float distance)
            {
                this.node = node;
                closestPoint = closest;
                this.distance = distance;
            }

            public void Populate(Node node, Vector3 closestPoint)
            {
                this.node = node;
                this.closestPoint = closestPoint;
                this.distance = float.MaxValue;
            }

            public int CompareTo(QueryNode other)
            {
                if(other.distance == distance)
                {
                    return 0;
                }
                else if(distance < other.distance)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

            public int CompareTo(object obj)
            {
                if(obj is QueryNode)
                {
                    return CompareTo((QueryNode)obj);
                }
                else
                {
                    return -1;
                }
            }
        }

        private ObjectPool<QueryNode> queryPool;
        private MinHeap<QueryNode> queryMinHeap;
        private int queryCount;
        private int queryIndex;

        private int LeftToProcess
        {
            get
            {
                return queryCount - queryIndex;
            }
        }

        private void ResetQuery()
        {
            if(queryMinHeap == null)
            {
                queryMinHeap = new MinHeap<QueryNode>();
            }
            else
            {
                queryMinHeap.Clear();
            }
            if(queryPool == null)
            {
                queryPool = new ObjectPool<QueryNode>();
            }
            else
            {
                queryPool.Clear();
            }
        }

        public void ClosestPoint(Vector3 queryLoc, ArrayList<int> resultIndices, ArrayList<float> resultDistances = null)
        {
            ResetQuery();

            ArrayList<Vector3> searchPoints = points;
            int[] searchPermutation = permutation;

            if (searchPoints.Count == 0)
            {
                Debug.Log("Trying to search empty tree");
                return;
            }

            int smallestIndex = 0;
            float smallestSquaredRadius = float.PositiveInfinity;

            QueryNode qN = queryPool.Pop();
            qN.Populate(root, root.bounds.ClosestPoint(queryLoc), Vector3.SqrMagnitude(root.bounds.ClosestPoint(queryLoc) - queryLoc));
            queryMinHeap.Insert(qN);

            QueryNode queryNode = null;
            Node node = null;

            int partitionAxis;
            float partitionCoord;

            Vector3 closestPoint;

            while(queryMinHeap.Count > 0)
            {
                queryNode = queryMinHeap.TakeMin();

                if(queryNode.distance > smallestSquaredRadius)
                {
                    continue;
                }

                node = queryNode.node;

                if(!node.IsLeaf)
                {
                    partitionAxis = node.partitionAxis;
                    partitionCoord = node.partitionCoordinate;

                    closestPoint = queryNode.closestPoint;

                    if((closestPoint[partitionAxis] - partitionCoord) < 0)
                    {
                        qN = queryPool.Pop();
                        qN.Populate(node.negative, closestPoint, Vector3.SqrMagnitude(closestPoint - queryLoc));
                        queryMinHeap.Insert(qN);
                    
                        closestPoint[partitionAxis] = partitionCoord;
                        if(node.positive.Count != 0)
                        {
                            qN = queryPool.Pop();
                            qN.Populate(node.positive, closestPoint, Vector3.SqrMagnitude(closestPoint - queryLoc));
                            queryMinHeap.Insert(qN);
                        }
                    }
                    else
                    {
                        qN = queryPool.Pop();
                        qN.Populate(node.positive, closestPoint, Vector3.SqrMagnitude(closestPoint - queryLoc));
                        queryMinHeap.Insert(qN);

                        closestPoint[partitionAxis] = partitionCoord;
                        if (node.positive.Count != 0)
                        {
                            qN = queryPool.Pop();
                            qN.Populate(node.negative, closestPoint, Vector3.SqrMagnitude(closestPoint - queryLoc));
                            queryMinHeap.Insert(qN);
                        }
                    }
                }
                else
                {
                    float dist;
                    int index;
                    for(int i = node.start; i < node.end; i++)
                    {
                        index = permutation[i];
                        dist = Vector3.SqrMagnitude(points[index] - queryLoc);
                        if(dist < smallestSquaredRadius)
                        {
                            smallestSquaredRadius = dist;
                            smallestIndex = index;
                        }
                    }
                }
            }
            resultIndices.Add(smallestIndex);
            if(resultDistances != null)
            {
                resultDistances.Add(smallestSquaredRadius);
            }
        }

        public void Interval(Vector3 min, Vector3 max, ArrayList<int> resultIndices)
        {
            ResetQuery();

            ArrayList<Vector3> searchPoints = points;
            int[] searchPermutation = permutation;

            Queue<QueryNode> queryQueue = new Queue<QueryNode>();

            QueryNode qN = queryPool.Pop();
            qN.Populate(root, root.bounds.ClosestPoint((min + max) / 2));
            queryQueue.AppendBack(qN);

            Node node;
            QueryNode queryNode;
            while(queryQueue.Count > 0)
            {
                queryNode = queryQueue.TakeFirst();
                node = queryNode.node;

                if(!node.IsLeaf)
                {
                    int partitionIndex = node.partitionAxis;
                    float partitionCoord = node.partitionCoordinate;

                    Vector3 closestPoint = queryNode.closestPoint;

                    if((closestPoint[partitionIndex] - partitionCoord) < 0)
                    {
                        qN = queryPool.Pop();
                        qN.Populate(node.negative, closestPoint);
                        queryQueue.AppendBack(qN);

                        closestPoint[partitionIndex] = partitionCoord;

                        if(node.positive.Count != 0 &&
                            closestPoint[partitionIndex] <= max[partitionIndex])
                        {
                            qN = queryPool.Pop();
                            qN.Populate(node.positive, closestPoint);
                            queryQueue.AppendBack(qN);
                        }
                    }
                    else
                    {
                        qN = queryPool.Pop();
                        qN.Populate(node.positive, closestPoint);
                        queryQueue.AppendBack(qN);

                        closestPoint[partitionIndex] = partitionCoord;

                        if(node.negative.Count != 0 &&
                            closestPoint[partitionIndex] >= min[partitionIndex])
                        {
                            qN = queryPool.Pop();
                            qN.Populate(node.negative, closestPoint);
                            queryQueue.AppendBack(qN);
                        }
                    }
                }
                else
                {
                    if(node.bounds.min[0] >= min[0] &&
                        node.bounds.min[1] >= min[1] &&
                        node.bounds.min[2] >= min[2] &&
                        
                        node.bounds.max[0] <= max[0] &&
                        node.bounds.max[1] <= max[1] &&
                        node.bounds.max[2] <= max[2])
                    {
                        for(int i = node.start; i < node.end; i++)
                        {
                            resultIndices.Add(permutation[i]);
                        }
                    }
                    else
                    {
                        for(int i = node.start; i < node.end; i++)
                        {
                            int index = permutation[i];
                            Vector3 v = points[index];

                            if(v[0] >= min[0] &&
                                v[1] >= min[1] &&
                                v[2] >= min[2] &&
                                
                                v[0] <= max[0] &&
                                v[1] <= max[1] &&
                                v[2] <= max[2])
                            {
                                resultIndices.Add(index);
                            }
                        }
                    }
                }
            }
        }

        public void KNearest(Vector3 queryLoc, int k, IListExtented<int> resultIndices, IListExtented<float> resultDistances = null)
        {
            if(k > count)
            {
                k = count;
            }

            OrderedList<int,float> smallestIndices = new OrderedList<int,float>();
            Queue<QueryNode> queue = new Queue<QueryNode>();

            ResetQuery();

            ArrayList<Vector3> searchPoints = points;
            int[] searchPermutation = permutation;

            float BSSR = float.PositiveInfinity;

            QueryNode qN = queryPool.Pop();
            qN.Populate(root, root.bounds.ClosestPoint(queryLoc), Vector3.Distance(queryLoc, root.bounds.ClosestPoint(queryLoc)));
            queue.AppendBack(qN);

            int partitionAxis;
            float partitionCoord;
            Vector3 closestPoint;

            QueryNode cur;
            Node node;
            while(queue.Count > 0)
            {
                cur = queue.TakeFirst();

                // Location is farther away than the max in the k
                // nearest collection
                if(cur.distance > BSSR)
                {
                    continue;
                }

                node = cur.node;

                if(!node.IsLeaf)
                {
                    partitionAxis = node.partitionAxis;
                    partitionCoord = node.partitionCoordinate;
                    closestPoint = cur.closestPoint;

                    if ((closestPoint[partitionAxis] - partitionCoord) < 0)
                    {
                        qN = queryPool.Pop();
                        qN.Populate(node.negative, node.negative.bounds.ClosestPoint(queryLoc), Vector3.Distance(queryLoc, node.negative.bounds.ClosestPoint(queryLoc)));
                        queue.AppendBack(qN);

                        closestPoint[partitionAxis] = partitionCoord;

                        if (node.positive.Count != 0)
                        {
                            qN = queryPool.Pop();
                            qN.Populate(node.positive, node.positive.bounds.ClosestPoint(queryLoc), Vector3.Distance(queryLoc, node.positive.bounds.ClosestPoint(queryLoc)));
                            queue.AppendBack(qN);
                        }
                    }
                    else
                    {
                        qN = queryPool.Pop();
                        qN.Populate(node.positive, node.positive.bounds.ClosestPoint(queryLoc), Vector3.Distance(queryLoc, node.positive.bounds.ClosestPoint(queryLoc)));
                        queue.AppendBack(qN);

                        closestPoint[partitionAxis] = partitionCoord;

                        if (node.positive.Count != 0)
                        {
                            qN = queryPool.Pop();
                            qN.Populate(node.negative, node.negative.bounds.ClosestPoint(queryLoc), Vector3.Distance(queryLoc, node.negative.bounds.ClosestPoint(queryLoc)));
                            queue.AppendBack(qN);
                        }
                    }
                }
                else
                {
                    float sqrDist;
                    for(int i = node.start; i < node.end; i++)
                    {
                        int index = permutation[i];
                        sqrDist = Vector3.SqrMagnitude(points[index] - queryLoc);

                        if(sqrDist <= BSSR)
                        {
                            smallestIndices.AppendBack(index, sqrDist);

                            if(smallestIndices.Count >= k)
                            {
                                BSSR = smallestIndices[k - 1].Value;
                            }
                        }
                    }
                }
            }

            for(int i = 0; i < k; i++)
            {
                Pair<int, float> temp = smallestIndices[i];
                resultIndices.Add(temp.Key);
                if(resultDistances != null)
                {
                    resultDistances.Add(temp.Value);
                }
            }
        }
    }
}
