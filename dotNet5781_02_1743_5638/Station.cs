using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    public class Station            //  POSITION : 
    {

        private int NumStation;  // code à 6 chiifres max     { get; set; } ??   
        // Verifier que yen a pas 2

        //Position     	:
        private double latitude;   // -90, 90     ISR : [31,33.3]  unicode U+00B0 
        private double longitude;  // -180, 180   ISR : [34.3,35.5]   
        private string address;

        List<string> listAddresses;

        public Station() // ctor   position
        {
            // if (NumStation) existe déja; 

            listAddresses = new List<string> { "12 Chazar, Jerusalem", "30 Havaad Heleumi, Jerusalem", "21 Begin, Jerusalem", "12 Hebron Road, Jerusalem", "5 Herzl, Jerusalem" };

            Random r = new Random();
            latitude = r.NextDouble() * 2.3 + 31;       // [31,33.3] להגריל     
            longitude = r.NextDouble() * 1.2 + 34.3;        // [34.3,35.5] 
            
            int index =  r.Next(listAddresses.Count) ;   // כתובת להגריל BETWEEN 0 and Count
            address = listAddresses.ElementAt(index); // listAddresses[index];
        } 

        public override string ToString()
        {
            return $"Bus Station Code: {NumStation},{latitude}\u00b0N  {longitude}°E";
        }
    }
    
}
