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
using System.Collections.ObjectModel;

namespace PlGui
{
    /// <summary>
    /// Logique d'interaction pour BusWindow.xaml
    /// </summary>
    public partial class BusWindow : Window
    {
        ObservableCollection<BO.Bus> busList;
        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();


        public BusWindow()
        {
            InitializeComponent();
            busList = new ObservableCollection<BO.Bus>(bl.GetAllBuses());
            ListView_Bus.DataContext = busList;
        }

     

        private void Add_Click(object sender, RoutedEventArgs e)   // Button
        {

        }

        private void Fuel_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void Maintenance_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBus_SelectionDetail_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_STARTClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
