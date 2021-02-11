using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class ClockChangedEventArgs : EventArgs
    {
        TimeSpan Time { get; }
        public ClockChangedEventArgs(TimeSpan time)
        {
            Time = time;
        }
    }
}
