using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineSchedule
    {
        public int id { get; set; }
        public int LineNumber { get; set; }
        public TimeSpan NextArrival { get; set; }
        public TimeSpan NextArrival_2 { get; set; }
        public string Destenation { get; set; }
    }
}
