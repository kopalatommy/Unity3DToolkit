using System;
using Toolkit.DataStructures;
using Toolkit.Test;
using UnityEngine;

namespace Toolkit.DataStructures.Tests
{

    public class StackTester : TesterBase
    {
        public StackTester()
        {
            testName = "Stack test";
            testType = typeof(StackTester);
        }

        [RunTest(true)]
        private bool PushTest1()
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < 10; i++)
                stack.Push(i);

            if(stack.Count != 10)
            {
                Debug.Log("Stack count != 10: " + stack.Count);
            }

            for (int i = 9; i >= 0; i--)
                if(stack.Pop() != i)
                {
                    Debug.Log("Stack returned unexpected value at check: " + i);
                    return false;
                }

            return stack.Count == 0;
        }

        // Tests the fail case when the stack is empty
        [RunTest(true)]
        private bool PushTest2()
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < 10; i++)
                stack.Push(i);

            if (stack.Count != 10)
            {
                Debug.Log("Stack count != 10: " + stack.Count);
            }

            for (int i = 9; i >= 0; i--)
                if (stack.Pop() != i)
                {
                    Debug.Log("Stack returned unexpected value at check: " + i);
                    return false;
                }

            try
            {
                stack.Pop();
                return false;
            }
            catch(System.Exception)
            {
                return true;
            }
        }

        // Peek test
        [RunTest(true)]
        private bool PeekTest1()
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < 10; i++)
                stack.Push(i);

            if (stack.Count != 10)
            {
                Debug.Log("Stack count != 10: " + stack.Count);
            }

            return stack.Peek() == 9;
        }

        // Tests the fail case when the stack is empty
        [RunTest(true)]
        private bool PeekTest2()
        {
            Stack<int> stack = new Stack<int>();

            try
            {
                stack.Peek();
                return false;
            }
            catch (System.Exception)
            {
                return true;
            }
        }

        [RunTest(true)]
        private bool MaxCountTest1()
        {
            Stack<int> stack = new Stack<int>();
            stack.MaxCount = 10;

            for (int i = 0; i < 1000; i++)
                stack.Push(i);

            if (stack.Count != 10)
            {
                Debug.Log("MaxCountTest1 failed: Count(" + stack.Count + ") > MaxCount(" + stack.MaxCount + ")");
                return false;
            }

            for (int i = 9; i >= 0; i--)
                if (stack.Pop() != i)
                {
                    Debug.Log("Stack returned unexpected value at check: " + i);
                    return false;
                }

            return stack.Count == 0;
        }
    }
}
