//using System;
//using DL;
//using DO;
//using DS;
//using DalApi;
//using System.Collections;

//namespace PlCon
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            //DalObject dal = new DalObject();
//            IDAL dal = new DalObject();

            

//            dal.AddBusOnTrip(new BusOnTrip { Id = 1, LicenceNum = 11111111});
//            dal.AddBusOnTrip(new BusOnTrip { Id = 2, LicenceNum = 22222222});
//            dal.AddBusOnTrip(new BusOnTrip { Id = 3, LicenceNum = 22222222});
//            dal.AddBusOnTrip(new BusOnTrip { Id = 4, LicenceNum = 22222222});


//            BusOnTrip b = dal.GetBusOnTrip(1);

//            Console.WriteLine(b.LicenceNum);

//            var l = dal.GetAllBusesOnTrip();

//            foreach (BusOnTrip b2 in l)
//                Console.WriteLine(b2.Id);

//            dal.DeleteBusOnTrip(4);

//            l = dal.GetAllBusesOnTrip();

//            foreach (BusOnTrip b2 in l)
//                Console.WriteLine(b2.Id);

//            l = dal.GetAllBusesOnTripBy(b => b.LicenceNum ==  22222222 );

//            dal.UpdateBusOnTrip(new BusOnTrip { Id = 3, LicenceNum = 55555555 });

//            l = dal.GetAllBusesOnTripBy(b => b.LicenceNum == 22222222);
//            foreach (BusOnTrip b2 in l)
//                Console.WriteLine(b2.Id);
   

 
 


//            Console.WriteLine("Hello World!");

//            Console.Read();
//        }
//    }
//}
