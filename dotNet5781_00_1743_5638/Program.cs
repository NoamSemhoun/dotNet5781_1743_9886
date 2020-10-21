using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNet5781_00_1743_5638
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome1743();
            Welcome5638();
            Console.ReadKey();
        }
        static partial void Welcome5638();

        private static void Welcome1743()
        {
            Console.WriteLine("Enter your user name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
