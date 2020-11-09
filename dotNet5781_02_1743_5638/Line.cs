using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    enum Area { General, North, South, Center, Jerusalem }
    public class Line : IEnumerable  // IComparable  ?    //    List   KAV BODED    with all ther station
    {
        /*  // 1 et 2 
                bool SearchStation(int Kav,int code); code de la station sur une ligne
                int Distance(int NumStation1, int NumStation2); // distance entre les 2 station
                int Time(int NumStation1, int NumStation2); // temp  entre les 2 station      */

        private  int busLineNumber;
        private StationLine firstStation;
        private StationLine lastStation;
        private Area area;
        private static List<StationLine> listStations;  // List des Stations de cette Kav

        private static int sampleShelterNumber = 10000;   //  code  6 numbers 
        private static int sampleLineNumber = 1;

        #region Constructors

        public Line()   // ctor  create for      Initialisation
        {
            //    Tirer au sort le num/ Random random = new Random();  //    NumStation.Station.firstStation = random.NextDouble() * 999999;   // 1000 to 99999 ; //    NumStation.Station.lastStation = random.Next() * 999999;   // 1 to 999999 ;

            listStations = new List<StationLine>();  // New Kav
            for (int i = 0; i < 4; ++i, ++sampleShelterNumber)
            {
                //++sampleShelterNumber;
                listStations.Add(new StationLine(sampleShelterNumber));
            }

            //listStations.Sort();

            busLineNumber = sampleLineNumber++;
            area = Area.Jerusalem;

            firstStation = listStations.First();
            lastStation = listStations.Last();
        }

        public Line(int a)   // 2nd ctor    attention area missoug Enum Area
        {
                 busLineNumber = a;
            Console.WriteLine("Enter the area of this new Bus Line :\n" +
                             "1 to General,\n" +
                             "2 to North,\n" +
                             "3 to South,\n" +
                             "4 to Center\n" +
                             "or 5 to Jerusalem)\n");
            int temp = int.Parse(Console.ReadLine());
            area = (Area)temp;
            listStations = new List<StationLine>();  // New Kav
            Console.WriteLine("Enter the shellterNumber of your first station :");
            int first = int.Parse(Console.ReadLine());
            listStations.Add(new StationLine(first));
            Console.WriteLine("Enter the shellterNumber of your last station :");
            int last = int.Parse(Console.ReadLine());
            listStations.Add(new StationLine(first));
            firstStation = listStations.First();
            lastStation = listStations.Last();
        }

        #endregion




        #region FONCTION
        public static bool IsNumberStationExists(int numStation) =>       // verifier TOUTE station
           listStations.Exists(station => station.ShelterNumber == numStation);
        // check number station
        public static void addline(int NumLine, StationLine S) // a quel station l'ajouter ??
        {
            listStations.Add(S);
        }
        //public static void search(StationLine S) // retourne
        //{
        //    listStations.Find(S);
        //}
      //  public static void deleteStation(StationLine S) // a quel station l'ajouter ??
        //{
        //    listStations.Find(S);
        //      delete
        //}
        //public static void deleteaLine(List<StationLine> L1)
        //{
        //    L1.Clear();
        //    //L1.Remove();
        //}

        #endregion

        #region Admin

        public IEnumerator GetEnumerator()
        {
            return listStations.GetEnumerator();
        }

        public override string ToString()
        {
            string result = $"Bus Line Number: {busLineNumber}, in {area}, Stations : \n";
            foreach (Station station in listStations)
            { 
                result += station.ToString() + "\n";  // print all the stations    /// pblmmm
            }
            return result;
        }

        #endregion
    }
}
