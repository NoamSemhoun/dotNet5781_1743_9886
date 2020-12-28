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
    }
}
