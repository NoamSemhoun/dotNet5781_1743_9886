using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;

namespace dotNet5781_03B_1743_5638
{
    public class Bus:INotifyPropertyChanged
    {
        // To do ;

        private static Random random = new Random(); // to Liscence

        //   Bus :
        public string license_str; // to the format to string
        //private int liscence;

        public DateTime DateStart;
               //private DateTime date; 
        public string DateStart_str { get => DateStart.ToString("dd/MM/yyyy"); set => DateStart = DateTime.Parse(value); }

        public double Fuel { get; set; }

        public double KmTotal { get; set; } // Kilometrage total
        public string NameDriver = "Mr Vizen Arie"; // default
        public float speed; // default speed
        public int seat { get; set; } // in a bus

        public Status Status { get; set; } // enum

          // Maintenance    :            TIPOUL 
        public DateTime DateOfMaintenance { get; set; } // The last Maintenance (checkup)
        public double Km_remaining { get; set; }   // before the next maintenance 
                                                   // maintenance must be done every year or every 2000km

        private float kmAfterlastMaintenance;

        private int percent = 100;//For the progressbar Value

        public event PropertyChangedEventHandler PropertyChanged;
        private Status status;
        private TimeSpan timeBeforeArrival;     //For the timer when in Road
        public float timeToRoad;



        #region Ctor BUS :

        public float KmAfterLastMaintenance { get => kmAfterlastMaintenance; set => kmAfterlastMaintenance = value; }
        public Bus()   // ctor for the initialisation randomalit , Of 10 New buses :
        {
            DateStart = new DateTime(random.Next(1995, 2021), random.Next(1,13), random.Next(1,28) ); //The compagny has existed since 1995
                                                                                            // 28 because february have 28 days...
            if (DateStart.Year < 2018)
            {
                License = random.Next(1000000, 9999999).ToString(); // 7 digits   XX-XXX-XX      // TO STRING !
            }
            else License = random.Next(10000000, 99999999).ToString(); // 8 digits   XXX-XX-XXX

            Fuel = 1200;
            Status = Status.READY;  // aaa
            KmTotal = random.Next(0,2000); // 1 to 1000 km au compteur
            DateOfMaintenance = DateTime.Now;  // it's a new bus
            if (kmAfterlastMaintenance <= 20000)
            {
                Km_remaining = 20000 - kmAfterlastMaintenance;   // Maintenance = every 2000 km  or  every year
            }
            else
                Km_remaining = 0;
            seat = 50;
            speed = random.Next(20, 51);
            KmAfterLastMaintenance = 0;
        }

        public Bus(DateTime date, int mylicense, double myFuel, string drivername, int myKmTotal,float kmAfterLastMaintenance,DateTime lastCheckupDate, int myseat)
        {
           
            DateStart = date;
            License = mylicense.ToString(); 
            if (mylicense > 10000000 && DateStart.Year < 2018 || mylicense < 10000000 && DateStart.Year > 2018)
            {
               throw new Exception("Incorrect Liscense format (year - digits)");
            }
            KmAfterLastMaintenance = kmAfterLastMaintenance;
            DateOfMaintenance = lastCheckupDate; // all buses are undergoing maintenance before they are put into service,  => .Now
            KmTotal = myKmTotal; // New Bus
            if (kmAfterlastMaintenance <= 20000)
            {
                Km_remaining = 20000 - kmAfterlastMaintenance;   // Maintenance = every 2000 km  or  every year
            }
            else
                Km_remaining = 0; // if Km total = 3500, km remaining BEFORE next maintenance = 500
            Fuel = myFuel;
             // GEREEEEE     KM  MAINTENANCE ETCC ////////////------------
            seat = myseat;
            NameDriver = drivername;
            speed = random.Next(20, 51);
        }



        #endregion






        #region Fonctions :


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
                timeBeforeArrival = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeBeforeArrival"));
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
    public void setKmRemaining()
        {
            if (kmAfterlastMaintenance <= 20000)
            {
                Km_remaining = 20000 - KmAfterLastMaintenance;
            }
            else
                Km_remaining = 0;
        }


        public void checkStatus()
        {
            if ((DateTime.Compare(DateOfMaintenance.Date, DateTime.Now.AddYears(-1))) < 0 || kmAfterlastMaintenance>20000)
            {
                returnStatus = "NEED_MAINTENANCE";
                Percent = 0;
            }
               //Only the bus who have to pass in maintenance cant be ready
           
            else if (Fuel < 8 )   // no risk 
            {
                returnStatus = "NEED_REFUEL";
                Percent = 0;
            }
          

        }
        #endregion


        #region TO STRING
        public override string ToString() // arranger
        {
            return String.Format("License: {0,-10}, lastCheckupDate: {1}, Total km: {2}, fuel: {3}"  , license_str, DateStart, KmTotal, Fuel);
            // base.ToString();
        }


        public string License
        {
            get
            {
                string first, middle, last;
                if (license_str.Length == 7)
                {
                    // xx-xxx-xx
                    first = license_str.Substring(0, 2);
                    middle = license_str.Substring(2, 3);
                    last = license_str.Substring(5, 2);
                    return string.Format("{0}-{1}-{2}", first, middle, last);
                }
                else
                {
                    // xxx-xx-xxx
                    first = license_str.Substring(0, 3);
                    middle = license_str.Substring(3, 2);
                    last = license_str.Substring(5, 3);
                    return string.Format("{0}-{1}-{2}", first, middle, last);
                }
            }

            private set
            {
                if ((DateStart.Year < 2018 && value.Length == 7) || (DateStart.Year >= 2018 && value.Length == 8))
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (value[i] < '0' || value[i] > '9')
                            throw new Exception("license not valid");
                    }
                    license_str = value;
                }
                else
                {
                    throw new Exception("license not valid");
                }
            }
        }

    

        #endregion

    }
    }








