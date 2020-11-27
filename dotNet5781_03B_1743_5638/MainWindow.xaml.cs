using System;
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
using System.Windows.Shapes;
using dotNet5781_03B_1743_5638.Fenetres;
namespace dotNet5781_03B_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Bus> buses;
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
            buses[2].Fuel = 100;//This will be the bus with not much gasoil


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
            else//If the Line dont have to go on maintenance ,then it will be possible to it to go on road
            {
                GoClicked window = new GoClicked(c);
                window.ShowDialog();
                if (window.flag == true)//It will take the road only if we get our flag=true,this will mean that we can make it 
                {
                    c.Km += int.Parse(window.Distance.Text);
                    c.KmAfterLastMaintenance += int.Parse(window.Distance.Text);
                    c.Fuel -= int.Parse(window.Distance.Text);
                    MessageBox.Show("The journey has started !");
                }

            }
            ListBus.Items.Refresh();
        }
        private void RefuelClicked(object sender, RoutedEventArgs e)//Function to take the DataContext of the line containing this button and set it fuel to fulltank
        {
            var btn = sender as Button;
            var goodContext = btn.DataContext as Bus;
            goodContext.Fuel = 1200;
            MessageBox.Show("Your bus is full of gasoil now !");
            ListBus.Items.Refresh();

        }
        private void MaintenanceClicked(object sender, RoutedEventArgs e)//Function to take the DataContext of the line containing this button and set is MaintenanceDate to the current Date and set it kmAfterMaintenance to 0 because its now..
        {
            var btn = sender as Button;
            var goodContext = btn.DataContext as Bus;
            goodContext.Checkup = DateTime.Now;
            goodContext.KmAfterLastMaintenance = 0;
            goodContext.Fuel = 1200;
            MessageBox.Show("Ready to go !");
            ListBus.Items.Refresh();
        }
        private void ListBus_SelectionDetail(object sender, MouseButtonEventArgs e)//Function to mrk the index of the DataContext of the line we clicked ,and open new window with all the details about this Line
        {
            int index = ListBus.SelectedIndex;
            BusDetail window = new BusDetail(buses[index]);
            window.ShowDialog();
            
            if (window.TextWasChanged > 2)
            {
                buses[index].SeatNumber = int.Parse(window.Seat.Text);
                buses[index].namechauffeur = window.Drivername.Text;
                MessageBox.Show("Commit saved !");
            }
        }
    }
}
