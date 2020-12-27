using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class LineStation    // Station in Line Context
    {
        public int LineId { get; set; }
        public int Stations { get; set; }
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }
        public int Code { get; set; }   // Of the Station 
    }
}
