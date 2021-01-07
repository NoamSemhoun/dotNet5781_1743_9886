using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace BO
{
    
        public enum BusStatus { Available, OnDrive, InMaintenance, InRefuling }  // NEED_MAINTENANCE, NEED_REFUEL ?  , Delete
        public enum Areas { General, North, South, Center, Jerusalem }
        public enum DataType { AdjacentStation, LineData, StationData, LineStation }
}
