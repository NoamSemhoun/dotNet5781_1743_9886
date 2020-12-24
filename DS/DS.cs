using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public class DS
    {
        //Random r = new Random()

        #region collections
        public static List<Bus> Buses;
        public static List<AdjacentStation> AdjacentStations;
        public static List<BusOnTrip> BusesOnTrip;
        public static List<Line> Lines;
        public static List<LineStation> LineStations;
        public static List<LineTrip> LineTrips;
        public static List<Station> Stations;
        public static List<Trip> Trips;
        public static List<User> Users;
        #endregion


        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            Buses = new List<Bus>
            {
                new Bus
                {
                    LicenseNum = 1111111,
                    FromDate = new DateTime(2012, 01, 01),
                    TotalTrip = 150000,
                    FuelRemain = 500,
                    Status = BusStatus.Available
                }
            };


        }
    }
}

