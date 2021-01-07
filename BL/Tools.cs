using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BlAPI;

namespace BL
{
    
    

    
 

    public static class Tools
    {
        
        public static bool isEqual(this object obj, object other)
        {
            if(obj.GetType().Name != other.GetType().Name)
            {
                return false;
            }
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (!prop.GetValue(obj).Equals(prop.GetValue(other)))
                    return false;
            }
            return true;
        }

        
    }



    class MyPredicat<T>
    {
        private PropertyInfo[] propList;
        private T obj;
        public MyPredicat (T o, params PropertyInfo[] pl)
        {
            propList = pl;
            obj = o;
        }

        
        
        public bool MyPredicatFunc(T otherObj)
        {
            foreach(PropertyInfo prop in propList)
            {
                if ((int)prop.GetValue(obj) != (int)prop.GetValue(otherObj))
                    return false;
            }
            return true;
        }
        
    }
}
