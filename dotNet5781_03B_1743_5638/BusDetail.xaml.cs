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
using System.Windows.Shapes;

namespace dotNet5781_03B_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour BusDetail.xaml
    /// </summary>
    public partial class BusDetail : Window
    {
        public bool refuelWasClicked = false;
        public bool maintenanceWasClicked = false;
        public int TextWasChanged;//We have to increment 2 times this variable to be sure that we dont change the driver and seat value,because if we changed it ,we have to declare it to MainWindow


        public BusDetail(Bus bus)
        {

            InitializeComponent();
            this.myGrid.DataContext = bus;
            StartingDate.Text = bus.DateStart.ToString();
            LicenseNumber.Text = bus.License;
            CheckupDatee.Text = bus.DateOfMaintenance.ToString();
            Drivername.Text = bus.NameDriver;
            Kilometration.Text = bus.KmTotal.ToString();
            State.Text = bus.Status.ToString();
            kmRemaining.Text = bus.Km_remaining.ToString();
            seat.Text = bus.seat.ToString();
            KmAfterLastMaintenance.Text = bus.KmAfterLastMaintenance.ToString();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void refuel_Click(object sender, RoutedEventArgs e)
        {
            refuelWasClicked = true;
            MessageBox.Show("All's good , a Refuel will be launched at your exit !");
        }
        private void Maintenance_Click(object sender, RoutedEventArgs e)
        {
            maintenanceWasClicked = true;
            MessageBox.Show("All's good, a Maintenance will be launched at your exit !");
        }

        private void State_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void kmAfterMaintenance_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Seat_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextWasChanged++;
        }
        private void Drivername_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextWasChanged++;
        }

        private void CheckupDatee_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}