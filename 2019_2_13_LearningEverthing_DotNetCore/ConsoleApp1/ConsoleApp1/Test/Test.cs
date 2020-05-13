using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Test {
    class Object1 { }
    class Object2 {
        private int aName;

    }

    abstract class Hey {
        public abstract void hello();
    }

    class Hello : Hey {
        public override void hello() {

        }
    }

    class Shape {
        public Shape() {
            Console.WriteLine("Shape constructor!");
        }

        public virtual void cacuSize() {
            Console.WriteLine("Caculate area in shape.");
        }
    }

    class Rect : Shape {
        private int width;
        private int height;
        public Rect(int w, int h) {
            Console.WriteLine("Rect constructor!");

            width = w;
            height = h;
        }

        public override void cacuSize() {
            Console.WriteLine("are: " + width * height);
        }
    }

    class Circle : Shape {
        private int radius;
        public Circle(int r) {
            Console.WriteLine("Circle constructor!");
            radius = r;
        }

        public override void cacuSize() {
            Console.WriteLine("area: " + Math.PI * radius * radius);
        }
    }

    public class Test {
        static void testVirtual() {
            Shape circle = new Circle(10);
            Shape rect = new Rect(10, 20);
            circle.cacuSize();
            rect.cacuSize();
        }

        static int addArray(int[] arr, int index) {
            if (index == 0) return arr[index];

            return arr[index--] + addArray(arr, index);
        }

        static void testTypeConvert() {
            Circle a = new Circle(2);
            //Rect b = new Rect(10, 23);
            //Shape c = new Shape();
            //a = (Circle)c;
            //a.cacuSize();
            //c.cacuSize();
        }

        static void logArray(int[] array) {
            foreach (var item in array) {
                Console.Write(item.ToString() + ',');
            }
            Console.WriteLine();
        }

        static ref int getAArrayRef(int[] array) {
            return ref array[1];
        }

        static void testOut(out char button) {
            button = 'a';
        }

        static void actualParam(int param1, int param2) {
        }

        static string converAIntegerToBinaryStr(int convertVal) {
            string returnVal = "";
            int size = 32;
            int mask = 1 << size - 2;
            for (int i = 1; i < size; i++) {
                char bit = ((mask & convertVal) != 0) ? '1' : '0';
                Console.Write(bit);
                mask >>= 1;
            }

            return returnVal;
        }
    }
}
