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

namespace dotNet5781_03B_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        private Bus myNewBus = new Bus();
        public WindowAdd()
        {
            InitializeComponent();
            this.DataContext = myNewBus; // modifier
        }

        public string MyData { get; set; }

        public Bus NewBus { get { return myNewBus; } }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      
        private void statusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }





        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
        //    // Charger les données en définissant la propriété CollectionViewSource.Source :
        //    // busViewSource.Source = [source de données générique]
        //}
    }
}
