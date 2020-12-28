using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        public int LineId { get; set; } // 1 to 9999
        public int CodeLine { get; set; }   
        public int FirstStation { get; set; }  // Code
        public int LastStation { get; set; } 
        public Areas Area { get; set; }

        public List<LineStation> List_LineStations;  // Masloul
    }
}
