using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BL
{
    static class Tools
    {
        static bool isEqual<T>(this T current, T other)
        {
            Type type = current.GetType();
            foreach (PropertyInfo prop in current.GetType().GetProperties())
            {
                if (prop.GetValue(current) != prop.GetValue(other))
                    return false;
            }
            return true;
        }
    }
}
