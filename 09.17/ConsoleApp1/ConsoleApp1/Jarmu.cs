using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Jarmu
    {
        public string? Tulajdonos;
        public void TulajdonosBemutatasa()
        {
            Console.WriteLine(Tulajdonos ?? "Nincs megadva tulajdonos");
        }
    }



    
}
