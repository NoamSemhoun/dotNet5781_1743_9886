using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    enum Area { General, North, South, Center, Jerusalem }
    public class Line : IEnumerable // IComparable                        //    List   KAV BODED    with all ther station
    {
        /*  // 1 et 2 
                bool SearchStation(int Kav,int code); // code de la station sur une ligne
                int Distance(int NumStation1, int NumStation2); // distance entre les 2 station
                int Time(int NumStation1, int NumStation2); // temp  entre les 2 station      */
               
        private int busLineNumber;
        private StationLine firstStation;
        private StationLine lastStation;
        private Area area;
        private List<StationLine> listStations;  // List des Stations de cette Kav

        public Line()   // ctor  create for
        {
            //    Random random = new Random();
            //    NumStation.Station.firstStation = random.NextDouble() * 999999;   // 1 to 99999 ;
            //    NumStation.Station.lastStation = random.Next() * 999999;   // 1 to 999999 ;

        }

        public Line(int bus, int area, List<StationLine> listStationLines)   // 2nd ctor    attention area missoug Enum Area
        {
           
        }

        public IEnumerator GetEnumerator()
        {
            return listStations.GetEnumerator();
        }
        
         public override string ToString()
         {
             return $"Bus Lin Number: {busLineNumber}, in {area} \n {listStations}";
         }
    }
}
