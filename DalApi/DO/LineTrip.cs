using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public class LineTrip
    {
        public int Id { get; set; }
        public int lineId { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan Frequency { get; set; }
        public TimeSpan FinishAt { get; set; }
    }
}
