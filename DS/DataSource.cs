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

        public static List<string> List_Addresses = new List<string> 
        {"12 Chazar", "30 Havaad Heleumi", "21 Begin", "12 Hebron Road","32 Bayt vagan", "51 Herzl" ,"79 Ben Gourion",
        "26 Harav Kook","61 Ben Yeouda"};  // Num between 10-99 only
        

        private static Random random = new Random();

        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()    // 50 stations ,  10lines with each 10stations,  20Buses  etc... 
            // Check existing 
        {
            List_Stations = new List<Station>();
            for (int i = 0; i < 50; i++)  // Create 50 Stations 
            {
             List_Stations.Add(
                new Station { 
                Code= 10000+i,            //  or : random.Next(0,999)    (code existing !)
                Longitude = random.NextDouble() * 1.2 + 34.3,       // In ISRAEL :  [34.3, 35.5]
                Latitude = random.NextDouble() * 2.3 + 31,          //              [31, 33.3]
                Address = List_Addresses[ i % List_Addresses.Count() ],
                Name = List_Addresses[i % List_Addresses.Count()].Substring(3) +  "/" + List_Addresses[i % List_Addresses.Count()].Substring(3) //    Begin/Hebron  9=this Adress  (??
                });
            }

            List_Buses = new List<Bus>() ;
            for (int i = 0; i < 20; i++)  // Create 20 Buses 
            {
                List_Buses.Add(
                new Bus
                {
                    LicenseNum = 10000000 + i,   // 10000000 to 10000020
                    FromDate = new DateTime(2021, 01, 01),
                    TotalTrip = random.Next(0, 999),
                    FuelRemain = random.Next(0, 1200),
                    Status = BusStatus.Available
                });


            };

            List_Lines = new List<Line>(); // Create 10 Line stations (= Masloul)
            List_LineStations = new List<LineStation>();   // With 10 Lines each
            for (int i = 0; i < 10; i++)
            {
            List_Lines.Add(
                new Line {
                    Id = 1 + i,
                    Area = (Areas)random.Next(0, 5),
                    Code =10000+i,  // CodeLine ??
                    FirstStation= 10000 + i*10,
                    LastStation= 10009 + i*10
                });

                for (int j = 0; j < 10; j++)
                {
                    List_LineStations.Add(    // Create 10 LineStation for THIS Line
                        new LineStation {
                            LineId= 1 + i ,
                            LineStationIndex= 1+j,   // x/10
                            Code = 10000 + j + (i * 10),
                            NextStation =( 10001 + j + (i * 10) ) % (10010 + i * 10),  // The Next of the LastStation = 0
                            PrevStation = (999 + j + (i * 10)) % (999 + (j * 2) + (i * 10)),//  The Prev of the FirstStation = 0
                            Stations = 10      
                        } );
                } 

            }

        }
    }
}

