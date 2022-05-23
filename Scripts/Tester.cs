using System.Collections;
using UnityEngine;
using Toolkit.DataStructures.Tests;

public class Tester : MonoBehaviour
{
    class TestStruct
    {
        public int val;

        public TestStruct next;
    }

    // ToDo, there is a way to search the entire application for classes
    // with a given attribute. That would be much better than creating the
    // instances here.
    private void Start()
    {
        Application.targetFrameRate = 100;

        Debug.Log("Starting all tests");

        /*LinkedListTester llTester = new LinkedListTester();
        llTester.RunTests();*/

        /*DoublyLinkedListTester dllTester = new DoublyLinkedListTester();
        dllTester.RunTests(false, 60000);*/

        /*CircularListTester circularListTester = new CircularListTester();
        circularListTester.RunTests();*/

        /*ArrayListTester arrayListTester = new ArrayListTester();
        arrayListTester.RunTests(false, 60000);*/

        /*SkipListTester skipListTester = new SkipListTester();
        skipListTester.RunTests(false, 5000);*/

        /*SkipDictTester skipDictTester = new SkipDictTester();
        skipDictTester.RunTests(false, 60000);*/

        /*BinaryTreeTester binaryTreeTester = new BinaryTreeTester();
        binaryTreeTester.RunTests(false, 60000);*/

        /*StackTester stackTester = new StackTester();
        stackTester.RunTests(false, 60000);*/

        /*BTreeTester bTreeTester = new BTreeTester();
        bTreeTester.RunTests(false, 60000);*/

        /*BTreeV2Tester bTreevV2Tester = new BTreeV2Tester();
        bTreevV2Tester.RunTests(false, 60000);*/

        /*RedBlackTreeTester rbTester = new RedBlackTreeTester();
        rbTester.RunTests(false, 60000);*/

        /*AVLTreeTester avlTester = new AVLTreeTester();
        avlTester.RunTests(false, 60000);*/

        /*MaxHeapTester maxHeapTester = new MaxHeapTester();
        maxHeapTester.RunTests(false, 60000);*/

        /*MinHeapTester minHeapTester = new MinHeapTester();
        minHeapTester.RunTests(false, 60000);*/

        OrderedListTester orderedListTester = new OrderedListTester();
        orderedListTester.RunTests(false, 60000);

        KDTreeV1Tester kDTreeV1Tester = new KDTreeV1Tester();
        kDTreeV1Tester.RunTests(false, 60000);
    }
}
