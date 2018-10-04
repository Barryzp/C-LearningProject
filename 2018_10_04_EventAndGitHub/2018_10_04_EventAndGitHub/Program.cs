using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_10_04_EventAndGitHub
{
    class Program
    {
        public static void EventTest()
        {
            Teacher teacher = new Teacher();
            Woman mary = new Woman("Mary");
            Man barry = new Man("Barry");

            //To subscribe event that same params.
            teacher.SayingWords += barry.Saying;
            teacher.SayingWords += mary.Saying;

            teacher.SpeakYourName();
        }

        static void Main(string[] args)
        {
            EventTest();
            Console.WriteLine("end!");
        }
    }
}
