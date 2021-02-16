using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    class ClockSimulator
    {
        private readonly TimeSpan setZero = new TimeSpan(24, 0, 0);
        private int simulationRate;
        private TimeSpan time;
        private bool stopFlag;
        
        internal bool StopFlag { get => stopFlag; }

        private bool isRun = false;
        public bool IsRun { get => isRun; }

        #region singlton

        private static ClockSimulator instance;

        internal static ClockSimulator Instance { get => instance; }

        static ClockSimulator()
        {
            instance = new ClockSimulator();
        }
        private ClockSimulator() { stopFlag = false; }

        #endregion

        internal TimeSpan Time
        {
            get{ return time; }
            private set
            {
                if (value != setZero)
                    time = value;
                else
                    time = new TimeSpan(0, 0, 0);
                if (TimeChanged != null)
                    TimeChanged(time);
            }
          
        }
        
        internal event  Action<TimeSpan> TimeChanged;
        
       


        internal void Run(int rate, TimeSpan startTime)
        {
            if (!isRun)
            {
                stopFlag = false;
                simulationRate = rate;
                time = startTime;
            
                isRun = true;
                while (!stopFlag)
                {
                    Time += new TimeSpan(0, 0, 1);
                    Thread.Sleep(1000 / simulationRate);
                }
                isRun = false;
            }
        }

        internal void stop()
        {
            stopFlag = true;
        }

        internal TimeSpan GetSimTimeSpan(TimeSpan time)
        {
            return new TimeSpan(time.Ticks / simulationRate);
        }


    }
}
