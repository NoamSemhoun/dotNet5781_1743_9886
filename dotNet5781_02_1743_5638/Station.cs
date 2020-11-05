using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    class Station
    {

        private int NumStation { get; set; } // code à 6 chiifres max

        //Position     	:
        double Latitude;   // -90, 90     ISR : [31,33.3]  unicode U+00B0 
        double Longitude;  // -180, 180   ISR : [34.3,35.5]   
        string adress;


        public Station() { } //ctor
        ~Station() { }  // dtor


        public override string ToString()
        {
            return $"Bus Station Code: {NumStation},{Latitude}\u00b0N  {Longitude}°E";
        }
    }
    
}
