using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
     class Mozijegy
    {
        public int _ar;

        public int Ar
        {
            get { return Ar; }
            set { 
            if (Ar !> 0) 
                {
                    throw new ArgumentException();
                }
            else
                {
                    Ar = value;
                }
            }
        }
            

        public Mozijegy(int ar)
        {

            if (ar > 0) { this._ar = ar; }
            else { throw new ArgumentException(); }
            
        }

        
            
             


    }
}
