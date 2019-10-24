using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_2_13_Generic_01.Iterator
{

    public interface IKiller
    {
        void Kill();
    }

    public interface IGentleman
    {
        void Love();
    }

    class WarmKiller : IKiller, IGentleman
    {
        void IKiller.Kill()
        {
            Console.WriteLine("I gonna kill you.");
        }
        void IGentleman.Love()
        {
            Console.WriteLine("you always are my precious treasure.");
        }
    }

    class ItreatorTest
    {
        public IEnumerator<string> GetEnumerator()
        {
            yield return "begin:";
            for(int index=0;index<10;index++)
            {
                yield return index.ToString();
            }
            yield return "end.";
        }

    }
}
