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
using System.ComponentModel;
using System.Threading;

namespace PlGui
{
    /// <summary>
    /// Logique d'interaction pour StationDétails_Window.xaml
    /// </summary>
    public partial class StationDétails_Window : Window
    {
        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();

        ObservableCollection<BO.AdjacentStation> nextesData;
        ObservableCollection<BO.AdjacentStation> prevsData;
        ObservableCollection<BO.LineSchedule> schedules;
        public BO.Station station;
        BackgroundWorker worker;
        int simulationRate = 10;
        

        int stationCode;

        public StationDétails_Window(BO.Station MyStation)
        {
            InitializeComponent();
            station = MyStation;
            MyGrid.DataContext = MyStation;
            Gridof_Lines.DataContext = (MyStation.List_Lines);
            nextesData = new ObservableCollection<BO.AdjacentStation>(bl.GetNextStations(MyStation.Code));
            nextStations_ListView.DataContext = nextesData;
            //clock.SimulationRate = simulationRate;
            //clock.Run();

            prevsData = new ObservableCollection<BO.AdjacentStation>(bl.GetprevStations(MyStation.Code));
            prevStations_ListView.DataContext = prevsData;

            schedules = new ObservableCollection<BO.LineSchedule>(bl.GetLinesSchedule(TimeSpan.Zero, MyStation.Code));
            ListView_Lines.DataContext = schedules;

            stationCode = MyStation.Code;

            worker = new BackgroundWorker();
            worker.DoWork += schdualTable;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += refreshSchedual;
            worker.WorkerSupportsCancellation = true;

            worker.RunWorkerAsync();



            this.Closing += closing_evenet;

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = "";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updaePrevStation_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save the changes?", "", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                bl.UpdateAdjStations(prevsData.ToList());
        }

        private void updaeNextStation_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save the changes?", "", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                bl.UpdateAdjStations(nextesData.ToList());

        }

        private void schdualTable(object sender, DoWorkEventArgs e)
        {
           
            while (!worker.CancellationPending)
            {
                Thread.Sleep(1000 / simulationRate);
                worker.ReportProgress(0);
            }
        }

        private void refreshSchedual(object sendr, ProgressChangedEventArgs e)
        {

            List<BO.LineSchedule> list = bl.GetLinesSchedule(TimeSpan.Zero, stationCode).ToList();
            list.Sort((l1, l2) =>
            {
                if (l1.NextArrivals == null || !l1.NextArrivals.Any())
                    return 1;
                if (l2.NextArrivals == null || !l2.NextArrivals.Any())
                    return -1;
                return l1.NextArrivals[0].CompareTo(l2.NextArrivals[0]);
            });

            ListView_Lines.DataContext = list;
        }

        private void closing_evenet(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }

        private void ListView_Lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SimulatorClick(object sender, RoutedEventArgs e)   
        {
            StationPanelSimulatorWindow win = new StationPanelSimulatorWindow(station);
            win.ShowDialog();
        }
    }
}
