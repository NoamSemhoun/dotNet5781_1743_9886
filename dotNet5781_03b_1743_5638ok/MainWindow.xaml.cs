using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace dotNet5781_03B_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random rand = new Random();
        public static List<Bus> ListBuses = new List<Bus>(); // protected ? for windows heritage
        public float timetoRoad;
        GoClicked window;
        BackgroundWorker threadMaintenance;
        BackgroundWorker threadRefuel;
        BackgroundWorker threadRoad;
        DispatcherTimer TimerLeftRoad;

        public MainWindow()
        {
            init();      // initialisation of 10 buses
            InitializeComponent();
            Lvbus.DataContext = ListBuses;

        }
        private void init()
        {
            for (int i = 0; i < 10; i++)
            {
                ListBuses.Add(new Bus());
            }
            ListBuses[1].Fuel = 1;  // a lot of gas  need refuel
            ListBuses[2].KmTotal = 4000; // To see that he need maintenance
            ListBuses[2].Km_remaining = 5;  // 2000 need Tipoul
            ListBuses[0].DateOfMaintenance = ListBuses[0].DateOfMaintenance.AddYears(-1); // need tipoul too



            for (int a = 0; a < 10; a++)
            {
                ListBuses[a].checkStatus();
            }

            // Refresh
        }





        #region Event :                                   ////////////////////////////////////


        private void Button_Click(object sender, RoutedEventArgs e)  // clique 
        {
            WindowAdd WindowAdd = new WindowAdd();
            WindowAdd.ShowDialog();  // Dialog block the access to the main window

            Lvbus.Items.Refresh();

        }
        private void Button_STARTClick(object sender, RoutedEventArgs e)  // Lets GO 
        {
            var btn = sender as Button;
            var thisbus = btn.DataContext as Bus;//Move to the DataContext of the good line 

            if ((DateTime.Compare(thisbus.DateOfMaintenance.Date, DateTime.Now.AddYears(-1))) < 0 || thisbus.Km_remaining == 0)
            {
                MessageBox.Show("Impossible ! You need a Maintenance !");
            }
            else if (thisbus.returnStatus != "READY")
            {
                MessageBox.Show("Your bus is not avilable right now ,try later !");
            }
            else//If the Line dont have to go on maintenance ,then it will be possible to it to go on road
            {
                window = new GoClicked(thisbus);
                window.ShowDialog();
                if (window.flag == true)//It will take the road only if we get our flag=true,this will mean that we can make it 
                {
                    thisbus.returnStatus = "ON_ROAD";
                    Lvbus.Items.Refresh();
                    thisbus.timeToRoad = ((float)((float.Parse(window.Distance.Text) / (thisbus.speed) * 3600000) * 0.1 / 60));//Set the time for the progressbar (have to /100 in the loop for)

                    thisbus.TimeBeforeArrival = new TimeSpan(TimeSpan.FromMilliseconds(thisbus.timeToRoad).Hours, TimeSpan.FromMilliseconds(thisbus.timeToRoad).Minutes, TimeSpan.FromMilliseconds(thisbus.timeToRoad).Seconds);//Set the value timer in BusDetail Window 
                    TimerLeftRoad = new DispatcherTimer();
                    makeTimer(thisbus);//Set the timer

                    threadRoad = new BackgroundWorker();//Set the thread
                    threadRoad.DoWork += threadRoad_DoWork;
                    threadRoad.WorkerReportsProgress = true;
                    threadRoad.RunWorkerCompleted += threadRoad_RunWorkerCompleted;
                    threadRoad.RunWorkerAsync(thisbus);
                }
            }
        }
        private void makeTimer(Bus c)
        {
            TimerLeftRoad.Interval = new TimeSpan(0, 0, 1);
            TimerLeftRoad.Tick += (s, args) =>
            {
                if (c.TimeBeforeArrival.Seconds == 0)
                {
                    if (c.TimeBeforeArrival.Minutes > 0)
                    {
                        c.TimeBeforeArrival = c.TimeBeforeArrival.Subtract(TimeSpan.FromMinutes(1));
                        c.TimeBeforeArrival = c.TimeBeforeArrival.Add(TimeSpan.FromSeconds(59));
                    }
                    else if (c.TimeBeforeArrival.Minutes == 0 && c.TimeBeforeArrival.Hours > 0)
                    {
                        c.TimeBeforeArrival = c.TimeBeforeArrival.Subtract(TimeSpan.FromHours(1));
                        c.TimeBeforeArrival = c.TimeBeforeArrival.Add(TimeSpan.FromMinutes(59));
                        c.TimeBeforeArrival = c.TimeBeforeArrival.Add(TimeSpan.FromSeconds(59));
                    }
                    else
                        TimerLeftRoad.Stop();
                }
                else
                    c.TimeBeforeArrival = c.TimeBeforeArrival.Subtract(TimeSpan.FromSeconds(1));

            };
        }
        private void threadRoad_DoWork(object sender, DoWorkEventArgs e)
        {

            var mine = (Bus)e.Argument;//mine will be the bus we passed in parameter of the runworkerasynch
            for (int i = 0; i < 100; i++)
            {
                TimerLeftRoad.Start();
                mine.Percent = i;
                Thread.Sleep((int)mine.timeToRoad / 100);
            }
            mine.Percent = 100;
            e.Result = mine;
        }

        private void threadRoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var mine = (Bus)e.Result;

                float distance = float.Parse(window.Distance.Text);
                mine.Km_remaining -= distance;
                mine.KmTotal += distance;
                mine.Fuel -= distance;
                mine.returnStatus = "READY";
                mine.checkStatus();
                Lvbus.Items.Refresh();

            }
            else
                MessageBox.Show("Error !");
        }



        private void Button_DelekClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var goodContext = btn.DataContext as Bus;
            if (goodContext.Percent == 100 || goodContext.returnStatus == "NEED_REFUEL")
            {
                goodContext.returnStatus = "REFUELING";
                Lvbus.Items.Refresh();
                doRefuel(goodContext);


            }
            else if (goodContext.returnStatus == "NEED_MAINTENANCE")
                MessageBox.Show("You have to pass in maintenance and you will go out with a FULLTANK !");
            else
                MessageBox.Show("Your bus is not avilable right now ,try later !");
        }
        private void doRefuel(Bus b)
        {
            threadRefuel = new BackgroundWorker();
            threadRefuel.DoWork += threadRefuel_DoWork;
            threadRefuel.WorkerReportsProgress = true;
            threadRefuel.RunWorkerCompleted += threadRefuel_RunWorkerCompleted;
            threadRefuel.RunWorkerAsync(b);
        }
        private void threadRefuel_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var mine = (Bus)e.Argument;//mine will be the bus we passed in parameter of the runworkerasynch

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(12000 / 100);
                mine.Percent = i;
            }
            mine.returnStatus = "READY";
            mine.Percent = 100;
            e.Result = mine;
        }

        private void threadRefuel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var item = (Bus)e.Result;
            if (e.Error == null)//If all's good so we make the maintenance
            {
                item.Fuel = 1200;
                Lvbus.Items.Refresh();
            }
            else
                MessageBox.Show("Error !");
        }





        private void Maintenance_Click(object sender, RoutedEventArgs e)
        {

            var btn = sender as Button;
            var goodContext = btn.DataContext as Bus;
            if (goodContext.Percent == 100 || goodContext.returnStatus == "NEED_MAINTENANCE")
            {
                goodContext.returnStatus = "IN_MAINTENANCE";
                Lvbus.Items.Refresh();
                doMaintenance(goodContext);
            }
            else
                MessageBox.Show("Your bus is not available right now,try later !");

        }


        private void doMaintenance(Bus b)
        {
            threadMaintenance = new BackgroundWorker();
            threadMaintenance.DoWork += threadMaintenance_DoWork;
            threadMaintenance.WorkerReportsProgress = true;
            threadMaintenance.RunWorkerCompleted += threadMaintenance_RunWorkerCompleted;
            threadMaintenance.RunWorkerAsync(b);
        }

        private void threadMaintenance_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var mine = (Bus)e.Argument;//mine will be the bus we passed in parameter of the runworkerasynch

            for (int i = 0; i < 100; i++)
            {
                mine.Percent = i;
                Thread.Sleep(144000 / 100);
            }
            mine.returnStatus = "READY";
            mine.Percent = 100;
            e.Result = mine;
        }
        private void threadMaintenance_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var item = (Bus)e.Result;
            if (e.Error == null)//If all's good so we make the maintenance
            {
                item.DateOfMaintenance = DateTime.Now;
                item.Km_remaining = 2000;
                item.Fuel = 1200;
                Lvbus.Items.Refresh();
            }
            else
                MessageBox.Show("Error !");
        }



        private void ListBus_SelectionDetail(object sender, MouseButtonEventArgs e)
        {
            int index = Lvbus.SelectedIndex;
            BusDetail window = new BusDetail(ListBuses[index]);
            window.ShowDialog();
            if (window.refuelWasClicked)
            {
                ListBuses[index].returnStatus = "REFUELING";

                Lvbus.Items.Refresh();
                doRefuel(ListBuses[index]);
            }
            else if (window.maintenanceWasClicked)
            {
                ListBuses[index].returnStatus = "IN_MAINTENANCE";
                Lvbus.Items.Refresh();
                doMaintenance(ListBuses[index]);
            }
            else if (!(window.TextWasChanged <= 2))//It mean that the user modified the data that he is able to modify, NameDriver and number of seat available 
            {
                ListBuses[index].seat = int.Parse(window.seat.Text);
                ListBuses[index].NameDriver = window.Drivername.Text;
                MessageBox.Show("Commit saved !");
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }




        #endregion

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
