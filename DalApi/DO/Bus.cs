using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class Bus
    {
        public int LicenseNum { get; set; } // Format : 7 digits : XX-XXX-XX  or 8 digits  XXX-XX-XXX
        public DateTime StartDate { get; set; }
        public double Total_Km { get; set; }
        public double Fuel { get; set; }
        public BusStatus Status { get; set; }
        public double Km_LastMaintenance { get; set; }
        public DateTime Date_LastMaintenance { get; set; }

        // bonus : bool Handicap, WIFI, int seat, speed ? ...

    }
}
