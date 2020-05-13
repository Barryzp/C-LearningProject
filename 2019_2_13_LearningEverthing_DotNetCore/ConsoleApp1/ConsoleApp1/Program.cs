using System;
using ConsoleApp1.LeetCode;


namespace ConsoleApp1 {
    class Program {
        static void Main(string[] args) {
            MinStack minStack = new MinStack();
            minStack.Push(-2);
            minStack.Push(0);
            minStack.Push(-3);
            Console.WriteLine("GetMin: "+ minStack.GetMin());
            minStack.Pop();
            Console.WriteLine("Top: " + minStack.Top());
            Console.WriteLine("GetMin: " + minStack.GetMin());
        }
    }
}
