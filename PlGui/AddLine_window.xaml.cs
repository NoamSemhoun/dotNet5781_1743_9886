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

    public delegate void AddLineEvent(object sender, EventArgs e);
    /// <summary>
    /// Interaction logic for AddLine_window.xaml
    /// </summary>
    public partial class AddLine_window : Window
    {

        public event AddLineEvent ALE;        

        int lineNumber;
        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();
        List<int> selectedStationList = new List<int>();
        ObservableCollection<BO.Station> selectedStationListView = new ObservableCollection<BO.Station>();

        public AddLine_window()
        {
           
            List<int> nums = new List<int>();
            for (int i = 1; i <= 999; i++)
                nums.Add(i);
            
            InitializeComponent();
            LineNumber.DataContext = nums;
            StationsCB.DataContext = bl.GetAllStations();
            viewSelectedStationList.DataContext = selectedStationList;
        }

        private void LineNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lineNumber = (int)(sender as ComboBox).SelectedItem;
        }

        private void StationsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStationList.Add((((sender as ComboBox).SelectedItem) as BO.Station).Code);
            selectedStationListView.Add((sender as ComboBox).SelectedItem as BO.Station);
            viewSelectedStationList.DataContext = selectedStationListView;
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show((sender as Button).DataContext.GetType().Name);
            selectedStationListView.Remove((sender as Button).DataContext as BO.Station);

        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            bl.AddLine(lineNumber, selectedStationList, BO.Areas.General);
            if (ALE != null)
                ALE(this, new EventArgs());
            this.Close();
        }
    }
}
