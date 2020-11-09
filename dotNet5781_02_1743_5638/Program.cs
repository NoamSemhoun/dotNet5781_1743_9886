// TARGIL 2   in same solution
//   Noam

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    class Program
    {

        //expl FOr each :
        //BusLine busLine = new BusLine();
        //foreach (var line in busLine)
        //{

        //}

        //foreach (string element in list)
        //{
        //    Console.WriteLine(element.ToUpper());
        //}

        static List<Line> listLines;

        static void Init()  // initialisation : 40 station in 10line (all in Jerusalem)
        {
            listLines = new List<Line>();
            for (int i = 0; i < 10; i++)
            {
                listLines.Add(new Line());
            }
            //Line l1 = listLines.First();
            //Line l2 = listLines.Last();

            //l1 > l2;  comparable
        }

        static void Main(string[] args)
        {
            Init();  // Initialisation
            int choice;
            do
            {
                Console.WriteLine("* MENU * : Please Enter your choice : \n\n " +
                "\t1 to add a New Line,\n" +
                "\t2 to add a New Station in a Line\n" +
                "\t3 to delete a Line\n" +
                "\t4 to delete a Station from a Line\n" +
                "\t5 to Search Lines in a Station\n" +
                "\t6 to Find the route between 2 Station\n" +   ////////
                "\t7 to Print all Lines\n" +
                "\t8 to Print all the Stations (and their Lines)\n" +
                "\t0 to Exit ");

                bool ok = false;
                ok = int.TryParse(Console.ReadLine(), out choice);
                while (!ok || choice > 8 || choice < 0) // if it's KELET ERROR
                {
                    Console.WriteLine("\nPlease enter a Number between 0 to 9\n");
                    ok = int.TryParse(Console.ReadLine(), out choice);
                }

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Thank You, Good Bye !");
                        break;

                    case 1:  // ADD NEW LINE
                     
                          Console.WriteLine("Enter the number of the new Bus Line");
                        int a= int.Parse(Console.ReadLine());
                        listLines.Add(new Line(a));
                        
                        // Console.WriteLine(L1);
                        break;

                    case 2:  // // to add a New Station in a Line
                        
                        Console.WriteLine("Enter the Number of the Line :");
                        int NumLine = int.Parse(Console.ReadLine());
                        //do
                        //{
                        //    try
                        //    {
                                Console.WriteLine("Enter the Number of the Station to add :");
                                int NumStation = int.Parse(Console.ReadLine());
                                
                                Station S1 = new StationLine(NumStation); 
                        // create the station 
                       //}
                       //catch (ArgumentException e)
                       //{
                       //    Console.WriteLine(e.Message);
                       //    ok = false;
                       //}

                        //} while (!ok);

                      //  Line.addline(NumLine,  S1); // Ajouter a la liste station
                        
                        // Console.WriteLine($"New station added successfully:\n" + S1);
                        break;

                    case 3:   // delete a Line
                        Console.WriteLine("Enter the number Line to delete :");
                        try
                        {
                            int numLine =int.TryParse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("No existing Line, Error ");
                        }

                       //  Line.deleteaLine(NumLine);
                        break;

                    case 4:
                        Console.WriteLine("Enter the Station to delete :");
                        int.TryParse(Console.ReadLine(), out int num);
                       // Line.deleteStation(num);
                        break;
                    case 5:
                        Console.WriteLine("Enter the line to search and the stations in which it is located:");
                        //Line.search(Line L1);
                        //  search(line, station);
                        break;

                    case 6:
                        break;
                    case 7:  //Print all the Stations(and their Lines)

                        //foreach(list<StationLine> list in Line)
                        //{
                        //    Console.WriteLine(list);
                        //}

                        // printLines();
                        break;
                    case 8:
                        foreach (Line line in listLines)
                        {
                            Console.WriteLine(line);
                        }
                        //  printStations();  // avec les lignes
                        break;

                }

            } while (choice != 0);

            Console.Read();
        }
    }

}
