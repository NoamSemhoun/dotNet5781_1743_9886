using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    static class GetBOItems
    {

        static DalApi.IDAL DAL = DalApi.DalFactory.GetDal();


        public static IEnumerable<Line> GetLines(Predicate<Line> predicat)
        {
            return from item in DAL.GetAllLines()
                   let line = (Line)item.CloneNew(typeof(Line))
                   where predicat(line)
                   select setStationsData(line);
        }


        public static Line GetLine(Predicate<Line> predicat)
        {
            Line line = GetBase<DO.Line, Line>(predicat);
            if (line == null)
                throw new ItemNotExeistExeption(typeof(Line), 000);

            

            setStationsData(line);
 
            return line;
        }

        private static Line setStationsData(Line line)
        {
            line.List_LineStations = GetLineStations(lS => lS.LineId == line.LineID).OrderBy(l => l.LineStationIndex).ToList();

            line.LastStation = line.List_LineStations.Last().Code;
            line.FirstStation = line.List_LineStations[0].Code;

            line.LastStationName = line.List_LineStations[line.List_LineStations.Count - 1].Name;
            line.FirstStationName = line.List_LineStations[0].Name;

            return line;
        }

        public static IEnumerable<LineStation> GetLineStations(Predicate<LineStation> predicate)
        {
            return from item in DAL.GetAllLineStations()
                   let lineStation = item.CloneNew(typeof(LineStation))
                   where predicate((LineStation)lineStation)
                   select (((LineStation)lineStation).AddAdjData().AddStationName());
        }

        public static LineStation GetLineStation(Predicate<LineStation> predicat)
        {
            LineStation lineStation = (from item in DAL.GetAllLineStations()
                                       let adj = item.CloneNew(typeof(LineStation))
                                       where predicat((LineStation)adj)
                                       select (LineStation)adj).FirstOrDefault();
            if (lineStation == null)
                throw new ItemNotExeistExeption(typeof(AdjacentStation), 000);

            AddAdjData(lineStation);

            return lineStation;
        }

        private static LineStation AddAdjData(this LineStation lineStation)
        {   
            if(lineStation.NextStation <= 0)
            {
                return lineStation;
            }
            try
            {
                AdjacentStation adj = GetAdjacentStation(a => a.Statoin1 == lineStation.Code && a.Station2 == lineStation.NextStation);
                lineStation.Time_ToNext = adj.Time;
                lineStation.Distance_ToNext = adj.Distance;
            }
            catch (ItemNotExeistExeption)
            { throw new LackOfDataExeption(DataType.AdjacentStation, lineStation.Code, lineStation.NextStation); }
            return lineStation;
        }

        private static LineStation AddStationName(this LineStation lineStation)
        {
            try 
            {
                lineStation.Name = DAL.GetStation(lineStation.Code).Name;
                return lineStation;
            }
            catch (DO.ItemNotExeistExeption)
            { throw new LackOfDataExeption(DataType.LineStation, lineStation.Code); }
        }

        public static AdjacentStation GetAdjacentStation(Predicate<AdjacentStation> predicat)
        {
            AdjacentStation returnVal = (from item in DAL.GetAllAdjacentStations()
                                         let adj = item.CloneNew(typeof(AdjacentStation))
                                         where predicat((AdjacentStation)adj)
                                         select (AdjacentStation)adj).FirstOrDefault();
            if (returnVal == null)
                throw new ItemNotExeistExeption(typeof(AdjacentStation), 000);
            return returnVal;
        }

        public static T GetBase<S, T>(Predicate<T> predicat) where S: class where T: class
        {
            Predicate<S> newPred = s => predicat((T) s.CloneNew(typeof(T)));

            object obj = GetDalObj(newPred);
            if (obj == null)
                return null;
            return (T)obj.CloneNew(typeof(T));
        }        
        
        public static T GetDalObj<T>(Predicate<T> predicate) where T: class
        {

            GetDeleget<T> getDeleget;

            Type type = typeof(GetDeleget<T>);
            MethodInfo[] methodList = DAL.GetType().GetMethods();

            foreach (MethodInfo method in methodList)
            {
                if (method.ReturnType == typeof(IEnumerable<T>) && method.GetParameters().Any())
                {
                    getDeleget = (GetDeleget<T>)Delegate.CreateDelegate(type, DAL, method);
                    return getDeleget(predicate).FirstOrDefault(); 
                }
            }
            return null;
        }

    }
}
