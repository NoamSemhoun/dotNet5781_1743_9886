using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        public int LineId { get; set; }
        public int LineNumber { get; set; } 
        public TimeSpan DepertureTime { get; set; }
        public TimeSpan ExpectedTime { get; set; }
        public string LastStationName { get; set; }
        public int StationId { get; set; }
    }
}
