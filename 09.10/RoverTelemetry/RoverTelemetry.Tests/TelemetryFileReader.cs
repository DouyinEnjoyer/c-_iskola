using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverTelemetry.Tests
{
    class TelemetryFileReader
    {
        static void Main(string[] args)
        {
            string[] sorok = File.ReadAllLines("telemetria.txt");

            for (int i = 0; i < sorok.Length; i++)
            {
                try
                {
                    if (sorok[i]=="0")
                    {
                        string[] szavak = sorok[i].Split(";");
                        int Id = Int32.Parse(szavak[0]);
                        string RoverName = szavak[1];
                        DateTime Date = DateTime.ParseExact(szavak[2], "d/M/yyyy h:mm", CultureInfo.InvariantCulture);
                        int Distance = Int32.Parse(szavak[3]);
                        bool IsSuccessful = bool.Parse(szavak[4]);
                    }
                    
                    



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            foreach (var sor in sorok)
            {

                
            }
        }
        
}
}
