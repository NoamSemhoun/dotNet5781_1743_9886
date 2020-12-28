using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class UserTrip  
    {
        public string UserName { get; set; }
        public int Id { get; set; }   // Trip Number
        public int LineID { get; set; }
        public int InStation { get; set; }
        public TimeSpan InAt { get; set; }
        public int OutStation { get; set; }
        public TimeSpan OutAt { get; set; }

    }
}
