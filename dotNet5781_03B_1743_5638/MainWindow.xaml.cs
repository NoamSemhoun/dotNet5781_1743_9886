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

namespace dotNet5781_03B_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random rand = new Random();
        List<Bus> ListBuses = new List<Bus>() ;
        WindowAdd WindowAdd = new WindowAdd();


        public MainWindow()
        {
            init();      // initialisation of 10 buses
            InitializeComponent();
            Lvbus.DataContext = ListBuses;


            
        }
        private void init()
        {
            

        }


        private void Button_Click(object sender, RoutedEventArgs e) // clique 
        {


            WindowAdd.ShowDialog();  // block the access to the main window






            //ListView1.Items.Add(textBox.Text);  or list Box
        }

        private void Lvbus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
