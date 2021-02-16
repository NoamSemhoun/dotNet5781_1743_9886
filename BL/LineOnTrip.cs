using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BO;


namespace BL
{
    class LineOnTrip
    {
        internal int Id { get; private set; }
        BO.Line line;
        TimeSpan startTime;
        Random random = new Random();
        Thread tripThread;
        ClockSimulator clock = ClockSimulator.Instance;
        //Dictionary<int, Action<TimeSpan>> stationInTracking;
        volatile bool stopFlag = false;

        internal IEnumerable<int> StationInTrip 
        {
            get =>
                from lineStation in line.List_LineStations
                select lineStation.Code; 
        }

        public event EventHandler LineOnStation;

        //public event Action<TimeSpan> LineOnStation;

        internal LineStation CurrentStation 
        {   get;    private set;  }

        //internal event EventHandler BusOnStation;
        
        internal LineOnTrip(LineTrips lineTrip)
        {
            line = GetBOItems.GetLine(l => l.LineID == lineTrip.lineId);
            startTime = lineTrip.DepartureTime;
            Id = line.LineID;
        }

        internal void RunTrip()
        {
            tripThread = new Thread(runTrip);
            tripThread.Start();
        }


        private void runTrip()
        {
            //LineStation station = line.List_LineStations.First()
            TimeSpan sleepTime;
            //Action<TimeSpan> lineInStation;

            Console.WriteLine("run line" + line.LineID + " " + clock.Time);
            foreach(LineStation station in line.List_LineStations)
            {
                Console.WriteLine("line" + line.LineID + " station "+ station.LineStationIndex + " " + clock.Time);
                CurrentStation = station;
                //if (stationInTracking.ContainsKey(station.Code))
                //{
                //    stationInTracking.TryGetValue(station.Code, out Action<TimeSpan> lineInStation);
                //    lineInStation(TimeSpan.Zero);
                //}
                if (LineOnStation != null)
                    LineOnStation(this, new EventArgs());
                sleepTime = clock.GetSimTimeSpan(new TimeSpan((long)(station.Time_ToNext.Ticks * ((random.NextDouble() * 0.35) + 0.9))));
                Thread.Sleep(sleepTime);
                if (stopFlag)
                    break;
            }
            Console.WriteLine("stop line" + line.LineID + " " + clock.Time);
        }

        internal TimeSpan GetExpectedTime(int stationId)
        {
            LineStation station = line.List_LineStations.FirstOrDefault(s => s.Code == stationId);
            if (CurrentStation.Code == stationId)
                return TimeSpan.Zero;
            return clock.Time + new TimeSpan((from lineStation in line.List_LineStations
                                              where lineStation.LineStationIndex >= CurrentStation.LineStationIndex && lineStation.LineStationIndex < station.LineStationIndex
                                              select lineStation.Time_ToNext.Ticks).Sum());
        }


        //internal void AddStationInTraking(int stationId, Action<TimeSpan> action)
        //{
        //    stationInTracking.Add(stationId, action);
        //}

        internal void StopRun()
        {
            stopFlag = true;
        }


        internal LineTiming GetLineTiminig(int stationId)
        {
            return new LineTiming
            {
                DepertureTime = startTime,
                LineId = line.LineID,
                LastStationName = line.LastStationName,
                LineNumber = line.LineNumber,
                StationId = stationId,
                ExpectedTime = GetExpectedTime(stationId)
            };
        }

        //internal void SetExpectedTime(LineTiming lineTiming)
        //{
        //    lineTiming.ExpectedTime = GetExpectedTime(lineTiming.StationId);
        //}

    }
}
