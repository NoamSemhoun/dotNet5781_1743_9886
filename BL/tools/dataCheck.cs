using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BL
{
    internal class dataCheck<T>
    {
        delegate IEnumerable<T> GetDeleget(Predicate<T> predicate);
        DalApi.IDAL dal = DalApi.DalFactory.GetDal();
        
        internal bool isExeist(Predicate<T> predicate)
        {
            
            GetDeleget getDeleget;

            Type type = typeof(GetDeleget);           
            MethodInfo[] methodList = dal.GetType().GetMethods();

            foreach (MethodInfo method in methodList)
            {
                if (method.ReturnType == typeof(IEnumerable<T>) && method.GetParameters().Any())
                {
                    getDeleget  = (GetDeleget)Delegate.CreateDelegate(type, null, method);

                    if (getDeleget(predicate).Any())
                        return true;
                    else
                        return false;
                }                
            }
            return false;
        }


        internal bool didNeedUpdaete(object obj ,Predicate<T> predicate)
        {
            GetDeleget getDeleget;


            MethodInfo[] methodList = dal.GetType().GetMethods();

            foreach (MethodInfo method in methodList)
            {
                if (method.ReturnType == typeof(IEnumerable<T>) && method.GetParameters().Any())
                {
                    getDeleget = (GetDeleget)Delegate.CreateDelegate(typeof(GetDeleget), null, method);

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
