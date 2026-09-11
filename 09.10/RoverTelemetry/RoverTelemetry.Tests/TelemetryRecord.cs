using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverTelemetry.Tests
{
    public class TelemetryRecord
    {
        public int Id { get; set; }
        public string RoverName { get; set; }
        public DateTime Date { get; set; }
        public int Distanec { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
