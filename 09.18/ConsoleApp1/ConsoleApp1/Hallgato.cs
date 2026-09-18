using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
     class Hallgato
    {
        string nev;
        double atlag;

        public string Nev1 { get => nev; set => nev = value; }
        public double Atlag1 
        { 
            
            get => atlag;
            

            set
            {
                if(value < 0 && value > 5)
                {
                    throw new Exception();
                }
                atlag = value;
            }


        }
        /*
        public Hallgato(string nev, double atlag)
        {
            this.nev = nev;
            
            if(atlag < 0 && atlag > 5)
            {
                throw new Exception();
            }
            this.atlag = atlag;
        }
        */

    }
}
