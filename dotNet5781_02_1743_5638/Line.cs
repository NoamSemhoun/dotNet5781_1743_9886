using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Services;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    public  enum Area { General, North, South, Center, Jerusalem }
   public  class Line : IEnumerable  
    {
       
       private int busLineNumber;
        public StationLine firstStation;
        public StationLine lastStation;
        public Area area;
        public  List<StationLine> listStations;  

        private static int sampleShelterNumber = 100000;    // code at 6 number
        private static int sampleLineNumber = 0;
        #region Constructors
       public int BusLineNumber { get => busLineNumber; set => busLineNumber = value; }
        public Line()//This ctor is only for the initialisation for the targuil ,so no parameter,only random values   
        {
        
            listStations = new List<StationLine>();  
            for (int i = 0; i < 4; ++i, ++sampleShelterNumber)
            {
                if (i == 0)
                {
                    StationLine s = new StationLine(sampleShelterNumber);
                    TimeSpan zero = new TimeSpan(0, 0, 0);
                    s.Temps = zero;
                    s.Distance = 0;
                    listStations.Add(s);
                }
                else
                {
                    listStations.Add(new StationLine(sampleShelterNumber));
                }
            }
            busLineNumber = sampleLineNumber++;
            area = Area.Jerusalem;
            firstStation = listStations.First();
            lastStation = listStations.Last();
        }
        public Line(Line l)//This ctor is a copy ctor thats I use in my getStation function in HandleCollectionBus ,it was easier to pass trough a copy ctor for my needs 
        {
            listStations = new List<StationLine>();
            busLineNumber = l.busLineNumber;
            area = l.area;
            listStations = l.listStations;
        }
       
        public Line(int a)   //This ctor will be used in all Adding line ,there's lot of data because ..no more random values,only the data of the User
        {
                busLineNumber = a;
                Console.WriteLine("Enter the area of this new Bus Line :\n" +
                                 "0 to General,\n" +
                                 "1 to North,\n" +
                                 "2 to South,\n" +
                                 "3 to Center\n" +
                                 "or 4 to Jerusalem\n");
            bool ok = int.TryParse(Console.ReadLine(), out int temp);
            while (temp<0||temp>4)
                {
                    Console.WriteLine("Error of Input,try again :");
                    temp = int.Parse(Console.ReadLine());
                }
                area = (Area)temp;
                listStations = new List<StationLine>();  
                Console.WriteLine("Enter the shellterNumber of your first station :");
            bool ok2 = int.TryParse(Console.ReadLine(), out int first);
            if (!ok2) { throw new ExceptionTarguil2("Enter a number only !"); }

            StationLine First = new StationLine(first);
            TimeSpan timeoftheFirst = new TimeSpan(0, 0, 0);
            First.Temps = (timeoftheFirst);
            First.Distance = 0;
                listStations.Add(First);
                Console.WriteLine("Enter the shellterNumber of your last station :");
            bool ok3 = int.TryParse(Console.ReadLine(), out int last);
            if (!ok3) { throw new ExceptionTarguil2("Enter a number only !"); }
            while (last==first)
            {
                Console.WriteLine("You can't type the same station for the departure and the arrival ! Type again the station of arrival :");
                last = int.Parse(Console.ReadLine());
            }
                StationLine Last = new StationLine(last);
                listStations.Add(Last);
                firstStation = listStations.First();
                lastStation = listStations.Last();
        }
        public Line(int a,string Null)//This ctor will help us for the return path Line because here we dont need to know what is the Area of the user wants ,the string parameter is here just to make a difference beetween the previous ctor 
        {
            busLineNumber = a;
            listStations = new List<StationLine>();
            Console.WriteLine("Enter the shellterNumber of your first station :");
            bool ok = int.TryParse(Console.ReadLine(), out int first);
            if (!ok) { throw new ExceptionTarguil2("Enter a number only !"); }
            firstStation = new StationLine(first);
            listStations.Add(firstStation);

            TimeSpan Premier = new TimeSpan(0, 0, 0);
            firstStation.Temps = Premier;
            Console.WriteLine("Enter the shellterNumber of your last station :");
            bool ok2 = int.TryParse(Console.ReadLine(), out int last);
            if (!ok2) { throw new ExceptionTarguil2("Enter a number only !"); }
            lastStation = new StationLine(last);
            listStations.Add(lastStation);
        }
        #endregion

        #region FONCTION
        public  bool IsNumberStationExists(int numStation) =>      
           listStations.Exists(station => station.ShelterNumber == numStation);
        
        public void addStation()//This function add station beetwween the first and the last Stations ,one after the other or not  
        {
          Console.WriteLine("Enter the number of the station to add :");
            bool ok = int.TryParse(Console.ReadLine(), out int number);
            if (!ok) { throw new ExceptionTarguil2("Enter a number only !"); }
            Console.WriteLine("Tape 0 to insert your station by default ,or Tape the ShellterNumber of the station where do you want to add the new One just before :");
            bool ok2 = int.TryParse(Console.ReadLine(), out int response);
            if (response == 0)
            {
                if (!IsNumberStationExists(number))
                {
                    int AvailabeIndex = listStations.Count() - 1;
                    StationLine newStation = new StationLine(number);
                    listStations.Insert(AvailabeIndex, newStation);
                    Console.WriteLine("success !");

                }
                else
                    throw new ExceptionTarguil2("This station already exists !");
            }
            else if (IsNumberStationExists(response))
            {

                int Indexto = listStations.FindIndex(station => station.ShelterNumber == response);
                StationLine newStation = new StationLine(number);
                listStations.Insert(Indexto, newStation);
                Console.WriteLine("success !");


            }
            else
                throw new ExceptionTarguil2("This station doesn't exists !");
          
        }
       public void setArea(int l)
        {
            area = (Area)l;
        }
        public int getArea()
        {
            return (int)area;
        }
        public Area GetArea()
         {
        return area;
         }
        public  void deleteStation() //Same as adding function
        {
            Console.WriteLine("Enter the number of the station to delete :");
            bool ok = int.TryParse(Console.ReadLine(), out int stationTodelete);
            if (!ok) { throw new ExceptionTarguil2("Enter a number only !"); }
            if (IsNumberStationExists(stationTodelete))
            {
                listStations.RemoveAll(station => station.ShelterNumber == stationTodelete);
            }
            else
            {
                throw new ExceptionTarguil2("This station doesn't exists !");
            }

        }
   
        public TimeSpan TimeBetween(StationLine s1,StationLine s2)//Function take 2 Stations and return a TimeSpan value that corresponds on the time elapsed between
        {
            if (IsNumberStationExists(s1.ShelterNumber) && IsNumberStationExists(s2.ShelterNumber))
            {
                TimeSpan Timer = new TimeSpan(0, 0, 0);
                int begin = listStations.FindIndex(station => station.ShelterNumber == s1.ShelterNumber);
                int end = listStations.FindIndex(station => station.ShelterNumber == s2.ShelterNumber);
                for (; begin < end + 1; begin++)
                {
                    Timer += listStations[begin].Temps;
                }
                return Timer;
            }
            else
                throw new ExceptionTarguil2("One of your staton in input doesn't exists !");
        }
        public double DistanceBetween (StationLine s1,StationLine s2)//Function take 2 stations and return the distance bewteen them 
        {
            if (IsNumberStationExists(s1.ShelterNumber) && IsNumberStationExists(s2.ShelterNumber))
            {
                double distanciation = 0;
                int begin = listStations.FindIndex(station => station.ShelterNumber == s1.ShelterNumber);
                int end = listStations.FindIndex(station => station.ShelterNumber == s2.ShelterNumber);
                for (; begin<end+1; begin++)
                {
                    distanciation += listStations[begin].Distance;
                }
                return distanciation;
                
            }
            else
                throw new ExceptionTarguil2("One of your staton in input doesn't exists !");
            
        }
        
        public Line TatMasloul(StationLine s1,StationLine s2)//This function returns a Line object and in his list of the stations there's all the station between the 2 stations passed in parameters
        {
            if (IsNumberStationExists(s1.ShelterNumber) && IsNumberStationExists(s2.ShelterNumber))
            {
                Line l1 = new Line(0,"null");
                int begin = listStations.FindIndex(station => station.ShelterNumber == s1.ShelterNumber);
                int end= listStations.FindIndex(station => station.ShelterNumber == s2.ShelterNumber);
                for (;begin<end+1;begin++)
                {
                    l1.listStations.Add(listStations[begin]);
                }
                return l1;
            }
            else
                throw new ExceptionTarguil2("One of your staton in input doesn't exists !");
        }

        public int CompareTo(object obj,StationLine Start,StationLine Stop)//This function compare the time elapsed in 2 station ,between 2 Line 
        {
            
            if(Start.ShelterNumber==Stop.ShelterNumber)
            {
                throw new ExceptionTarguil2("Impossible ,your Start location and Stop location are the same ");
               
            }
            if (obj == null) return 1;
            Line otherLine = obj as Line;
            if (otherLine != null)
                return this.TimeBetween(Start,Stop).CompareTo(otherLine.TimeBetween(Start,Stop));
            else
                throw new ExceptionTarguil2("Object is not a Line");
        }
        #endregion

        #region Admin

        public IEnumerator GetEnumerator()
        {
            return listStations.GetEnumerator();
        }

        public override string ToString()
        {
            string result = $"Bus Line Number: {busLineNumber}, in {area}, Stations : \n";
            foreach (StationLine station in listStations)
            {
               result+= station.ToString() + "\n";
            }
            return result;
          
        }

        #endregion
    }
}


