using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_10_04_EventAndGitHub
{
    class Program
    {
        public static void JustForTest()
        {
            Console.WriteLine("For testing.");
        }

        public static void EventTest()
        {
            Teacher teacher = new Teacher();
            Woman mary = new Woman("Mary");
            Man barry = new Man("Barry");

            //To subscribe event that same params.
            teacher.SayingWords += barry.Saying;
            teacher.SayingWords += mary.Saying;

            teacher.SpeakYourName("just for test");
        }

        static void Main(string[] args)
        {
            EventTest();
            //List<int> numberSet = new List<int>();
            //for (var index = 0; index < 13; index++)
            //    numberSet.Add(index);
            //foreach (var item in numberSet)

            //string str = new string(new char[] {'a','b','c'});
            //Console.WriteLine("end!"+str);
        }
    }
}
