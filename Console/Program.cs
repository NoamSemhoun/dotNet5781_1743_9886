using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BO;
using BL;
using BlAPI;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
               



            //IBL bL = BLFactory.GetBL();

            //var l = bL.GetAllBuses();
            //foreach(Bus b in l)
            //    System.Console.WriteLine(b.LicenseNum + "  " + b.Status);

            //bL.AddBus(new Bus { LicenseNum = 22222222, Status = BusStatus.InRefuling, Fuel = 100, Date_LastMaintenance = new DateTime(2019, 2,23), Km_LastMaintenance = 20000 });


            //bL.MaintenanceBus(22222222);

            //print(bL);

            //bL.DeleteBus(22222222);

            //print(bL);

            IDAL dal = DalFactory.GetDal();

            //foreach (DO.Line l in dal.GetAllLines())
            //    System.Console.WriteLine(l);
        
            foreach (DO.LineStation l in dal.GetAllLineStations())
                System.Console.WriteLine(l);

            foreach (DO.AdjacentStation a in dal.GetAllAdjacentStations())
                System.Console.WriteLine((a));
            System.Console.ReadLine();        
        }

    }
}
