using System;
using System.Reflection;
//using DO;

namespace DalObject
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


    }
}
