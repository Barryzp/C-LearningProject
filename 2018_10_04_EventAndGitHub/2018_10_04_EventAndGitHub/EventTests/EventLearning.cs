using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_10_04_EventAndGitHub
{

    public abstract class animal
    {
        public abstract void Eat();
        public void Drink() { }
    }

    public class Teacher
    {
        //claim a delegate.
        public delegate void PrintWordsDelegate(string param);
        //claim a event.
        public event PrintWordsDelegate SayingWords;

        public void SpeakYourName(string param)
        {
            Console.WriteLine("Guys,you got to say something interested!");
            SayingWords.Invoke(param);
        }
    }

    public class Person
    {
        private string name;
        public Person(string name)
        {
            this.name = name;
        }

        public virtual void Saying(string param) { Console.WriteLine("My name is: " + name+param); }
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
