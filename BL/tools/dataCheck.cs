using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BL
{

    delegate IEnumerable<T> GetDeleget<T>(Predicate<T> predicate);

    internal class DataCheck
    {
        
        static DalApi.IDAL dal = DalApi.DalFactory.GetDal();
        
        internal static bool isExeist<T>(Predicate<T> predicate)
        {
            GetDeleget<T> getDeleget;


            Type type = typeof(GetDeleget<T>);
            MethodInfo[] methodList = dal.GetType().GetMethods();

            foreach (MethodInfo method in methodList)
            {
                if (method.ReturnType == typeof(IEnumerable<T>) && method.GetParameters().Any())
                {
                    getDeleget = (GetDeleget<T>)Delegate.CreateDelegate(type, dal, method);

                    if (getDeleget(predicate).Any())
                        return true;
                    else
                        return false;
                }
            }
            return false;

            //switch(typeof(T).Name)
            //{
            //    case "AdjacentStation":
            //        return dal.GetAllAdjacentStationsBy(predicate as Predicate<DO.AdjacentStation>).Any();
            //    case "Bus":
            //        return dal.GetAllBusBy(predicate as Predicate<DO.Bus>).Any();
            //    case "BusOnTrip":
            //        return dal.GetAllBusesOnTripBy(predicate as Predicate<DO.BusOnTrip>).Any();
            //    case "Line":
            //        return dal.GetAllLinesBy(predicate as Predicate<DO.Line>).Any();
            //    case "LineStation":
            //        return dal.GetAllLineStationsBy(predicate as Predicate<DO.LineStation>).Any();
            //    case "LineTrip":
            //        return dal.GetAllLineTripsBy(predicate as Predicate<DO.LineTrip>).Any();
            //    case "Station":
            //        return dal.GetAllStationesBy(predicate as Predicate<DO.Station>).Any();
            //    case "User":
            //        return dal.GetAllStationesBy(predicate as Predicate<DO.Station>).Any();
            //}

            //throw new Exception("ERROR incorect type");

        }


        internal static bool didNeedUpdaete<T>(object obj ,Predicate<T> predicate)
        {
            GetDeleget<T> getDeleget;


            MethodInfo[] methodList = dal.GetType().GetMethods();

            foreach (MethodInfo method in methodList)
            {
                if (method.ReturnType == typeof(IEnumerable<T>) && method.GetParameters().Any())
                {
                    getDeleget = (GetDeleget<T>)Delegate.CreateDelegate(typeof(GetDeleget<T>), dal, method);

                    T t = getDeleget(predicate).FirstOrDefault();

                    if (t != null && t.isEqual(obj))
                        return false;
                    else
                        return true;
                    
                        
                }
            
            }
            return false;
        }



    }
}
