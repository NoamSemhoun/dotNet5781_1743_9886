using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BL
{
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
