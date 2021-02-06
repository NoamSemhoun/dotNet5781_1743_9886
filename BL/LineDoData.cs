using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BL
{
    class LineDoData
    {
        public DO.Line DOLine { get; }
        public List<DO.LineStation> DOLineStationsList { get; }
        public List<DO.Station> DOStationList { get; }
        public List<DO.AdjacentStation> DOAdjacentStationsList { get; }

        public LineDoData(Line line)
        {
            DOLine = (DO.Line)line.CloneNew(typeof(DO.Line));
            DOLineStationsList = new List<DO.LineStation>();
            DOStationList = new List<DO.Station>();
            DOAdjacentStationsList = new List<DO.AdjacentStation>();

            foreach (LineStation lineStation in line.List_LineStations)
            {
                LineStationDoData lineStationData = new LineStationDoData(lineStation);

                DOLineStationsList.Add(lineStationData.DOLineStation);
                DOStationList.Add(lineStationData.DOStation);
                if (lineStation.NextStation > 0)
                    DOAdjacentStationsList.Add(lineStationData.DOAdjacentStation);
            }
        }

        public bool didStationsExeist()
        {
            foreach (DO.Station station in DOStationList)
                if (!DataCheck.isExeist<DO.Station>(s => s.Code == station.Code))
                    return false;
            return true;
        }

        public bool didAdjacentStationsExeist(out int first, out int second)
        {
            if (!GetMissAdjStation().Any())
            {
                first = second = 0;
                return true;
            }
            first = GetMissAdjStation().First().Station1;
            second = GetMissAdjStation().First().Station2;
            return false;
        }

        public bool didAdjacentStationsExeist()
        {
            return GetMissAdjStation().Any();
        }

        public IEnumerable<AddLineExeption.StationAdjMissNumbers> GetMissAdjStation()
        {
            foreach (DO.AdjacentStation adj in DOAdjacentStationsList)
            {
                if (!DataCheck.isExeist<DO.AdjacentStation>(aS => aS.Statoin1 == adj.Statoin1 && aS.Station2 == adj.Station2))
                {
                    yield return new AddLineExeption.StationAdjMissNumbers { Station1 = adj.Statoin1, Station2 = adj.Station2 };
                }
            }

        }

        
    }


}
