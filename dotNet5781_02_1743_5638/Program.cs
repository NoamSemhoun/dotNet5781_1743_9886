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
        static void Main(string[] args)
        {

            int choice;
            HandleCollectionBus h = new HandleCollectionBus();
            do
            {

                Console.WriteLine("* MENU * : Please Enter your choice : \n\n " +
                "\t1 to add a New Line \n" +
                "\t2 to add a New Staion  \n" +
                "\t3 to delete a Line \n" +
                "\t4 to delete a Station from a Line\n" +
                "\t5 to print Lines in a Station\n" +
                "\t6 to Find the faster Line between 2 Station\n" +   ////////
                "\t7 to Print all Lines\n" +
                "\t8 to Print all the Stations and the lines that pass through it\n" +
                "\t0 to Exit ");

                bool ok = false;
                ok = int.TryParse(Console.ReadLine(), out choice);
                while (!ok || choice > 8 || choice < 0)
                {
                    Console.WriteLine("\nPlease enter a Number between 0 to 9\n");
                    ok = int.TryParse(Console.ReadLine(), out choice);
                }

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Thank You, Good Bye !");
                        break;
                    case 1:
                        try
                        {
                            h.addLine();
                        }
                        catch (ExceptionTarguil2 s)
                        {
                            Console.WriteLine(s.Message);
                        }
                        break;

                    case 2:
                        Console.WriteLine("What is the number of the line where you want to add a station :");
                        int reponse = int.Parse(Console.ReadLine());
                        if (h.IsNumberLineExists(reponse))
                        {
                            Console.WriteLine("Line found !");
                            try
                            { h.AddStation(reponse); }
                            catch (ExceptionTarguil2 e) { Console.WriteLine(e.Message); }
                        }
                        else
                        {
                            Console.WriteLine("This line doesn't exists");
                        }
                        break;
                    case 3:
                        try { h.deleteLine(); }
                        catch (ExceptionTarguil2 e) { Console.WriteLine(e.Message); }

                        break;

                    case 4:
                        Console.WriteLine("What is the number of the line where you want to delete a station :");
                        reponse = int.Parse(Console.ReadLine());
                        if (h.IsNumberLineExists(reponse))
                        {
                            Console.WriteLine("Line found !");
                            try { h.DeleteStation(reponse); }
                            catch (ExceptionTarguil2 e)
                            { Console.WriteLine(e.Message); }
                        }
                        else
                        {
                            Console.WriteLine("This line doesn't exists");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Please enter the ShellterNumber you want to search");
                        reponse = int.Parse(Console.ReadLine());
                        try { h.ThroughStation(reponse); }
                        catch (ExceptionTarguil2 e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 6:
                        try
                        {
                            h.Faster();
                        }
                        catch (ExceptionTarguil2 e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 7:
                        if (h.listLine.Count != 0)
                        {
                            foreach (Line line in h.listLine)
                            {
                                Console.WriteLine(line.ToString());
                            }
                        }
                        else { Console.WriteLine("Your system is empty !"); }

                        break;
                    case 8:
                        List<StationLine> StationL = new List<StationLine>(h.getStation());
                        Console.WriteLine("Here's all the Stations saved and number of line bus that pass through them :");
                        Console.WriteLine();
                        foreach (StationLine station in StationL)
                        {
                            Console.WriteLine(station.ShelterNumber);
                            h.ThroughStation(station.ShelterNumber);
                            Console.WriteLine();
                        }
                        break;

                }

            } while (choice != 0);
        

    }

    }

}
