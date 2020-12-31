using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class AdjacentStation
    {
        public int Statoin1 { get; set; }
        public int Station2 { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }

        public override string ToString()
        {
            return $"Statoin1 { Statoin1 }  Station2 {Station2} Distance { Distance } Time {Time}";
        }
    }
}
