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

    public delegate void addStationAdjEvent(object sender, EventArgs e);
    /// <summary>
    /// Interaction logic for AddAdjacentStation.xaml
    /// </summary>
    public partial class AddAdjacentStation : Window
    {
        string prevHTB = "";
        string prevMTB = "";
        string prevSTB = "";

        public event addStationAdjEvent ASAE;

        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();

        public AddAdjacentStation()
        {
            InitializeComponent();
            Station1CB.DataContext = bl.GetAllStations();
            Station2CB.DataContext = bl.GetAllStations();
        }

        public AddAdjacentStation(int station1Code, int station2Code)
        {
            InitializeComponent();
            Station1CB.DataContext = new List<BO.Station> { bl.GetStation(station1Code) };
            Station2CB.DataContext = new List<BO.Station> { bl.GetStation(station2Code) };

            Station1CB.SelectedIndex = 0;
            Station2CB.SelectedIndex = 0;

            Station1CB.IsEnabled = false;
            Station2CB.IsEnabled = false;
        }

        private void hTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            int input;
            if ((!int.TryParse(hTB.Text, out input) || input >= 24) && hTB.Text != "")
                hTB.Text = prevHTB;
            else
                prevHTB = hTB.Text;
        }

        private void mTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            int input;
            if ((!int.TryParse(mTB.Text, out input) || input >= 60) && mTB.Text != "")
                mTB.Text = prevMTB;
            else
                prevMTB = mTB.Text;
        }

        private void sTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            int input;
            if ((!int.TryParse(sTB.Text, out input) || input >= 60) && sTB.Text != "")
                sTB.Text = prevSTB;
            else
                prevSTB = sTB.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int station1 = (Station1CB.SelectedItem as BO.Station).Code;  
            int station2 = (Station2CB.SelectedItem as BO.Station).Code;

            try
            {
                bl.AddAdjStation(station1, station2, disSlide.Value, new TimeSpan(int.Parse(prevHTB), int.Parse(prevMTB), int.Parse(prevSTB)));
                if (ASAE != null)
                    ASAE(this, new EventArgs());
                this.Close();
            }
            catch ( BO.ItemAlreadyExeistExeption )
            {
                MessageBoxResult result = MessageBox.Show("the stations already have adj data\n do you wont to update it?", "Error", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    bl.UpdateAdjStation(station1, station2, disSlide.Value, new TimeSpan(int.Parse(prevHTB), int.Parse(prevMTB), int.Parse(prevSTB)));
            }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        //private void Station1Code_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    int input;
        //    if ((!int.TryParse(Station1Code.Text, out input) || input >= 10000) && Station1Code.Text != "")
        //        Station1Code.Text = prevS1;
        //    else
        //        prevS1 = Station1Code.Text;
        //    Station1CB.DataContext = bl.GetStation(input);
        //}
    }
}
