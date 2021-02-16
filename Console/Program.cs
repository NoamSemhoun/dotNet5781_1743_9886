using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using BL;
using BlAPI;
using DL;
using System.Xml.Linq;
using System.ComponentModel;
using System.Threading;

namespace Console
{
    class Program
    {

        static DalApi.IDAL dal = DalApi.DalFactory.GetDal();
        static   ClockSimulator clock = ClockSimulator.Instance;
        static IBL bl = BlAPI.BLFactory.GetBL();
        
        static void Main(string[] args)
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += xxx;
            //worker.RunWorkerAsync();
            //TravelOperator travelOperator = new TravelOperator();
            //travelOperator.AddStation(115);
            //travelOperator.RunTravelOperator();
            //Thread.Sleep(1000 * 500);
            //clock.stop();

            int[] A = new int[] {10, 1, 3,  1, 2, 2, 1, 0, 4 };
            

            string s = "011100";

           
            System.Console.WriteLine(solution(A));


            System.Console.ReadLine();


        }

     
        public static int solution(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            
            if (A.Length <= 1)
                return 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();
            bool towInRowFlag = false;


            dic.Add((A[0] + A[1]), 1);
            for (int i = 1; i < A.Length - 1; i++)
                if (towInRowFlag || (A[i] + A[i + 1]) != (A[i - 1] + A[i]))
                {
                    if (dic.ContainsKey((A[i] + A[i + 1])))
                        dic[(A[i] + A[i + 1])]++;
                    else
                        dic.Add((A[i] + A[i + 1]), 1);
                    towInRowFlag = false;
                }
                else
                    towInRowFlag = true;
            return dic.Values.Max();
        }




        static void xxx (object sender, DoWorkEventArgs e)
        {
            clock.Run(60, new TimeSpan(7,0,0));
        }

    }
}



       


