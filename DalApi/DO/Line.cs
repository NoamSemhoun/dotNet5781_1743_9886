using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class Line
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Areas Area { get; set; }
    }
}
