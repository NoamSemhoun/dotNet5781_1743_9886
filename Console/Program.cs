using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IDAL dal = DalFactory.GetDal();

            dal.AddLine(new DO.Line { Id = 12 });

            System.Console.WriteLine(dal.GetLine(12).Id);
            System.Console.ReadKey();   
        }
    }
}
