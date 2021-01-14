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
using System.Collections.ObjectModel;


namespace PlGui
{
    /// <summary>
    /// Logique d'interaction pour StationDétails_Window.xaml
    /// </summary>
    public partial class StationDétails_Window : Window
    {
        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();
        
        ObservableCollection<BO.AdjacentStation> nextesData;
        ObservableCollection<BO.AdjacentStation> prevsData;


        public StationDétails_Window(BO.Station MyStation)
        {
            InitializeComponent();
            MyGrid.DataContext = MyStation;
            Gridof_Lines.DataContext = (MyStation.List_Lines);
            nextesData = new ObservableCollection<BO.AdjacentStation>(bl.GetNextStations(MyStation.Code));
            nextStations_ListView.DataContext = nextesData;

            prevsData = new ObservableCollection<BO.AdjacentStation>(bl.GetprevStations(MyStation.Code));
            prevStations_ListView.DataContext = prevsData;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = "";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updaePrevStation_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save the changes?", "", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
                bl.UpdateAdjStations(prevsData.ToList());
        }

        private void updaeNextStation_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save the changes?", "", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                bl.UpdateAdjStations(nextesData.ToList());

        }
    }
}
