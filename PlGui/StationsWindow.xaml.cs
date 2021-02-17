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

            //Gridof_Lines.DataContext = ((ListViewStations.SelectedItem as BO.Station).List_Lines);
            
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // stationViewSource.Source = [source de données générique]
        }

        private void ListViewStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try 
            {
                if(ListViewStations.SelectedItem != null)
                    Kavim_list.ItemsSource = ((ListViewStations.SelectedItem as BO.Station).List_Lines);
            }
            catch { }

            //BO.Station S = (BO.Station)ListViewStations.SelectedItem;
            //addressTextBox.Text = S.Address; // To test not by binding  

        }

        private void Linesview_Click(object sender, RoutedEventArgs e)
        {
            LinesWindow linesWindow = new LinesWindow();
            linesWindow.Show(); 
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

                    DeleteStation_func();
                    break;
                
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Ouf...", "Cancel");
                    break;
            }

        }

        private void DeleteStation_func()
        {
            foreach (BO.Station item in ListViewStations.SelectedItems)
            {
                try
                {
                    bl.DeleteStation(item.Code);
                    Stations_list = new ObservableCollection<BO.Station>(bl.GetAllStations());
                    

                    MessageBox.Show("These Stations have been deleted", "Deleted Stations");

                }
                catch (BO.LackOfDataExeption exp)
                {
                    if (exp.Data == BO.DataType.AdjacentStation)
                    {
                        MessageBox.Show("lack of Adjacent Station data:");
                        AddAdjacentStation win = new AddAdjacentStation(exp.id[0], exp.id[1]);
                        win.ASAE += AddAdjStation_event;
                        win.Show();
                    }
                }

            }
            ListViewStations.DataContext = Stations_list;
            //ListViewStations.Items.Refresh();
            // UPDATE LINE & ADJACENT STATION

        }

        private void AddAdjStation_event(object sender, EventArgs e)
        {
            DeleteStation_func();
        }

        private void Station_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            StationDétails_Window myWin = new StationDétails_Window((ListViewStations.SelectedItem as BO.Station));
            myWin.Show();
        }

        

        private void ViewLine_Click(object sender, RoutedEventArgs e)
        {
            LinesWindow win = new LinesWindow();
            win.ShowDialog() ;  // Send the Line to Selection
            //Kavim_list.SelectedItem;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
