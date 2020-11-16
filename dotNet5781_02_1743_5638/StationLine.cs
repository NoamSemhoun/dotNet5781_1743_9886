using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{

    public class StationLine : Station, IComparable
    { 

        private double distance;    
        private static Random r=new Random();
        private TimeSpan temps;
       
        public TimeSpan Temps { get => temps; set => temps = value; }
    
        public double Distance { get => distance; set => distance = value; }
        public StationLine(int StationNumber) : base(StationNumber)
        {
             
            distance = r.Next(0,500);
            if(distance>250)//I tried to make it logic ...the distance against the time.
            {
                Temps = new TimeSpan(0,r.Next(5, 10),r.Next(0,60));
            }
            else
            {
                Temps = new TimeSpan(0, r.Next(0, 5), r.Next(0, 60));
            }
                     
        }

       
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            StationLine otherStationLine = obj as StationLine;
            if (otherStationLine != null)
                return this.temps.CompareTo(otherStationLine.temps);
            else
                throw new ArgumentException("Object is not a StationLine");
        }

        public override string ToString()
        {
            return $"Bus Station Code: {this.shelterNumber}, {this.latitude}\u00b0N {longitude}°E ,Distance from the last Stop : {distance}, Time from the last Stop :{temps.ToString()}";
         
        }

    };

}