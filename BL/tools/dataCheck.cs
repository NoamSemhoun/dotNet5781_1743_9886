using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BL.tools
{
    class dataCheck<T>
    {
        delegate T GetDeleget(int id);
 
        bool isExeist(Type type, int id)
        {
            DalApi.IDAL dal = DalApi.DalFactory.GetDal();
            GetDeleget getDeleget; 


            MethodInfo[] methodList = dal.GetType().GetMethods();

            foreach (MethodInfo method in methodList)
            {
                if (method.ReturnType == type)
                {
                    getDeleget = (GetDeleget)method.CreateDelegate(typeof(GetDeleget));
                    try
                    {
                        getDeleget(id);
                        return true;
                    }
                    catch (DO.ItemNotExeistExeption)
                    { return false; }
                }                
            }
            return false;
        }




    }
}
