using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2019_2_13_Generic_01.Attributes;
using _2019_2_13_Generic_01.Iterator;

namespace _2019_2_13_Generic_01
{
    public class Point<T>
    {
        public T x;
        public T y;
        public Point(T x,T y)
        {
            this.x = x;
            this.y = y;
        }
}

    class Program
    {
        static IEnumerable<string> GetEnumeratorTest()
        {
            yield return "begin:";
            for (int index = 0; index < 10; index++)
            {
                yield return index.ToString();
            }
            yield return "end.";
        }

        static void EnumeratorTest()
        {
            ItreatorTest itreatorTest = new ItreatorTest();
            foreach (string item in itreatorTest)
            {
                Console.WriteLine(item);
            }

            foreach (string item in GetEnumeratorTest())
            {
                Console.WriteLine(item);
            }
        }

        static void ExplictInterfaceTest()
        {
            var item = new WarmKiller();
            IKiller killer = item;
            IGentleman gentleman = item;
            killer.Kill();
            gentleman.Love();
        }

        static void testStr() {
            string str1 = "abc";
            string str2 = "abc";
            string str3 = new string(new char[] { 'a','b','c'});
            String str4 = "abc";
            Console.WriteLine(str1 == str2);
            Console.WriteLine(str1.Equals(str2));
            Console.WriteLine(str1 == str3);
            Console.WriteLine(str1 == str4);
        }

        static int[] twoSum(int[] nums,int target)
        {
            int index1 = -1, index2 = -1;
            for(int i = 0; i < nums.Length; i++)
            {
                index1 = nums[i];
                for (int j=i+1;j<nums.Length;j++)
                {
                    index2 = nums[j];
                    if((index1+index2) == target)
                    {
                        return new int[] {i,j};
                    }
                }
            }

            return new int[]{ -1,-1};
        }

        static void Main(string[] args)
        {
            int[] array = new int[] { 1, 2, 4, 3 };
            int []array2 = twoSum(array,5);
            for (int index=0;index<array2.Length;index++) {
                Console.WriteLine(array2[index]);
            }
        }
    }
}
