using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_1743_5638
{
    public class Bus
    {                                                   // To do ;
        public int BusNumber { get; set; } // Liscence
        public DateTime DateStart { get; set; }
        public int Fuel { get; set; }
        public double KmTotal { get; set; }

        public Status Status { get; set; } // enum
        public struct Maintenance // ou ds une autre classe avec { get; set;)
       {
            public DateTime DateOfMaintenance { get; set; }
            public double Kilometrage { get; set; } 
       }
        public override string ToString()
        {
            return String.Format("License: {0,-10}, lastCheckupDate: {1}, Total km: {2}, fuel: {3}"  , BusNumber, DateStart, KmTotal, Fuel);
        }
    
    }
}
