using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace BL
{
    public class LineTrips
    {
        public TimeSpan DepartureTime;
        public int lineId;
        static int i = 0;

        public static IEnumerable<LineTrips> CreatLineTripsList(int id )
        {
            IDAL dal = DalFactory.GetDal();
           
            TimeSpan time;

            foreach (DO.LineTrip lT in dal.GetAllLineTripsBy(l => l.LineID == id))
            {
                time = lT.StartAt;
                while (time < lT.FinishAt)
                {
                    yield return new LineTrips { DepartureTime = time, lineId = lT.LineID };
                    time += lT.Frequency;
                }
            }
        }

        public static IEnumerable<LineTrips> GetAllLineTrips()
        {
            IDAL dal = DalFactory.GetDal();
            return from line in dal.GetAllLines()
                   from lineTrips in CreatLineTripsList(line.LineID)
                   select lineTrips;
        }


    }

   
}
