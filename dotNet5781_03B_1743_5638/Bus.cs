﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_1743_5638
{
    class RandomDateTime
    {
        DateTime start;
        Random gen = new Random();
        int range;

        public RandomDateTime()
        {
            start = new DateTime(1995, 1, 1);
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
    }
    public class Bus
    {
        const int FULLTANK = 1200;
        public DateTime StartDate;
        private string license;
        private int km;
        public string namechauffeur;
        public Status status;
        public int Km { get => km; set => km = value; }
        public int Fuel { get; set; }
        public DateTime Checkup;
        private int kmAfterLastMaintenace;
        private static Random LicenseKmFuel = new Random();
        private static RandomDateTime randomDate = new RandomDateTime();
        private int seatNumber;
        public int SeatNumber { get => seatNumber; set => seatNumber = value; }
        public int KmAfterLastMaintenance { get => kmAfterLastMaintenace; set => kmAfterLastMaintenace = value; }
        private string StartingDate;
        public string startingdate { get => StartDate.ToString(); }
        public Bus()//ctor for random 
        {

            StartDate = randomDate.Next();
            Checkup = DateTime.Now;
            if (StartDate.Year < 2018)
            {
                License = LicenseKmFuel.Next(1000000, 10000000).ToString();
            }
            else
            {
                License = LicenseKmFuel.Next(10000000, 100000000).ToString();
            }
            km = LicenseKmFuel.Next(0, 20000);
            Fuel = LicenseKmFuel.Next(0, 1200);

        }
        public Bus(string date, string license, string newone, string kilometrages, string checkup, string seat, string drivername, string kmAMaintenance)
        {

            bool result = DateTime.TryParse(date, out StartDate);
            if (result == false)
            {
                throw new Exception("invalid StartDate string format");
            }
            else if (newone == "N")
            {
                Checkup = StartDate;
                km = 0;
            }
            else if (newone == "O")
            {
                result = DateTime.TryParse(checkup, out Checkup);
                if (result == false)
                {
                    throw new Exception("invalid Checkup string format");
                }
                km = int.Parse(kilometrages) + int.Parse(kmAMaintenance);
                KmAfterLastMaintenance = int.Parse(kmAMaintenance);

            }
            else
                throw new Exception("Error of answer to New/Old !");
            try
            {
                License = license;
            }
            catch (Exception e)
            {
                throw new Exception("invalid License format");
            }

            Fuel = FULLTANK;
            status = Status.Ready;
            bool flag = int.TryParse(seat, out seatNumber);
            if (flag == false)
                throw new Exception("Even if there's no place avilable you have to type 0 ");
            namechauffeur = drivername;

        }
        public Bus(Bus bus)
        {

            Checkup = bus.Checkup;
            license = bus.License;
            Fuel = bus.Fuel;
            status = bus.status;
            namechauffeur = bus.namechauffeur;
            km = bus.km;
            kmAfterLastMaintenace = bus.KmAfterLastMaintenance;
            StartDate = bus.StartDate;
            seatNumber = bus.SeatNumber;
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

        public bool needMaintenance()
        {
            if ((DateTime.Compare(Checkup.Date, DateTime.Now.AddYears(-1))) < 0 || Km >= 20000)
            {
                return true;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return String.Format("license: {0,-10}, StartDate: {1}, km: {2}", License, StartDate, Km);
        }

    }
}
