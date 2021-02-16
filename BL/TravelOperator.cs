using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BO;

namespace BL
{
    class TravelOperator
    {
        static TravelOperator instance;

        internal static TravelOperator Instance { get => instance;}


        Thread travelOperatorThread;

        ClockSimulator clock = ClockSimulator.Instance;
        List<LineOnTrip> lineOnTrips_list = new List<LineOnTrip>();
        List<LineTrips> lineTrips_List = new List<LineTrips>();
        Dictionary<int, Action<LineTiming>> updateLinePanel = new Dictionary<int, Action<LineTiming>>();



        static TravelOperator()
        {
            instance = new TravelOperator();
        }

        private TravelOperator()
        {
            //travelOperatorThread = new Thread(runTravelOperator);                    
        }

       

        internal void AddStation(int stationId, Action<LineTiming> updateLines)
        {
            foreach (int lineId in ManageDoData.GetLinesInStation(stationId))
                AddLine(lineId);
            updateLinePanel.Add(stationId, updateLines);
        }


        internal void RunTravelOperator()
        {
            if (travelOperatorThread == null || !travelOperatorThread.IsAlive)
            {
                travelOperatorThread = new Thread(runTravelOperator);
                travelOperatorThread.Start();
            }
        }

       
        private void AddLine(int lineId)
        {
            foreach (LineTrips lineTrip in LineTrips.CreatLineTripsList(lineId))
                lineTrips_List.Add(lineTrip);                
        }

        private void runTravelOperator()
        {
            //Console.WriteLine("travelopearator start");
            TimeSpan sleepTime;
            while (!clock.StopFlag)
            {               
                foreach (LineTrips lineTrip in lineTrips_List.Where(lT => lT.DepartureTime >= clock.Time).OrderBy(lT => lT.DepartureTime))
                {
                    sleepTime = clock.GetSimTimeSpan(lineTrip.DepartureTime - clock.Time);
                    if(sleepTime >= TimeSpan.Zero)
                        Thread.Sleep(sleepTime);
                    if (clock.StopFlag)
                        break;
                    LineOnTrip lineOnTrip = new LineOnTrip(lineTrip);
                    lineOnTrip.LineOnStation += updateStationsPanel;
                    lineOnTrips_list.Add(lineOnTrip);
                    lineOnTrip.RunTrip();                    
                }
            }
            foreach (LineOnTrip lineOnTrip1 in lineOnTrips_list)
                lineOnTrip1.StopRun();
            //Console.WriteLine("TravelOperatorFinish");
        }

        private void updateStationsPanel(object sender, EventArgs e)
        {
            LineOnTrip thisTrip = sender as LineOnTrip;
            foreach (int code in thisTrip.StationInTrip)
            {
                if (updateLinePanel.ContainsKey(code))
                    updateLinePanel[code](thisTrip.GetLineTiminig(code));
            }
        }

        




    }
}
