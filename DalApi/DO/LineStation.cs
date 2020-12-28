using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class LineStation    // Station in Line Context
    {
        public int LineID { get; set; }    
        public int Code { get; set; }   // Of the Station 
        public int Num_Stations { get; set; }
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }
    }
}
