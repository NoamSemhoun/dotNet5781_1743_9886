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

    public delegate void AddLineEvent(object sender, EventArgs e);
    /// <summary>
    /// Interaction logic for AddLine_window.xaml
    /// </summary>
    public partial class AddLine_window : Window
    {

        public event AddLineEvent ALE;

        int index = 0;
        int lineNumber;
        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();
        List<int> selectedStationList = new List<int>();
        List<int> indexes = new List<int>();
       
        BO.Areas area;
        private BO.AddLineExeption addLineEx;


        //ObservableCollection<BO.Station> selectedStationListView = new ObservableCollection<BO.Station>();

        public AddLine_window()
        {
           
            List<int> nums = new List<int>();
            for (int i = 1; i <= 999; i++)
                nums.Add(i);
            
            InitializeComponent();
            AddLine.IsEnabled = false;
            LineNumber.DataContext = nums;
            station_ListBox.DataContext = bl.GetAllStations();
            //StationsCB.DataContext = bl.GetAllStations();
            //viewSelectedStationList.DataContext = selectedStationList;
        }

        private void LineNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lineNumber = (int)(sender as ComboBox).SelectedItem;
            if (AreaCB.SelectedItem != null)
                AddLine.IsEnabled = true;
        }

        //private void StationsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    selectedStationList.Add((((sender as ComboBox).SelectedItem) as BO.Station).Code);
        //    selectedStationListView.Add((sender as ComboBox).SelectedItem as BO.Station);
        //    //viewSelectedStationList.DataContext = selectedStationListView;
        //    if (selectedStationList.Count() >= 2 && AreaCB.SelectedItem != null &&  LineNumber.SelectedItem != null)
        //        AddLine.IsEnabled = true;
        //}

        private void del_Click(object sender, RoutedEventArgs e)
        {
            selectedStationList.Remove(((sender as Button).DataContext as BO.Station).Code);
            
            
            
            
            
        
            //selectedStationListView.Remove((sender as Button).DataContext as BO.Station);
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            
            if(indexes.Any())
            {
                MessageBox.Show("some indexes are missing", "Error");
                return;
            }
            if(index < 2)
            {
                MessageBox.Show("Not enough stations were selected", "Error");
                return;
            }


            try
            {
                bl.AddLine(lineNumber, selectedStationList, area);
                if (ALE != null)
                    ALE(this, new EventArgs());
                this.Close();
            }
            catch (BO.ItemAlreadyExeistExeption)
            {
                MessageBox.Show("a bus with this data already exeist", "Error");
            }
            catch (BO.AddLineExeption ex)
            {
                MessageBoxResult result = MessageBox.Show("some stations miss adjacent data\n do you wont to add it?", "Error", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    addLineEx = ex;
                    AddAdjacentStation win = new AddAdjacentStation(ex.StationAdjMisses[0].Station1, ex.StationAdjMisses[0].Station2);
                    ex.StationAdjMisses.RemoveAt(0);
                    win.ASAE += addAdj_event;
                    win.Show();
                }
            }
        }

        private void addAdj_event(object sender, EventArgs e)
        {
            if (addLineEx.StationAdjMisses.Any())
            {
                MessageBoxResult result = MessageBox.Show("still some stations miss adjacent data\n do you wont to add it?", "Error", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    AddAdjacentStation win = new AddAdjacentStation(addLineEx.StationAdjMisses[0].Station1, addLineEx.StationAdjMisses[0].Station2);
                    addLineEx.StationAdjMisses.RemoveAt(0);
                    win.ASAE += addAdj_event;
                    win.Show();
                }
            }
            else
                AddLine_Click(this, new RoutedEventArgs());
        }


        private void ComboBoxArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            area = (BO.Areas)(AreaCB.SelectedItem) ;
            if (LineNumber.SelectedItem != null)
                AddLine.IsEnabled = true;
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content == "")
            {
                if (!indexes.Any())
                {
                    (sender as Button).Content = ++index;
                    selectedStationList.Add(((sender as Button).DataContext as BO.Station).Code);
                }
                else
                {
                    indexes.Sort();
                    (sender as Button).Content = indexes[0];
                    selectedStationList[indexes[0] - 1] = ((sender as Button).DataContext as BO.Station).Code;
                    indexes.RemoveAt(0);
                }
            }
            else
            {
                int tmp = (int)(sender as Button).Content;
                (sender as Button).Content = "";
                indexes.Add(tmp);
                while (indexes.Contains(index))
                {
                    indexes.Remove(index);
                    index--;
                }
            }
        }
    }
}
