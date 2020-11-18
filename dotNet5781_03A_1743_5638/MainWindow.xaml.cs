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

        private dotNet5781_02_1743_5638.Line currentDisplayBusLine;   // Because There are 2 Line class


        public  MainWindow()  // ctor
        {
            InitializeComponent();

            HandleCollectionBus h = new HandleCollectionBus(); // initialisation of 10 lines
 
            cbBusLines.ItemsSource = h ;
            cbBusLines.DisplayMemberPath = " BusLineNum ";
            cbBusLines.SelectedIndex = 0;
            //ShowBusLine(……….)

        }

        //private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
            
        //}

        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = HandleCollectionBus[index]/*.First()*/;
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.firstStation;
        }

        private void cbBusLines_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as dotNet5781_02_1743_5638.Line).BusLineNumber);

        }
    }
}
