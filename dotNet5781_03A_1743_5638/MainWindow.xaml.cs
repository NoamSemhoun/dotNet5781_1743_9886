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
        private dotNet5781_02_1743_5638.Line currentDisplayBusLine;
        public HandleCollectionBus h;
        public MainWindow()  // ctor
        {
            InitializeComponent();
            h = new HandleCollectionBus();
            cbBusLines.ItemsSource = h.listLine;
            cbBusLines.DisplayMemberPath = "BusLineNumber";
            cbBusLines.SelectedIndex = 0;
            //ShowBusLine(cbBusLines.SelectedIndex);   // index ou aussi kav list 

            // lbBusLineStations.datacontext = cbBusLines.item.stations;
        }

        private void cbBusLines_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as dotNet5781_02_1743_5638.Line).BusLineNumber);
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = h[index];
            UpGrid.DataContext = currentDisplayBusLine.BusLineNumber;
            lbBusLineStations.DataContext = currentDisplayBusLine.listStations;
            tbArea_TextChanged(currentDisplayBusLine);
        }



        private void tbArea_TextChanged(dotNet5781_02_1743_5638.Line currentDisplayBusLine)
        {
            // afiicher la zone Area de la ligne 
            tbArea.DataContext = currentDisplayBusLine.area;
        }

        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}