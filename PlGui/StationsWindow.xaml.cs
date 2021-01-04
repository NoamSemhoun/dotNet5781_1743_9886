using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace PlGui
{
    /// <summary>
    /// Logique d'interaction pour StationsWindow.xaml
    /// </summary>
    public partial class StationsWindow : Window
    {
        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();
        ObservableCollection<BO.Line> Lines_list;
        ObservableCollection<BO.LineStation> LineStations_list;
        ObservableCollection<BO.Station> Stations_list;

        public StationsWindow()
        {
            InitializeComponent();
            Stations_list = new ObservableCollection<BO.Station>(bl.GetAllStations());
            ListViewStations.DataContext = Stations_list;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // stationViewSource.Source = [source de données générique]
        }

        private void ListViewStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if ((sender as ListView).SelectedItem != null)

            //gridStations.DataContext = sender....();

            //BO.Station S = (BO.Station)ListViewStations.SelectedItem;
            //addressTextBox.Text = S.Address; // To test

        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
                   this.Close();
        }

        private void addStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation_Window window = new AddStation_Window();
            window.ShowDialog();     

            //ListViewStations.Items.Refresh();
            Stations_list = new ObservableCollection<BO.Station>(bl.GetAllStations());
            ListViewStations.DataContext = Stations_list;
        }

        private void DeleteStation_Click(object sender, RoutedEventArgs e) // The selection
        {

            MessageBoxResult result = MessageBox.Show("Are you sur to delete "+ ListViewStations.SelectedItems.Count + " Elements ?","Delete this Stations", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            switch (result)
            {
                case MessageBoxResult.OK:
                   
                    foreach ( BO.Station item in ListViewStations.SelectedItems)
                    {
                               bl.DeleteStation(item.Code);
                    }
                    //ListViewStations.Items.Refresh();
                    Stations_list = new ObservableCollection<BO.Station>(bl.GetAllStations());
                    ListViewStations.DataContext = Stations_list;

                    MessageBox.Show("These Stations have been deleted", "Deleted Stations");
                    break;
                
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Ouf...", "Cancel");
                    break;
            }

        }
    }
}
