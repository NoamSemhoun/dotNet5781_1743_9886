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
    /// Logique d'interaction pour StationDétails_Window.xaml
    /// </summary>
    public partial class StationDétails_Window : Window
    {
        public StationDétails_Window(BO.Station MyStation)
        {
            InitializeComponent();
            MyGrid.DataContext = MyStation;
            Gridof_Lines.DataContext = (MyStation.List_Lines);
        }

       
    }
}
