using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.LeetCode {
    #region 2020_05_12:最小栈

    public class MinStack {

        Stack<int> stackData;
        Stack<int> minStack;

        public MinStack() {
            stackData = new Stack<int>();
            minStack = new Stack<int>();
        }

        public void Push(int x) {
            Console.WriteLine("push: " + x);
            //如果是空栈
            stackData.Push(x);
            if (minStack.Count == 0) {
                minStack.Push(x);
            }
            else {
                int minElement = minStack.Peek();
                if (minElement >= x) {//为什么等于x也得加入呢，这是因为我们在Pop时，如果有多个重复的最小元素，但是我们只Push了一个，那GetMin得到的值显然是不对的
                    minStack.Push(x);
                }
            }
        }

        public void Pop() {
            if (stackData.Count == 0) {
                Console.WriteLine("Stack is Empty!");
                return;
            }

            int popEle = stackData.Pop();
            Console.WriteLine("Pop: " +popEle);
            if (minStack.Peek() == popEle) {
                minStack.Pop();
            }
        }

        public int Top() {
            return stackData.Peek();
        }

        public int GetMin() {
            return minStack.Peek();
        }
    }

    /*
        这个是错误解答：这个解答和需求不符，因为在需求中栈中元素的位置是不变的，
        我这儿就直接没插进来一个就全部重新从大到小排序
    */
    public class MinStack_FalseAnswer {

        Stack<int> stack;
        public MinStack_FalseAnswer() {
            stack = new Stack<int>();
        }

        public void Push(int x) {
            Console.WriteLine("push: "+x);
            //如果是空栈
            if (stack.Count == 0) {
                stack.Push(x);
            }
            else {
            //非空得判断若干情况
                int topElement = stack.Peek();
                //如果插入元素小于栈顶元素就直接插入
                if (x <= topElement) {
                    stack.Push(x);
                }else {
                    //如果插入元素大于栈顶元素就需要对栈中的元素进行操作
                    Stack<int> tempStack = new Stack<int>();
                    tempStack.Push(topElement);

                    //把之前的顺序用另一个栈暂时保存起来
                    while (stack.Count>0&&stack.Peek()<x) {
                        int tempPreStackTop = stack.Pop();
                        tempStack.Push(tempPreStackTop);
                    }

                    stack.Push(x);
                    //找到插入位置之后再把原来的栈中的元素再转移过去
                    while (tempStack.Count > 0) {
                        int tempShiftStackTop = tempStack.Pop();
                        stack.Push(tempShiftStackTop);
                    }
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
