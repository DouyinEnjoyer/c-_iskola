using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Rendeles
    {
        public static int OsszesRendeles = 0;
        public string aha;
        public Rendeles(string aha) 
        {
            this.aha = aha;
            OsszesRendeles++;
        }
    }
}
