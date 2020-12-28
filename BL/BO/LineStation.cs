using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public int Code { get; set; }
        public TimeSpan Time_ToNext { get; set; }
        public int Distance_ToNext { get; set; }
        public int LineId { get; set; }


        // ?? :
        public string Name { get; set; }
       /* public int Stations { get; set; } */// Num
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }


    }
}
