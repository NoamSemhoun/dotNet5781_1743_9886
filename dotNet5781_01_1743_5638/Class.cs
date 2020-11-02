using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_1743_5638
{
    public class Bus
    {
        const int FULLTANK = 1200;
        public readonly DateTime StartDate;
        private string license;
        private int km;
        public int Km { get => km; set => km = value; }
        public int Fuel { get; set; }
        public DateTime Checkup;

        public Bus()
        {
            Console.Write("Starting date: ");

            bool result = DateTime.TryParse(Console.ReadLine(), out StartDate);
            if (result == false)
            {
                throw new Exception("invalid StartDate string format");
            }
            
            Console.Write("License number: ");
            License = Console.ReadLine();
            Console.WriteLine("Is the bus was new at the purchase ? \n Y: Yes / N:No ");
            string reponse = Console.ReadLine();
            if (reponse == "Y")
                Checkup = StartDate;
            else if (reponse == "N")
            {
                Console.WriteLine("Please type the last maintenance date :");
                bool result1 = DateTime.TryParse(Console.ReadLine(), out Checkup);
                if (result1 == false)
                {
                    throw new Exception("invalid MaintenanceDate string format");
                }

            }
            else
            {
                throw new Exception("Invalid answer ");
            }

            Fuel = FULLTANK;

        }

        public string License
        {
            get
            {
                string first, middle, last;
                if (license.Length == 7)
                {
                    // xx-xxx-xx
                    first = license.Substring(0, 2);
                    middle = license.Substring(2, 3);
                    last = license.Substring(5, 2);
                    return string.Format("{0}-{1}-{2}", first, middle, last);
                }
                else
                {
                    // xxx-xx-xxx
                    first = license.Substring(0, 3);
                    middle = license.Substring(3, 2);
                    last = license.Substring(5, 3);
                    return string.Format("{0}-{1}-{2}", first, middle, last);
                }
            }

            private set
            {
                if ((StartDate.Year < 2018 && value.Length == 7) || (StartDate.Year >= 2018 && value.Length == 8))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (value[i] < '0' || value[i] > '9')
                            throw new Exception("license not valid");
                    }
                    license = value;
                }
                else
                {
                    throw new Exception("license not valid");
                }
            }
        }
        public int Refuel()
        {
            int fuelToAdd = FULLTANK - Fuel;

            if (fuelToAdd > 0)
            {
                Fuel = FULLTANK;
                return fuelToAdd;
            }
            return 0;
        }
        public void Maintenance()
        {
            Checkup = DateTime.Now;
            Km = 0;
        

        }

        public override string ToString()
        {
            return String.Format("license: {0,-10}, lastCheckupDate: {1}, km: {2}, fuel: {3}", License, Checkup, Km, Fuel);
        }

    }
}