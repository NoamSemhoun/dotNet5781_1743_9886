using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    class LineStationDoData
    {
        public DO.LineStation DOLineStation { get; }
        public DO.AdjacentStation DOAdjacentStation { get; }
        public DO.Station DOStation { get; }

        public LineStationDoData(LineStation lineStation)
        {
            DOLineStation = (DO.LineStation)lineStation.CloneNew(typeof(DO.LineStation));
            if (lineStation.NextStation > 0)
                DOAdjacentStation = new DO.AdjacentStation
                {
                    Statoin1 = lineStation.Code,
                    Station2 = lineStation.NextStation,
                    Distance = lineStation.Distance_ToNext,
                    Time = lineStation.Time_ToNext
                };
            DOStation = new DO.Station
            {
                Code = lineStation.Code,
                Name = lineStation.Name
            };
        }
    }
}
