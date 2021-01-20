using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        public int LicenseNum { get; set; }
        public DateTime StartDate { get; set; }
        public double Total_Km { get; set; } // Kilometrage
        public double Fuel { get; set; }  // Remain
        public BusStatus Status { get; set; }
        public double Km_LastMaintenance { get; set; }  // Since the last maintenance
        public DateTime Date_LastMaintenance { get; set; }


        public override string ToString()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n", LicenseNum, StartDate, Total_Km, Fuel, Status, Km_LastMaintenance, Date_LastMaintenance);
        }
    }
}
