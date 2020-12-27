using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Station
    {
        public int Code { get; set; }
        public string Name { get; set; }

        List<Line> List_Lines; // pass through
    }
}
