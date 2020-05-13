using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.LeetCode {
    #region 2020_05_12:最小栈

    public class MinStack {

        Stack<int> stack;

        public MinStack() {
            stack = new Stack<int>();
        }

        public void Push(int element) {
            //如果是空栈
            if (stack.Count == 0) {
                stack.Push(element);
            }
            else {
            //非空得判断若干情况
                int topElement = stack.Peek();
                if (element < topElement) {
                    stack.Push(element);
                }else {

                }
            }
        }

        public void Pop() {
            if (stack.Count == 0) {
                Console.WriteLine("Stack is Empty!");
                return;
            }

            stack.Pop();
        }

        public int Top() {
            return stack.Peek();
        }

        public int GetMin() {
            return Top();
        }
    }

    #endregion
}
