using System;
using System.Reflection;
//using DO;

namespace DL
{
    static class Cloning
    {
       
        //third way - With Bonus // generic shallowed copy, properties only
        internal static T Clone<T>(this T original)
        {
            T copyToObject = (T)Activator.CreateInstance(original.GetType());
            //...

            foreach (PropertyInfo sourcePropertyInfo in original.GetType().GetProperties())
            {
                PropertyInfo destPropertyInfo = original.GetType().GetProperty(sourcePropertyInfo.Name);

                destPropertyInfo.SetValue(copyToObject, sourcePropertyInfo.GetValue(original, null), null);
            }

            return copyToObject;
        }

        //internal static DO.LineStation LineStationClone(this DO.LineStation original)
        //{
        //    return new DO.LineStation
        //    {
        //        Code = original.Code,
        //        LineId = original.LineId,
        //        LineStationIndex = original.LineStationIndex,
        //        NextStation = original.NextStation,
        //        PrevStation = original.PrevStation
        //    };
        //}


    }
}
