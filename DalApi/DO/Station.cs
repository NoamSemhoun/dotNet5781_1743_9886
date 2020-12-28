using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class Station
    {
        public int Code { get; set; }    // 1 to 9999
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string Address { get; set; }

   

        // bonus : bool Handicap, shelter ...


    }
}
