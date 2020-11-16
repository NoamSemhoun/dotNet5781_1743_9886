using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    class HandleCollectionBus : Line
    {
        public List<Line> listLine;

        public bool CheckFirstLast(Line l2) => listLine.Exists(Line => Line.listStations.First().ShelterNumber == l2.listStations.Last().ShelterNumber && Line.listStations.Last().ShelterNumber == l2.listStations.First().ShelterNumber);
        public HandleCollectionBus()
        {
            listLine = new List<Line>();
          for(int a=0;a<10;a++)
            {
                Line l = new Line();
                listLine.Add(l);
            }
        }
        public bool IsNumberLineExists(int verif) =>
        listLine.Exists(Line => Line.BusLineNumber == verif);

        public void AddStation(int ligne)
        {
            int occurence = listLine.Where(line => line.BusLineNumber == ligne).Count();
            if (occurence == 1)
            {
                foreach (Line line in listLine)
                {
                    if (line.BusLineNumber == ligne)
                    {
                        line.addStation();
                    }
                }
            }
            else
            {
                Console.WriteLine("There's more of one line with this number,specify the line by the number of the First Station of it :");
                int first = int.Parse(Console.ReadLine());
                for (int a = 0; a < listLine.Count(); a++)
                {
                    if (listLine[a].listStations.First().ShelterNumber == first && listLine[a].BusLineNumber == ligne)
                    {
                        listLine[a].addStation();
                    }
                }

            }
        }
        public List<StationLine> getStation()
        {
            List<StationLine> retour = new List<StationLine>();
            foreach (Line line in listLine)
            {
                foreach (StationLine stationLine in line.listStations)
                {
                    if (!retour.Exists(station => station.ShelterNumber == stationLine.ShelterNumber))
                    {
                        retour.Add(stationLine);
                    }
                }
            }
            return retour;
        }
        public void DeleteStation(int ligne)
        {
            if (listLine.Find(line => line.BusLineNumber == ligne).listStations.Count == 2)
            {
                throw new ExceptionTarguil2("but you can't let less than 2 stations in this line !");
                return;
            }
            int occurence = listLine.Where(line => line.BusLineNumber == ligne).Count();
            if (occurence == 1)
            {
                foreach (Line line in listLine)
                {
                    if (line.BusLineNumber == ligne)
                    {
                        line.deleteStation();
                    }
                }
            }
            else
            {
                Console.WriteLine("There's more of one line with this number,specify the line by the number of the First Station of it :");
                int first = int.Parse(Console.ReadLine());
                for (int a = 0; a < listLine.Count(); a++)
                {
                    if (listLine[a].listStations.First().ShelterNumber == first && listLine[a].BusLineNumber == ligne)
                    {
                        listLine[a].deleteStation();
                    }
                }

            }
        }
        public void addLine()
        {
            Console.WriteLine("Enter the number of the line to add :");
            int number = int.Parse(Console.ReadLine());

            if (IsNumberLineExists(number))
            {
                if (listLine.Where(ligne => ligne.BusLineNumber == number).Count() == 2)
                {
                    throw new ExceptionTarguil2("This line already exists in both directions !");
                    return;
                }

                Console.WriteLine("This line already exists, you just can type its second way \n");
                Line l = new Line(number, "NULL");
                l.setArea(listLine.Find(line => line.BusLineNumber == number).getArea());
                if (CheckFirstLast(l))
                {

                    listLine.Add(l);
                }
                else
                {
                    throw new ExceptionTarguil2("ERROR ! you didn't listen to me !");
                    return;
                }
            }

            else
            {
                Line l = new Line(number);
                listLine.Add(l);
            }
        }
        public void deleteLine()
        {
            Console.WriteLine("Enter the number of the line to delete");
            int linetoDelete = int.Parse(Console.ReadLine());
            if (IsNumberLineExists(linetoDelete))
            {
                int count = listLine.Where(Line => Line.BusLineNumber == linetoDelete).Count();
                if (count == 2)
                {
                    Console.WriteLine("The list contains the line in the 2 directions , type the ShelterNumber of the first Stop of the Line to delete :");
                    bool ok = int.TryParse(Console.ReadLine(), out int linetodelete);
                    if (!ok) { throw new ExceptionTarguil2("Enter a number !"); }
                    listLine.RemoveAll(Line => Line.firstStation.ShelterNumber == linetodelete);
                }
                else
                    listLine.RemoveAll(Line => Line.BusLineNumber == linetoDelete);
            }
            else { throw new ExceptionTarguil2("This Line doesn't exists !"); }
        }
        public void ThroughStation(int a)
        {
            int count = 0;
            Console.WriteLine("Here's the line(s) passing through this stop :");
            foreach (Line line in listLine)
            {
                if (line.listStations.Exists(stationLine => stationLine.ShelterNumber == a))
                {
                    Console.WriteLine(line.BusLineNumber);
                    count++;
                }
            }
            if (count == 0)
            {
                throw new ExceptionTarguil2("There's no bus passing through this stop !");
            }
        }
        public override string ToString()
        {
            string result = "Voici la liste des bus :\n";
            foreach (Line line in listLine)
            {
                result += line.ToString();
            }
            return result;
        }
        public void Faster()
        {
            if (listLine.Count() != 0)
            {

                Console.WriteLine("Enter your current Station :");

                bool ok = int.TryParse(Console.ReadLine(), out int current);
                if (!ok) { throw new ExceptionTarguil2("Enter a number only !"); }
                int count = listLine.Where(line => line.IsNumberStationExists(current) == true).Count();
                if (count == 0)
                {
                    throw new ExceptionTarguil2("This number station doesn't exists !");

                }
                StationLine Current = new StationLine(current);
                Console.WriteLine("Enter your destination Station :");
                bool ok2 = int.TryParse(Console.ReadLine(), out int destination);
                if (!ok2) { throw new ExceptionTarguil2("Enter a number only !"); }
                count = listLine.Where(line => line.IsNumberStationExists(destination) == true).Count();
                if (count == 0)
                {
                    throw new ExceptionTarguil2("This number Station doesn't exists ");

                }
                StationLine Destination = new StationLine(destination);
                int size = listLine.Count();
                for (int i = 1; i < size; i++)
                {
                    for (int j = 0; j < (size - i); j++)
                    {
                        if (listLine[j].CompareTo(listLine[j + 1], Current, Destination) == 1)
                        {
                            Line temp = new Line(listLine[j]);
                            listLine[j] = listLine[j + 1];
                            listLine[j + 1] = temp;
                        }
                    }
                }
                Console.WriteLine("Here's all your possibilities,from the faster to the longer ");
                foreach (Line line in listLine)
                {
                    Console.WriteLine("Bus number {0} make it in {1}", line.BusLineNumber, line.TimeBetween(Current, Destination));
                }
            }
            else
                throw new ExceptionTarguil2("There's no line saved !");
        }

    }
}
