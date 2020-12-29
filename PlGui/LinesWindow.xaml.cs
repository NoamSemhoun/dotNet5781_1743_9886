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

namespace PlGui
{
    /// <summary>
    /// Logique d'interaction pour LinesWindow.xaml
    /// </summary>
    public partial class LinesWindow : Window
    {
        public LinesWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // lineViewSource.Source = [source de données générique]
        }

     

        private void ListLine_SelectionDetail_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
