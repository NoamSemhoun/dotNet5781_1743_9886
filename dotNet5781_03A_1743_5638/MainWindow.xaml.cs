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
using dotNet5781_02_1743_5638;

namespace dotNet5781_03A_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 
    
    
    public partial class MainWindow : Window
    {

        private Line currentDisplayBusLine;
       


        public  MainWindow()  // ctor
        {
            InitializeComponent();
            

            cbBusLines.ItemsSource = listLine;
            cbBusLines.DisplayMemberPath = " BusLineNum ";
            cbBusLines.SelectedIndex = 0;
            //ShowBusLine(……….)

        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ShowBusLine((cbBusLines.SelectedValue as Line).Bus);
            
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = listLine [index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }

        private void cbBusLines_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
