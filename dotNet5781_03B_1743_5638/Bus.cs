using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;

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
    public class Bus:INotifyPropertyChanged
    {
        private int percent = 100;//For the progressbar Value
        private TimeSpan timeBeforeArrival;//For the timer when in Road
        public float timeToRoad;
        public event PropertyChangedEventHandler PropertyChanged;  
        private float speed;
        const int FULLTANK = 1200;
        private DateTime startDate;
        public string StartDate { get => startDate.ToString(); set => startDate = DateTime.Parse(value); }
        private string license;
        private float km;
        public float Km { get => km; set => km = value; }
        public string namechauffeur;
        private Status status;
        public float Fuel { get; set; }
        public DateTime Checkup;
        private float kmAfterLastMaintenance;
        public float KmAfterLastMaintenance { get => kmAfterLastMaintenance; set => kmAfterLastMaintenance = value; }
        private static Random LicenseKmFuel = new Random();//this variable will be used For our random value initialization
        private static RandomDateTime randomDate = new RandomDateTime();//this too ,for the initialization at the beggining in the MainWindow
        private int seatNumber;
        public int SeatNumber { get => seatNumber; set => seatNumber = value; }
       
     
        public Bus()//ctor for random 
        {

            startDate = randomDate.Next();
            Checkup = DateTime.Now;
            if (startDate.Year < 2018)
            {
                License = LicenseKmFuel.Next(1000000, 10000000).ToString();
            }
            else
            {
                License = LicenseKmFuel.Next(10000000, 100000000).ToString();
            }
            km = LicenseKmFuel.Next(0, 20000);
            Fuel = LicenseKmFuel.Next(10, 1200);
            speed = LicenseKmFuel.Next(20, 51);

        }
        public Bus(string date, string license, string newone, string kilometrages, string checkup, string seat, string drivername, string kmAMaintenance)
        {

            bool result = DateTime.TryParse(date, out startDate);
            if (result == false)
            {
                throw new Exception("invalid StartDate string format");
            }
            else if (newone == "N")//If the bus is new so the date we added to the company,it is clean so the checkupdate will be the current date 
            {
                Checkup = DateTime.Now;
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
            speed = LicenseKmFuel.Next(20, 51);
        }
        public Bus(Bus bus)
        {

            Checkup = bus.Checkup;
            license = bus.License;
            Fuel = bus.Fuel;
            status = bus.status;
            namechauffeur = bus.namechauffeur;
            km = bus.km;
            kmAfterLastMaintenance = bus.KmAfterLastMaintenance;
            StartDate = bus.StartDate;
            seatNumber = bus.SeatNumber;
            speed = bus.speed;
        }
        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string returnStatus
        {
            get => status.ToString();
            set
            {
                status = (Status)Enum.Parse(typeof(Status), value);
            }

        }
      
        public int Percent//Value of our progressbar need to be bind and changed in synhcronization from the mainWindow
        {
            get { return percent; }
            set
            {
                percent = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Percent"));

            }
        }
        public TimeSpan TimeBeforeArrival
        {
            get { return timeBeforeArrival; }
            set
            {
                timeBeforeArrival =value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeBeforeArrival"));
            }
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
                if ((startDate.Year < 2018 && value.Length == 7) || (startDate.Year >= 2018 && value.Length == 8))
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
        public float Speed { get => speed; set => speed = value; }
        public bool needMaintenance()
        {
            if ((DateTime.Compare(Checkup.Date, DateTime.Now.AddYears(-1))) < 0 || kmAfterLastMaintenance == 20000)
            {
                return true;
            }
            else
                return false;
        }
        public void checkStatus()
        {
            if (needMaintenance())//Only the bus who have to pass in maintenance cant be ready
            {
                returnStatus = "NeedMaintenance";
                Percent = 0;
            }

            else if (Fuel == 0)
            {
                returnStatus = "NeedRefuel";
                Percent = 0;
            }
            else if(needMaintenance()&&Fuel==0)//The on who needs to refuel and maintenance ,pass in NeedMaintenance mode
            {
                returnStatus = "NeedMaintenance";
            }

        }
        public override string ToString()
        {
            return String.Format("license: {0,-10}, StartDate: {1}, km: {2}", License, StartDate, Km);
        }

    }
}
