using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class LineStation    // Station in Line Context
    {
        public int LineId { get; set; }    
        public int Code { get; set; }   // Of the Station 
        public int Num_Stations { get; set; }   //   3/12  ?? 
        public int LineStationIndex { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }

        public override string ToString()
        {
            return $"LineID: {LineId} Code: {Code}  Num_Stations {Num_Stations} LineStationIndex { LineStationIndex } PrevStation { PrevStation } NextStation { NextStation }";
        }
    }
}
