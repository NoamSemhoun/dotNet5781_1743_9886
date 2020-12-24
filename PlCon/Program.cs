using System;
using DL;
using DO;
using DS;
using DalApi;

namespace PlCon
{
    class Program
    {
        static void Main(string[] args)
        {
            //DalObject dal = new DalObject();
            IDAL dal = new DalObject();

            Bus b1 = new Bus { LicenseNum = 11111111 };

            dal.AddBus(new Bus { LicenseNum = 11111111 });
            dal.AddBus(new Bus { LicenseNum = 22222222 });
            dal.AddBus(new Bus { LicenseNum = 33333333 });

            //Console.WriteLine(dal.check());

            Bus b = dal.GetBus(11111111);

            Console.WriteLine(b.LicenseNum);


            Console.WriteLine("Hello World!");

            Console.Read();
        }
    }
}
