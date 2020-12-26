using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IDAL dal = DalFactory.GetDal();

            dal.AddLine(new DO.Line { Id = 12 });

            System.Console.WriteLine(dal.GetLine(12).Id);



            dal.AddLine(new Line { Id = 35, Area = Areas.Center });
            dal.AddLine(new Line { Id = 36, Area = Areas.General });
            dal.AddLine(new Line { Id = 37, Area = Areas.General });
            dal.AddLine(new Line { Id = 38, Area = Areas.Center });
     


            Line l1 = dal.GetLine(1);

            System.Console.WriteLine(l1.Area);

            var l = dal.GetAllLines();

            foreach (Line b2 in l)
                System.Console.WriteLine(b2.Id);

            dal.DeleteLine(4);

            l = dal.GetAllLines();

            foreach (Line b2 in l)
                System.Console.WriteLine(b2.Id);

            l = dal.GetAllLinesBy(b2 => b2.Area == Areas.General);

            foreach (Line b2 in l)
                System.Console.WriteLine(b2.Id);

            dal.UpdateLine(new Line { Id = 3, Area = Areas.South });

            l = dal.GetAllLines();
            foreach (Line b2 in l)
                System.Console.WriteLine(b2.Id + " " + b2.Area);






            System.Console.WriteLine("Hello World!");

            System.Console.Read();
        }
    }
}
