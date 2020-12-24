using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class Trip
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Lined { get; set; }
        public int InStation { get; set; }
        public TimeSpan InAt { get; set; }
        public int OutStation { get; set; }
        public TimeSpan OutAt { get; set; }
    }
}
