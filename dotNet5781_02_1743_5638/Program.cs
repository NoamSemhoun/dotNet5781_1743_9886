// TARGIL 2   in same solution
//   Noam

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    //  public enum Actions { }    PAS besoin titre trop long
    class Program
    {
            
            //BusLine busLine = new BusLine();
            //foreach (var line in busLine)
            //{

            //}

            //foreach (string element in list)
            //{
            //    Console.WriteLine(element.ToUpper());
            //}


          static void Main(string[] args)
        {
            // 10 ligne  40 station

            int choice;
            do
            {
                Console.WriteLine("* MENU * : Please Enter your choice : \n\n " +
                "\t1 to add a New Line,\n" +
                "\t2 to add a New Station,\n" +
                "\t3 to delete a Line\n" +
                "\t4 to delete a Station\n" +
                "\t5 to Search a Line in a Station\n" +
                "\t6 to 2 Station\n" +   ////////
                "\t7 to Print all Lines\n" +
                "\t8 to Print all the Stations (and them Line)\n" +
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
                        List<StationLine> list1 = new List<StationLine>();  // VERIFIER LE KELET
                        int num1 = int.Parse(Console.ReadLine());
                        list1.Add(new StationLine(num1)); 
                        Console.WriteLine("Enter the area of this new Bus Line :\n" +
                            "1 to General,\n" +
                            "2 to North,\n" +
                            "3 to South,\n" +
                            "4 to Center\n" +
                            "or 5 to Jerusalem)\n");
                        int area = int.Parse(Console.ReadLine());

                        Line L1 = new Line(num1,area,list1);
                        Console.WriteLine( L1 ); 
                        break;

                    case 2:  // // ADD NEW Station
                        // verifier si elle existe pas deja
                        //try {} catch  {Console.WriteLine("Error");}


                        Station S1 = new Station() { };
                        // ok = int.TryParse(Console.ReadLine(), out NumStation);


                        // trajet.Add(1) // Ajouter a la liste


                        Console.WriteLine($"New station added successfully:\n" + S1);
                        break;
                    case 3:   // remove
                        Console.WriteLine("Enter the number Line to delete :");
                        int.TryParse(Console.ReadLine(), out int num);
                        break;

                    case 4:
                        Console.WriteLine("Enter the Station to delete :");
                        int.TryParse(Console.ReadLine(), out  num);
                        break;
                    case 5:
                        Console.WriteLine("Enter the line to search and the stations in which it is located:");

                        //  search(line, station);
                        break;

                    case 6:
                        break;
                    case 7:
                        //foreach(list<StationLine> list in Line)
                        //{
                        //    Console.WriteLine(list);
                        //}

                        // printLines();
                        break;
                    case 8:
                       //  printStations();  // avec les lignes
                        break;

                }

            } while (choice != 0);

            Console.Read();
        }
    }
    
}
