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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlAPI;
using System.ComponentModel;

namespace PlGui.My_Controlles
{

    /// <summary>
    /// Interaction logic for ClockSimulatorPanel.xaml
    /// </summary>
    public partial class ClockSimulatorPanel : UserControl, INotifyPropertyChanged
    {
        TimeSpan time;
        public int Rate { get; set; }

        public TimeSpan Time
        {
            get => time;
            set
            {
                time = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Time"));
            }
        }

        public bool RunFlag { get => runFlag; set => runFlag = value; }

        public event EventHandler ButtonClick;

        //public static readonly DependencyProperty ClockSimulationTimeProperty = DependencyProperty.Register("Time", typeof(TimeSpan), typeof(ClockSimulatorPanel));


        bool runFlag = false;
        BackgroundWorker worker;
        IBL bl = BlAPI.BLFactory.GetBL();

        public event PropertyChangedEventHandler PropertyChanged;

        public ClockSimulatorPanel()
        {
            InitializeComponent();
            mainGrid.DataContext = this;
            worker = new BackgroundWorker();
            worker.DoWork += runSimulation;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += clockProgressEvent;
            Rate = 1;
            time = new TimeSpan(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonClick != null)
                ButtonClick(this, new EventArgs());
            if (!RunFlag)
            {
                run_Button.Content = "Stop";
                if(!worker.IsBusy)
                {   
                    worker.RunWorkerAsync();
                    rate_TextBox.IsEnabled = false;
                    time_textbox.IsEnabled = false;
                }
                    RunFlag = true;
            }
            else
            {
                run_Button.Content = "Run";
                stopSimulator();
                RunFlag = false;
                rate_TextBox.IsEnabled = true;
                time_textbox.IsEnabled = true;
            }
        }

        private void runSimulation(object sender, DoWorkEventArgs e)
        {
            bl.RunSimulator(Time, Rate, getProgress);
        }

        private void getProgress(TimeSpan simTime)
        {
            worker.ReportProgress(0, simTime);
        }

        private void clockProgressEvent(object sender, ProgressChangedEventArgs e)
        {
            Time = (TimeSpan)e.UserState;
        }

        private void stopSimulator()
        {
            bl.StopSimulator();
        }

        private void rate_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Key < 74 || (int)e.Key > 83)
            {
                e.Handled = true;
            }
        }
    }
}
