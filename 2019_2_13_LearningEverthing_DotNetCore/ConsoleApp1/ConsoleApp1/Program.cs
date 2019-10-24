using System;

namespace ConsoleApp1
{
    class Object1 { }
    class Object2 { }

    class Program
    {
        static void Main(string[] args)
        {
            Object1 obj1 = new Object1();
            Object2 obj2 = new Object2();
            Object1 obj3 = new Object1();

            Console.WriteLine("obj1's hashCode: "+obj1.GetHashCode());
            Console.WriteLine("obj2's hashCode: "+obj2.GetHashCode());
            Console.WriteLine("obj3's hashCode: "+obj3.GetHashCode());
        } 
    }
}
