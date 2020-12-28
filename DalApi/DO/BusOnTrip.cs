using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class BusOnTrip
    {
        public int Id { get; set; }
        public int LicenceNum { get; set; }
        public int LineID { get; set; }
        public TimeSpan Depart_Planned { get; set; }
        public TimeSpan Real_Depart { get; set; }
        public int PrevStation { get; set; }
        public TimeSpan Timeto_PrevStation { get; set; }
        public TimeSpan Timeto_NextStation { get; set; }
    }
}
