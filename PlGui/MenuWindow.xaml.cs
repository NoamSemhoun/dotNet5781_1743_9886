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
    /// Logique d'interaction pour MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void UserName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Lines_ButtonClick(object sender, RoutedEventArgs e)
        {
            LinesWindow linesWindow = new LinesWindow();
            linesWindow.Show();
        }

        private void Bus_ButtonClick(object sender, RoutedEventArgs e)
        {
            BusWindow BusWindow = new BusWindow();
            BusWindow.Show();
        }

      
    }
}
