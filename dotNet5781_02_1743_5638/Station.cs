using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    public class Station            //  POSITION : 
    {

        protected int shelterNumber;  //   { get; set; } ??   

        //Position     	:
        protected double latitude;   // -90, 90       unicode U+00B0 
        protected double longitude;  // -180, 180    
        protected string address;

        private static List<string> listAddresses = new List<string> { "12 Chazar, Jerusalem", "30 Havaad Heleumi, " +
            "Jerusalem", "21 Begin, Jerusalem", "12 Hebron Road, Jerusalem","32 Bayt vagan" ,"5 Herzl, Jerusalem" };

        protected Station(int StationNumber) // ctor   position crée new Station avec positon 
        {
            if (Line.IsNumberStationExists(StationNumber))
                throw new ArgumentException($"The Number Station {StationNumber} still exists. Enter another..."); //

            this.shelterNumber = StationNumber;

            Random r = new Random();
            latitude = r.NextDouble() * 2.3 + 31;       // [31,33.3] להגריל      in Israel
            longitude = r.NextDouble() * 1.2 + 34.3;        // [34.3,35.5] 

            int index = r.Next(listAddresses.Count);   // כתובת להגריל BETWEEN 0 and Count
            address = listAddresses.ElementAt(index); // listAddresses[index];
        }

        public override string ToString()    // lo tsarikh
        {
            return $"Bus Station Code: {shelterNumber},{latitude}\u00b0N  {longitude}°E";
        }
    }

}
