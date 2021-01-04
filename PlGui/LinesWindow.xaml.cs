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
                Station_ListView.DataContext = ((sender as ListView).SelectedItem as BO.Line).List_LineStations;
                freq_ListView.DataContext = bl.GetFerquencies((sender as ListView).SelectedItem as BO.Line);
                add_Station.IsEnabled = true;
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
            Lines_list = new ObservableCollection<BO.Line>(bl.GetAllLines());
            ListView_Lines.DataContext = Lines_list;
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
        }

        private void index_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            index_TB.Foreground = Brushes.Black; 
            int input;
            if (((!int.TryParse(index_TB.Text, out input) || input > ((Station_ListView.DataContext as IEnumerable<BO.LineStation>).Count() + 1)) && index_TB.Text != ""))
               index_TB.Text = prevIndex;
            else
                prevIndex = index_TB.Text;
            if (Stations_CB.SelectedItem != null)
                addStation_button.IsEnabled = true;
        }

        private void Stations_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (prevIndex != "")
                addStation_button.IsEnabled = true;
        }

        private void addStation_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
