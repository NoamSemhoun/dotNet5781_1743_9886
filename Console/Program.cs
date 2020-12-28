using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using BL;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //BlAPI.IBL bl = new BLImp();
            //bl.AddBus(new BO.Bus { LicenseNum = 1111111, Status = BO.BusStatus.InRefuling });

            IDAL dal = DalApi.DalFactory.GetDal();

            dal.AddBus(new Bus{ LicenseNum = 12345678, Status = BusStatus.InMaintenance});

            var l = dal.GetAllBus();

            foreach (Bus b in l)
                System.Console.WriteLine(b.LicenseNum + " " + b.Status);

            dal.UpdateBus(12345678, b => b.Status = BusStatus.Available);

            l = dal.GetAllBus();

            foreach (Bus b in l)
                System.Console.WriteLine(b.LicenseNum + " " + b.Status);

            System.Console.ReadLine();
        }
    }
}
