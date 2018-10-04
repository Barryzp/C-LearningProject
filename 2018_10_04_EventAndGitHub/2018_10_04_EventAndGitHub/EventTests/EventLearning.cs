using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_10_04_EventAndGitHub
{
    public class Teacher
    {
        //claim a delegate.
        public delegate void PrintWordsDelegate();
        //claim a event.
        public event PrintWordsDelegate SayingWords;

        public void SpeakYourName()
        {
            Console.WriteLine("Guys,start to saying!");
            SayingWords.Invoke();
        }
    }

    public class Person
    {
        private string name;
        public Person(string name)
        {
            this.name = name;
        }

        public virtual void Saying() { Console.WriteLine("My name is: " + name); }
    }

    public class Woman : Person
    {
        public Woman(string name) : base(name) { }
    }
    public class Man : Person
    {
        public Man(string name) : base(name) { }
    }
}
