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
        private readonly int simulationRate;
        private TimeSpan time;
        internal TimeSpan Time
        {
            get{ return time; }
            private set
            {
                time = value;
                if (TimeChanged != null)
                    TimeChanged(this, new BO.ClockChangedEventArgs(time));
            }
          
        }


        private bool StopFlag;
        internal event EventHandler TimeChanged;
        

        internal ClockSimulator(int rate, TimeSpan startTime)
        {
            simulationRate = rate;
            time = startTime;
            StopFlag = false;
        }

        internal void Run()
        {
            while( !StopFlag )
            {
                time += new TimeSpan(0, 0, 1);
                Thread.Sleep(1000 / simulationRate);
            }
        }
        
        internal void stop()
        {
            StopFlag = true;
        }


    }
}
