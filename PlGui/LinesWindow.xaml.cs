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
    /// Logique d'interaction pour LinesWindow.xaml
    /// </summary>
    public partial class LinesWindow : Window
    {
        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();
        ObservableCollection<BO.Line> Lines_list;
        ObservableCollection<BO.LineStation> LineStations_list;
        ObservableCollection<BO.Ferquency> freq_List;

        string prevIndex = "";

        public LinesWindow()
        {
            InitializeComponent();
            Lines_list = new ObservableCollection<BO.Line>( bl.GetAllLines());
            ListView_Lines.DataContext = Lines_list;
            ListView_Lines.SelectedIndex = 0;  
       
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // lineViewSource.Source = [source de données générique]
        }

     

        private void ListLine_SelectionDetail_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListView_Lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem != null)
            {
                //Station_ListView.DataContext = ((sender as ListView).SelectedItem as BO.Line).List_LineStations;
                freq_ListView.DataContext = bl.GetFerquencies((sender as ListView).SelectedItem as BO.Line);
                //add_Station.IsEnabled = true;
            }
            //LineStations_list = new ObservableCollection<BO.LineStation>(((sender as ListView).SelectedItem as BO.Line).List_LineStations);
            //Station_ListView.DataContext = LineStations_list;

        }

        private void addLine_Click(object sender, RoutedEventArgs e)
        {
            AddLine_window win = new AddLine_window();
            win.Show();
            win.ALE += addLineEvent;
        }

        private void addLineEvent(object sender, EventArgs e)
        {
            refresh(ListView_Lines.SelectedIndex);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Lines_list = new ObservableCollection<BO.Line>(bl.GetAllLines());
        //    ListView_Lines.DataContext = Lines_list;
        //}

        private void Menu_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
            
        }

        private void add_Station_Click(object sender, RoutedEventArgs e)
        {
            freq_ListView.Visibility = Visibility.Hidden;
            addStation_Grid.Visibility = Visibility.Visible;
            Stations_CB.DataContext = bl.GetAllStations();
            Line_TB.Text = ((ListView_Lines.SelectedItem as BO.Line).LineNumber).ToString() ; // No by Binding ??

        }

        private void index_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            index_TB.Foreground = Brushes.Black; 
            int input;
            if (((!int.TryParse(index_TB.Text, out input)|| input <= 0 || input > (ListView_Lines.SelectedItem as BO.Line).List_LineStations.Count + 1) && index_TB.Text != ""))
               index_TB.Text = prevIndex;
            else
                prevIndex = index_TB.Text;
            //if (Stations_CB.SelectedItem != null)
            //    addStation_button.IsEnabled = true;
        }

        private void Stations_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (prevIndex != "")
                addStation_button.IsEnabled = true;
        }

        private void addStation_button_Click(object sender, RoutedEventArgs e)
        {
            addStation_Func();
        }

        private void addStation_addAdjSta_event(object sender, EventArgs e)
        {
            addStation_Func();
        }

        private void addStation_Func()
        {
            int id = (ListView_Lines.SelectedItem as BO.Line).LineID;
            int station = (Stations_CB.SelectedItem as BO.Station).Code;
            int index = int.Parse(index_TB.Text);


            try
            {
                bl.AddLineStation(id, station, index);

                //Station_ListView.DataContext = bl.GetLine(id).List_LineStations;

                refresh(ListView_Lines.SelectedIndex);
                freq_ListView.Visibility = Visibility.Visible;
                addStation_Grid.Visibility = Visibility.Hidden;
            }
            catch (BO.LackOfDataExeption ex)
            {
                if (ex.Data == BO.DataType.AdjacentStation)
                {
                    AddAdjacentStation win = new AddAdjacentStation(ex.id[0], ex.id[1]);
                    win.ASAE += addStation_addAdjSta_event;
                    win.Show();
                }
            }
        }

        private void CreateANewStation_Click(object sender, RoutedEventArgs e)
        {
            freq_ListView.Visibility = Visibility.Visible;
            addStation_Grid.Visibility = Visibility.Hidden;
            AddStation_Window ad = new AddStation_Window();
        }

        private void deleteStation_button_Click(object sender, RoutedEventArgs e)
        {
            delStation();
        }

        private void delStation_addAdj_event (object sender, EventArgs e)
        {
            delStation();
        }

        private void delStation()
        {
            int id = (ListView_Lines.SelectedItem as BO.Line).LineID;
            int index = (Station_ListView.SelectedItem as BO.LineStation).LineStationIndex;
            try 
            {
                bl.DeleteLineStation(id, index);
                refresh(ListView_Lines.SelectedIndex);
            }
            catch (BO.LackOfDataExeption ex)
            {
                AddAdjacentStation win = new AddAdjacentStation(ex.id[0], ex.id[1]);
                win.ASAE += delStation_addAdj_event;
                win.Show();
            }
         

        }

        private void refresh(int line)
        {
            int id = (ListView_Lines.SelectedItem as BO.Line).LineID;
           

            ListView_Lines.DataContext = bl.GetAllLines();
            ListView_Lines.SelectedIndex = line;
            //Station_ListView.DataContext = bl.GetLine(id).List_LineStations;

        }

        private void Station_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sur to delete the Line " + (ListView_Lines.SelectedItem as BO.Line).LineID + " ?", "Delete this Line", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:

                    bl.DeleteLine(((sender as Button).DataContext as BO.Line).LineID);
                    refresh(0);
                    //ListViewStations.Items.Refresh();
                    // UPDATE LINE & ADJACENT STATION

                    //ListView_Lines.Items.Remove(sender);      

                    MessageBox.Show("These Line have been deleted", "Deleted Line", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;

                case MessageBoxResult.Cancel:
                    MessageBox.Show("Ouf...", "Cancel",MessageBoxButton.OK,MessageBoxImage.Information);
                    break;
            }
        }

        private void StationDeatails_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(sender.GetType().Name);
            StationDétails_Window myWindow = new StationDétails_Window(bl.GetStation(((sender as ListView).SelectedItem as BO.LineStation).Code));
            myWindow.Show();
        }

        private void X_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    
}
