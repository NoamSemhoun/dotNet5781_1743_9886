using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class BusOnTrip
    {
        public int Id { get; set; }
        public int LicenceNum { get; set; }
        public int LineId { get; set; }
        public TimeSpan PlannedTakeOff { get; set; }
        public TimeSpan ActualTakeOff { get; set; }
        public int PrevStation { get; set; }
        public TimeSpan PrevStationAt { get; set; }
        public TimeSpan nextStationAt { get; set; }
    }
}
