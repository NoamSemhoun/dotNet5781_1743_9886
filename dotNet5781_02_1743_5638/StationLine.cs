using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
   
    public class StationLine     //   MASLOUL    
    {
      
        private Station station;   // objet de l'autre classe
 
        private double distance;    // with the precedent station 
        private int time;           // avec la station précedente

        public StationLine(int num)        // ctor
        {
            Random random = new Random();

            station = new Station();   
            distance = random.NextDouble() * 500;   // 1 to 500m
            time = random.Next(2,15);          // 2 to 15 minutes
        }

        public StationLine(Station station, double distance, int time)  // 2nd ctor to 
        {



        }


        //public override string ToString()
        //{
        //    return $"Bus Station Code: {NumStation},{latitude}\u00b0N  {longitude}°E";
        //}

    };
    
}
