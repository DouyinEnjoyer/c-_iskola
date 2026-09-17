using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Konyv
    {
        private string _cim;
        private string _szerzo;
        private int _oldalszam;

        
        public string Cim
        {
            get { return  _cim; }
            set
            {
               
                _cim = value;
            }

        }
        public string Szerzo
        {
            get { return _szerzo; }
            set
            {
                
                _szerzo = value;
            }

        }
        public int Oldalszam
        {
            get { return _oldalszam; }
            set
            {
                
                _oldalszam = value;
            }

        }
        public void Bemutat()
        {
            Console.WriteLine($"adatok {this._cim} {this._szerzo} {this._oldalszam}");
        }
        public Konyv(string cim, string szerzo, int oldalszam)
        {
              this._cim = cim;
            this._szerzo = szerzo;
            this._oldalszam = oldalszam;
        }
    }
}
