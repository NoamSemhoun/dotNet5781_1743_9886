using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
   
    public class StationLine : Station, IComparable     //   MASLOUL    
    {

        private double distance;    // with the precedent station 
        private int time;           // avec la station précedente

        public StationLine(int StationNumber) : base(StationNumber)  // shelter !!      // ctor  father : Station Class
        {
            Random random = new Random(); 
            distance = random.NextDouble() * 500;   // 1 to 500m
            time = random.Next(2,15);          // 2 to 15 minutes
        }

        public StationLine(int num, double distance, int time) : base(num)  // 2nd ctor to 
        {

        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            StationLine otherStationLine = obj as StationLine;
            if (otherStationLine != null)
                return this.time.CompareTo(otherStationLine.time);
            else
                throw new ArgumentException("Object is not a StationLine");
        }

        public int GetStationNumber() {get=> shelterNumber;}  //

        public override string ToString()  
        {
            return $"Bus Station Code: {this.shelterNumber}, {this.latitude}\u00b0N {longitude}°E"; ///  BYAAAA
        }

    };
    
}
