using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace BL
{
    class Schedules
    {
        static IDAL dal = DalFactory.GetDal();

        public static IEnumerable<TimeSpan> BuildSchdual(int line, int station)
        {
            List<TimeSpan> schedual = new List<TimeSpan>();
            LineStation lineStation = dal.GetAllLineStationsBy(l => l.LineId == line && l.Code == station).FirstOrDefault();
            List<DO.LineTrip> lineTrips = dal.GetAllLineTripsBy(l => l.LineID == line).OrderBy(l => l.StartAt).ToList();
            TimeSpan timeFromStart = new TimeSpan ((from item in dal.GetAllLineStationsBy(ls => ls.LineId == line)
                                                  where item.LineStationIndex < lineStation.LineStationIndex
                                                  select dal.GetAdjacentStation(item.Code, item.NextStation).Time.Ticks).Sum());
            foreach (LineTrip lineTrip in lineTrips)
            {
                TimeSpan arival = lineTrip.StartAt + timeFromStart;
                while (arival < lineTrip.FinishAt + timeFromStart)
                {
                    schedual.Add(arival);
                    arival += lineTrip.Frequency;
                }
            }

            return schedual;
        }

        public static BO.LineSchedule GetLineScadual(TimeSpan now, int lineId, int station)
        {
            Line line = dal.GetLine(lineId);


            return new BO.LineSchedule {
                id = lineId,
                LineNumber = line.LineNumber,
                NextArrival = GetNextesArrivalTime(now, lineId, station).ElementAt(0),
                NextArrival_2 = GetNextesArrivalTime(now, lineId, station).ElementAt(1),
                Destenation = dal.GetStation(line.LastStation).Address
            };
        }

        public static IEnumerable<TimeSpan> GetNextesArrivalTime(TimeSpan now, int lineId, int station)
        {
            List<TimeSpan> schedual = BuildSchdual(lineId, station).ToList();
            schedual.Sort();
            int index = 0;
            foreach (TimeSpan time in schedual)
                if (time < now)
                    index++;
                else
                    break;
            while (index < schedual.Count())
                yield return schedual[index++];
            throw new Exception("the is not more buses today");
        }

    }
}
