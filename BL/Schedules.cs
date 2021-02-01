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

        private List<TimeSpan> departureTimes; 
        private List<TimeSpan> statinsTimes; 
       
        

        
        private BO.Line line;

        public IEnumerable<TimeSpan> DepartureTimes { get => departureTimes; }
        public IEnumerable<TimeSpan> StatinsTimes { get => statinsTimes; }

        public Schedules(BO.Line linePar)
        {
            line = linePar;
            buildDepartureTimes();
            buildStatinsTimes();           
        }

        private void buildDepartureTimes()
        {
            departureTimes = new List<TimeSpan>();
            foreach (LineTrip lineTrip in dal.GetAllLineTripsBy(lt => lt.LineID == line.LineID))
            {
                TimeSpan timeSpan = lineTrip.StartAt;
                if (departureTimes.Any() && timeSpan == departureTimes.Last())
                    departureTimes.RemoveAt(departureTimes.Count - 1);
                while(timeSpan <= lineTrip.FinishAt)
                {
                    departureTimes.Add(timeSpan);
                    timeSpan += lineTrip.Frequency;
                }
            }
        }

        private void buildStatinsTimes()
        {
            statinsTimes = new List<TimeSpan>() { new TimeSpan() };
            line.List_LineStations.Sort((x, y) =>
            {
                if (x.LineStationIndex < y.LineStationIndex)
                    return -1;
                else
                    return 1;
            });
            for (int i = 0; i < line.List_LineStations.Count - 1; i++)
            {
                statinsTimes.Add(statinsTimes.Last() + line.List_LineStations[i].Time_ToNext);
            }
        }



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
                NextArrivals = GetNextesArrivalTime(now, lineId, station).ToArray(),
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
            //////////////throw new Exception("the is not more buses today");
        }

    }
}
