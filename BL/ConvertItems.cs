using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    class ConvertItems
    {
        private static DalApi.IDAL dal = DalApi.DalFactory.GetDal();

        public static List<DO.AdjacentStation> GetAdjStationToAdd(Line line)
        {
            List<DO.AdjacentStation> returnList = new List<DO.AdjacentStation>();
            for(int i = 0; i < line.List_LineStations.Count - 1; i++)
            {
                DO.AdjacentStation adjacentStation = new DO.AdjacentStation
                {
                    Statoin1 = line.List_LineStations[i].Code,
                    Station2 = line.List_LineStations[i + 1].Code,
                    Distance = line.List_LineStations[i].Distance_ToNext,
                    Time = line.List_LineStations[i].Time_ToNext
                };
                try
                {
                    DO.AdjacentStation aS = dal.GetAdjacentStation(line.List_LineStations[i].Code, line.List_LineStations[i + 1].Code);
                    //if (!aS.isEqual(adjacentStation))
                        //throw new Exception(); // ***** /// ***** unadapt data exeption                
                }
                catch (DO.ItemNotExeistExeption)
                {
                    returnList.Add(adjacentStation);
                }
            }
            return returnList;
        }

        public static List<DO.LineStation> GetLineStationToAdd(int id, Line line)
        {
            List<DO.LineStation> list = new List<DO.LineStation>();
            for (int i = 0; i < line.List_LineStations.Count; i++)
            {
                var lS = (DO.LineStation)line.List_LineStations[i].CloneNew(typeof(DO.LineStation));
                lS.LineId = id;
                list.Add(lS);
            }
            return list;
        }
    }
}
