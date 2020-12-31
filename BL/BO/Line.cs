using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BO
{
    public class Line
    {
        public int LineID { get; set; } // 1 to 9999
        public int LineNumber { get; set; }  
        public int FirstStation { get; set; }  // LineNumber
        public int LastStation { get; set; }
        public string FirstStationName { get; set; }
        public string LastStationName { get; set; }

        public Areas Area { get; set; }

        public List<LineStation> List_LineStations { get; set; }  // Masloul
    }
}
