using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
     class Jarmu
    {
        int sebbeseg;

        public int Sebbeseg { get => sebbeseg; set => sebbeseg = value; }
        public void Halad()
        {

        }
    }
    class Auto : Jarmu 
    {
        int uzemanyag;

        public int Uzemanyag { get => uzemanyag; set => uzemanyag = value; }

        public void Tankol()
        {

        }
    }
    class Bicikli : Jarmu
    {
        public void Csenget()
        {
            Console.WriteLine();
        }
    }

}
