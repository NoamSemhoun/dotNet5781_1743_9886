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
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Address { get; set; }

        public List<Line> List_Lines; // who pass through this Station
                                      
        // bonus : (for user)
        public bool disabled_access;
        public bool Digital_PANNEL;
        public bool covered_shelter;
        // ...
    }

}
