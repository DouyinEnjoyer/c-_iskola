using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
     class Kurzus
    {
        List<Hallgato> hallgatok = new List<Hallgato>();

        public void Felvesz(Hallgato h )
        {
            hallgatok.Add(h);
        }
        public void Listaz()
        {
            double osszeg = 0;
            int db = 0;
            for (int i = 0; i < hallgatok.Count; i++)
            {
                Console.WriteLine(hallgatok[i].Nev1 );
                osszeg += hallgatok[i].Atlag1;
                db++;

            }
            Console.WriteLine(osszeg/db);

        }





    }
}
