using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class BusOnTrip
    {
        public int Id;
        public int LicenceNum;
        public int LineId;
        public TimeSpan PlannedTakeOff;
        public TimeSpan ActualTakeOff;
        public int PrevStation;
        public TimeSpan PrevStationAt;
        public TimeSpan nextStationAt;
    }
}
