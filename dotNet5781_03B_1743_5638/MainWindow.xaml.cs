﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.ComponentModel;
using dotNet5781_03B_1743_5638.Fenetres;
namespace dotNet5781_03B_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Bus> buses;
        public float timetoRoad;
        GoClicked window;
        BackgroundWorker threadMaintenance;
        BackgroundWorker threadRefuel;
        BackgroundWorker threadRoad;
        public MainWindow()
        {
           
            InitializeComponent();
            buses = new List<Bus>();
            for (int a = 0; a < 10; a++)
            {
                buses.Add(new Bus());
            }
         
            buses[0].Checkup = buses[0].StartDate;
            buses[0].Checkup = buses[0].Checkup.AddYears(1);//This will be the bus with a overdued DateCheckup 
            buses[1].Checkup = DateTime.Now.AddMonths(-2);//This will be the bus with a kilometrages close to a maintenance
            buses[1].KmAfterLastMaintenance = 19800;
            buses[1].Km += buses[1].KmAfterLastMaintenance;
            buses[2].Fuel = 1;//This will be the bus with not much gasoil
           for(int a=0;a<10;a++)
            {
                buses[a].checkStatus();

            }

            ListBus.DataContext = buses;
        }

     
        private void Button_Click(object sender, RoutedEventArgs e)//Function to add Bus to our Listview/ListBus
        {
            AddingBus window1 = new AddingBus();
            window1.ShowDialog();
            if (window1.b != null)//Only if b is not null we add the bus written in the window ,because if its Null that will mean that there was a problem in the details about this Bus 
            {
                buses.Add(window1.b);
                MessageBox.Show("Bus added successfully !");
            }
            ListBus.Items.Refresh();
        }
        private void LetsGoClicked(object sender, RoutedEventArgs e)//Function to do a route by the line who clicked it
        {

            var btn = sender as Button;
            var c = btn.DataContext as Bus;//Move to the DataContext of the good line 
            if (c.needMaintenance())
            {
                MessageBox.Show("Impossible ! You need a Maintenance !");
            }
            else if(c.Percent!=100)
            {
                MessageBox.Show("Your bus is not avilable right now ,try later !");
            }
            else//If the Line dont have to go on maintenance ,then it will be possible to it to go on road
            {
                window = new GoClicked(c);
                window.ShowDialog();
                if (window.flag == true)//It will take the road only if we get our flag=true,this will mean that we can make it 
                {  
                    c.returnStatus = "OnRoad";
                    ListBus.Items.Refresh();
                    timetoRoad = ((float.Parse(window.Distance.Text) / (c.Speed)) * 360000) / 100;
                    threadRoad = new BackgroundWorker();
                    threadRoad.DoWork += threadRoad_DoWork;
                    threadRoad.WorkerReportsProgress = true;
                    threadRoad.RunWorkerCompleted += threadRoad_RunWorkerCompleted;
                    threadRoad.RunWorkerAsync(c);
                }
            }
            
        }
        private void threadRoad_DoWork(object sender, DoWorkEventArgs e)
        {

            var mine = (Bus)e.Argument;//mine will be the bus we passed in parameter of the runworkerasynch
            for (int i = 0; i < 100; i++)
            {
                mine.Percent = i;
                Thread.Sleep((int)timetoRoad);
               
            }
            mine.Percent = 100;
            e.Result = mine;
        }
        private void threadRoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var mine = (Bus)e.Result;
                if (!window.checkfloat)
                {
                    mine.KmAfterLastMaintenance += int.Parse(window.Distance.Text);
                    mine.Km += mine.KmAfterLastMaintenance;
                    mine.Fuel -= int.Parse(window.Distance.Text);
                    mine.returnStatus = "Ready";
                    mine.checkStatus();
                    ListBus.Items.Refresh();
                }
                else
                mine.Km += float.Parse(window.Distance.Text);
                mine.KmAfterLastMaintenance += float.Parse(window.Distance.Text);
                mine.Fuel -= float.Parse(window.Distance.Text);
                mine.returnStatus = "Ready";
                mine.checkStatus();
                ListBus.Items.Refresh();

            }
            else
                MessageBox.Show("Error !");
        }

        private void RefuelClicked(object sender, RoutedEventArgs e)//Function to take the DataContext of the line containing this button and set it fuel to fulltank
        {
            var btn = sender as Button;
            var goodContext = btn.DataContext as Bus;
            if (goodContext.Percent == 100||goodContext.returnStatus=="NeedRefuel")
            {
                goodContext.returnStatus = "RefuelTime";
                ListBus.Items.Refresh();
                doRefuel(goodContext);
                
            }
            else
                MessageBox.Show("Your bus is not available right now,try later !");
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
                Thread.Sleep(10);
                mine.Percent = i;
            }
            mine.returnStatus = "Ready";
            mine.Percent = 100;
            e.Result = mine;
        }

        private void threadRefuel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var item = (Bus)e.Result;
            if (e.Error == null)//If all's good so we make the maintenance
            {
                
                item.Fuel = 1200;
                ListBus.Items.Refresh();
            }
            else
                MessageBox.Show("Error !");
        }
        private void MaintenanceClicked(object sender, RoutedEventArgs e)//Function to take the DataContext of the line containing this button and set is MaintenanceDate to the current Date and set it kmAfterMaintenance to 0 because its now..
        {
            var btn = sender as Button;
            var goodContext = btn.DataContext as Bus;
            if (goodContext.Percent == 100||goodContext.returnStatus=="NeedMaintenance")
            {
                goodContext.returnStatus = "InMaintenance";
                ListBus.Items.Refresh();
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
                Thread.Sleep(10);
                mine.Percent = i;
            }
            mine.returnStatus = "Ready";
            mine.Percent = 100;
            e.Result = mine;
        }
        private void threadMaintenance_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var item = (Bus)e.Result;
            if (e.Error == null)//If all's good so we make the maintenance
            {
                item.Checkup = DateTime.Now;
                item.KmAfterLastMaintenance = 0;
                item.Fuel = 1200;
                ListBus.Items.Refresh();
            }
            else
                MessageBox.Show("Error !");
        }

        private void ListBus_SelectionDetail(object sender, MouseButtonEventArgs e)//Function to mrk the index of the DataContext of the line we clicked ,and open new window with all the details about this Line
        {
            int index = ListBus.SelectedIndex;
            BusDetail window = new BusDetail(buses[index]);
            window.ShowDialog();
            if(window.refuelWasClicked)
            {
                buses[index].returnStatus = "RefuelTime";
                ListBus.Items.Refresh();
                doRefuel(buses[index]);
            }
            else if(window.maintenanceWasClicked)
            {
                buses[index].returnStatus = "InMaintenance";
                ListBus.Items.Refresh();
                doMaintenance(buses[index]);
            }
            else if (window.TextWasChanged > 2)//It mean that the user modified the data that he is able to modify, NameDriver and number of seat available 
            {
                buses[index].SeatNumber = int.Parse(window.Seat.Text);
                buses[index].namechauffeur = window.Drivername.Text;
                MessageBox.Show("Commit saved !");
            }
        }
    }
}
