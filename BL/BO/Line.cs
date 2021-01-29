using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BO
{
    public class Line: IComparable
    {
        public int LineID { get; set; } // 1 to 9999
        public int LineNumber { get; set; }  
        public int FirstStation { get; set; }  // LineNumber
        public int LastStation { get; set; }
        public string FirstStationName { get; set; }
        public string LastStationName { get; set; }

        public Areas Area { get; set; }

        public List<LineStation> List_LineStations { get; set; }  // Masloul  Atention to the order !

        public int CompareTo(object obj)
        {
            if (obj is Line)
                if (this.LineID < (obj as Line).LineID)
                    return 1;
                else 
                    return -1;
            throw new Exception();
                   
        }
    }
}
