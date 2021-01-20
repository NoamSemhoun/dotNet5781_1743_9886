using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlGui.My_Controlles
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : UserControl, INotifyPropertyChanged
    {
        private TimeSpan time;

        public event PropertyChangedEventHandler PropertyChanged;

        private BackgroundWorker worker;

        int simulationRate = 1;

        public Clock()
        {
            InitializeComponent();
            MainTextBox.DataContext = this;
            DateTime _now = DateTime.Now;
            time = new TimeSpan(_now.Hour, _now.Minute, _now.Second);
            worker = new BackgroundWorker();
            worker.DoWork += run;
            worker.ProgressChanged += appendSecond;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }

        public TimeSpan Time
        {
            get => time;
            private set
            {
                time = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Time"));
            }
        }

        public int SimulationRate
        {
            get => simulationRate;
            set { if (value >= 1) simulationRate = value; }
        }

        public void Run()
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
            //Time += new TimeSpan(0, 0, 1);
        }

        public void Stop()
        {
            worker.CancelAsync();
        }

        private void run(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                Thread.Sleep((int) (1.0/simulationRate * 1000));
                worker.ReportProgress(0);
            }
        }

        private void appendSecond(object sender, ProgressChangedEventArgs e)
        {
            Time += new TimeSpan(0, 0, 1);
        }
    }
}
