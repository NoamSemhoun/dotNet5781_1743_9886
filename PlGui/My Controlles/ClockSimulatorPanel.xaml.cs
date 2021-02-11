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
    public partial class ClockSimulatorPanel : UserControl
    {
        TimeSpan time;
        int rate;
        bool runFlag = false;
        BackgroundWorker worker;
        IBL bl = BlAPI.BLFactory.GetBL();

        public ClockSimulatorPanel()
        {
            InitializeComponent();
            mainGrid.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!runFlag)
            {
                run_Button.Content = "stop";
                worker = new BackgroundWorker();
                worker.DoWork += runSimulation;
                worker.RunWorkerAsync();
                runFlag = true;
            }
            else
            {
                run_Button.Content = "run";
                stopSimulator();
                runFlag = false;
            }
        }

        private void runSimulation(object sender, DoWorkEventArgs e)
        {
            /*  bl.RunSimulator(time, rate)*/
            ;
        }

        private void stopSimulator()
        {
            //bl.stopSimulator();
        }

    }
}
