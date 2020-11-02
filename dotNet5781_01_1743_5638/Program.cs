using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_1743_5638
{

    class Program
    {
        static void Main(string[] args)
        {
            Actions action;
            List<Bus> buses = new List<Bus>();
            bool result;
            do
            {
                Console.WriteLine("Choose an action from:");
                foreach (Actions act in (Actions[])Enum.GetValues(typeof(Actions)))
                {
                    Console.WriteLine("\t" + act);
                }
                Console.Write("\nYour choice: ");
                result = Enum.TryParse(Console.ReadLine(), out action);
                if (!result)
                {
                    Console.WriteLine("no such option\n");
                    continue;
                }

                switch (action)
                {
                    case Actions.ADD:
                        try
                        {
                            buses.Add(new Bus());
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        break;

                    case Actions.FIND:
                        {
                            bool flagy = false;
                            Console.WriteLine("Enter a number license :");/*type it with -*/
                            string rep = Console.ReadLine();
                            for (int a = 0; a < buses.Count; a++)
                            {
                                Random r = new Random();
                                int t;
                                t = r.Next(1200);
                                if (buses[a].License == rep)
                                {
                                    flagy = true;
                                    try
                                    {
                                        buses[a].Km += t;
                                        buses[a].Fuel -= t;
                                        mesoukan(buses[a]);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                        buses[a].Km -= t;
                                        buses[a].Fuel += t;

                                    }

                                }
                            }
                            if (!flagy)
                            {
                                Console.WriteLine("This bus doesn't exist");
                            }
                        }
                        break;
                    case Actions.MAINTENANCE:
                        {
                            bool flagy = false;
                            Console.WriteLine("Enter a number license");/*type it with -*/
                            string rep = Console.ReadLine();
                            for (int a = 0; a < buses.Count; a++)
                            {
                                if (buses[a].License == rep)
                                {
                                    buses[a].Maintenance();
                                    flagy = true;

                                }
                            }
                            if (!flagy)
                            {
                                Console.WriteLine("This bus doesn't exist");
                            }
                        }
                        break;
                    case Actions.REFUEL:
                        {
                            bool flagy = false;
                            Console.WriteLine("Enter a number license");/*type it with */
                            string rep = Console.ReadLine();
                            for (int a = 0; a < buses.Count; a++)
                            {
                                if (buses[a].License == rep)
                                {
                                    flagy = true;
                                    buses[a].Refuel();
                                }
                            }
                            if (!flagy)
                            {
                                Console.WriteLine("This bus doesn't exist");
                            }
                        }
                        break;
                    case Actions.PRINT:
                        for (int a = 0; a < buses.Count; a++)
                        {
                            Console.WriteLine(buses[a].ToString());
                        }
                        break;
                    default:
                        break;
                }

            } while (action != Actions.EXIT);

        }
        static void mesoukan(Bus b)
        {

            int i = DateTime.Compare(b.Checkup.Date, DateTime.Now.AddYears(-1));
            if (b.Km >= 20000)
                throw new Exception("Km > 20000");
            else if (i < 0)
                throw new Exception("Last maintenance > 1 year ");
            else if (b.Fuel <= 0)
                throw new Exception("Empty tank");
        }

    }

}