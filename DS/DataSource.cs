using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    public class DataSource
    {
        //Random r = new Random()

        #region collections
        public static List<Bus> List_Buses;
        public static List<AdjacentStation> List_AdjacentStations;
        public static List<BusOnTrip> List_BusesOnTrip;
        public static List<Line> List_Lines;
        public static List<LineStation> List_LineStations;
        public static List<LineTrip> List_LineTrips;
        public static List<Station> List_Stations;
        public static List<Trip> List_Trips;
        public static List<User> List_Users;
        #endregion


        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            List_Buses = new List<Bus>
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

