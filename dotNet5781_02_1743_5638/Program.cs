// TARGIL 2   in same solution
//  GIT Noam

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    public enum Actions { }
    class Program
    {
        static void Main(string[] args)
        {
            // 10 ligne  40 station
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

            int choice;
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
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;

            }

            Console.ReadKey();
        }
    }
    
}
