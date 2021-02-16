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
using BO;
using System.Threading;
using System.ComponentModel;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationPanelSimulatorWindow.xaml
    /// </summary>
    ///

    public partial class StationPanelSimulatorWindow : Window
    {
        private ObservableCollection<BO.LineTiming> linesTimes = new ObservableCollection<BO.LineTiming>();
        Station station;


        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();

        public StationPanelSimulatorWindow(Station s)
        {
            station = s;
            InitializeComponent();
            //ListView_Lines.DataContext = linesTimes;
            bl.SetStationPanel(station.Code, updateLine);    
        }

        BackgroundWorker lineInstationWorker;

        private void updateLine (LineTiming newLineTiming)
        {
            LineTiming oldLineTiminig = linesTimes.FirstOrDefault(l => l.LineId == newLineTiming.LineId);
            if (oldLineTiminig != null)
            {
                if (oldLineTiminig.DepertureTime == newLineTiming.DepertureTime)
                    linesTimes.Remove(oldLineTiminig);
                else
                    return;
            }

            if (newLineTiming.ExpectedTime == TimeSpan.Zero)
            {
                Action<object> action = lineOnStationinvoke;
                Dispatcher.Invoke(action, newLineTiming);
            }
            else
            {
                linesTimes.Add(newLineTiming);
            }
            Dispatcher.Invoke(updatelineinvok);
                        
        }

        private void lineOnStationinvoke(object xx)
        {
            lineInstationWorker = new BackgroundWorker();
            lineInstationWorker.DoWork += lineOnStation;
            lineInstationWorker.WorkerReportsProgress = true;
            lineInstationWorker.ProgressChanged += lineOnStation_ProggresChanged;
            lineInstationWorker.RunWorkerCompleted += lineLeaveStation;
            lineInstationWorker.RunWorkerAsync(xx);
        }

        private void updatelineinvok()
        {
            ListView_Lines.DataContext = linesTimes.OrderBy(l => l.ExpectedTime);
        }

        private void lineOnStation(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i < 16; i++)
            {
                lineInstationWorker.ReportProgress(i, e.Argument);
                Thread.Sleep(300);
            }
        }

        private void lineOnStation_ProggresChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
                LineOnStation_TextBox.Text = $"line {(e.UserState as LineTiming).LineNumber} on station";
            if (e.ProgressPercentage % 2 == 1)
                LineOnStation_TextBox.Foreground = Brushes.Red;
            else
                LineOnStation_TextBox.Foreground = Brushes.Blue;
        }

        private void lineLeaveStation(object sender, RunWorkerCompletedEventArgs e)
        {
            LineOnStation_TextBox.Text = "";
        }
        
    }
}
