using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    public static class Cloning
    {
        public static void Clone<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = from.GetType().GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                if (!propFrom.GetIndexParameters().Any())
                {
                    var value = propFrom.GetValue(from, null);
                    if (value is ValueType || value is string)
                        propTo.SetValue(to, value);
                }                    
            }
        }
        public static object CloneNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type);
            from.Clone(to);
            return to;
        }

        public static IEnumerable<T> CloneList<T>(this IEnumerable<T> list)
        {
            if (typeof(T) is object)
                return from item in list
                       select (T)item.CloneNew(typeof(T));
            else
                return from item in list
                       select item;
        }


        public static void LineStationListToDoObjectsLists(this List<LineStation> list, List<DO.LineStation> doLS_List, List<DO.AdjacentStation> doAS_List)
        {

            foreach (LineStation lineStation in list)
            {
                DO.LineStation doLStation = new DO.LineStation();
                lineStation.Clone(doLStation);
                doLS_List.Add(doLStation);

                if (lineStation.NextStation != -1)
                    doAS_List.Add(new DO.AdjacentStation
                    {
                        Statoin1 = lineStation.Code,
                        Station2 = lineStation.NextStation,
                        Distance = lineStation.Distance_ToNext,
                        Time = lineStation.Time_ToNext
                    });
            }
        }

        public static Station DoObjectsToStation(int code)
        {
            DalApi.IDAL dal = DalApi.DalFactory.GetDal();
            Station station = new Station();
            dal.GetStation(code).Clone(station);
            station.List_Lines = (from item in dal.GetAllLineStationsBy(lS => lS.Code == code)
                                  select (Line)dal.GetLine(item.LineId).CloneNew(typeof(Line))).ToList();
            return station;
        }




    
    }
}