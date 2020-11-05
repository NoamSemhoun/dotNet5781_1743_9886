using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    enum Area { General, North, South, Center, Jerusalem }
    public class Line : IEnumerable                         //    List   KAV BODED    with all ther station
    {
        
        /*  // 1 et 2 
                bool SearchStation(int Kav,int code); // code de la station sur une ligne
                int Distance(int NumStation1, int NumStation2); // distance entre les 2 station
                int Time(int NumStation1, int NumStation2); // temp  entre les 2 station      */
               
        

        private int busLineNumber;
        private StationLine firstStation;
        private StationLine lastStation;
        private Area area;
        private List<StationLine> listStations;

        public Line()
        {
            
        }

        public Line(int bus, Area area, List<StationLine> listStationLines)
        {
            //List<StationLine> list = new List<StationLine>();
            //list.Add(new StationLine());
        }

        public IEnumerator GetEnumerator()
        {
            return listStations.GetEnumerator();
        }
    }
}
