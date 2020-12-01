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

namespace dotNet5781_03B_1743_5638.Fenetres
{
    /// <summary>
    /// Logique d'interaction pour BusDetail.xaml
    /// </summary>
    public partial class BusDetail : Window
    {
        public bool refuelWasClicked = false;
        public bool maintenanceWasClicked = false;
        public int TextWasChanged;//We have to increment 2 times this variable to be sure that we dont change the driver and seat value,because if we changed it ,we have to declare it to MainWindow
        public BusDetail(Bus b)
        {
            InitializeComponent();
            StartingDate.Text = b.StartDate.ToString();
            LicenseNumber.Text = b.License;
            Gasoil.Text = b.Fuel.ToString();
            Kilometration.Text = b.Km.ToString();
            CheckupDatee.Text = b.Checkup.ToString();
            Drivername.Text = b.namechauffeur;
            State.Text = b.returnStatus.ToString();
            Seat.Text = b.SeatNumber.ToString();
            kmAfterMaintenance.Text = b.KmAfterLastMaintenance.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Drivername_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextWasChanged++;
        }

        private void Seat_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextWasChanged++;
        }

        private void refuel_Click(object sender, RoutedEventArgs e)
        {
            refuelWasClicked = true;
            MessageBox.Show("All's good , a Refuel will be launched at your exit !");
        }
        private void maintenance_Click(object sender, RoutedEventArgs e)
        {
            maintenanceWasClicked = true;
            MessageBox.Show("All's good, a Maintenance will be launched at your exit !");
        }
    }
}
