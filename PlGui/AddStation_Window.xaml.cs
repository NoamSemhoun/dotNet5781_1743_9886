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
    /// Logique d'interaction pour AddStation_Window.xaml
    /// </summary>
    public partial class AddStation_Window : Window
    {
        BlAPI.IBL bl = BlAPI.BLFactory.GetBL();
        bool isShelterCovered = true;
        bool isDisableOk = true;
        bool isDigital = false;


        public AddStation_Window()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // stationViewSource.Source = [source de données générique]
        }

        

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            //             grid1.DataContext = 

            try
                {
                BO.Station myNewStation = new BO.Station {

                    Address = addressTextBox.Text,
                    Code = int.Parse(codeTextBox.Text),
                    Longitude = SliderLongitude.Value,
                    Latitude = SliderLatitude.Value,
                    Name = nameTextBox.Text,
                    disabled_access = isDisableOk,
                    Digital_PANNEL = isDisableOk,
                    covered_shelter = isShelterCovered };

            
            
         
            bl.AddStation(myNewStation);
            }
            catch { MessageBox.Show("Error"); } // IN : Name Code Or Adress
            this.Close();

        }

        private void ShelterC_Checked(object sender, RoutedEventArgs e)
        {
            isShelterCovered = true;
        }

        private void Digital_Checked(object sender, RoutedEventArgs e)
        {
            isDisableOk = true;
        }

        private void Disabled_Checked(object sender, RoutedEventArgs e)
        {
            isDisableOk = true;
            //X_Notdisabled.Visibility = Visibility.Hidden;

        }

        private void NOT_Disabled_Checked(object sender, RoutedEventArgs e)
        {
            isDisableOk = false;
            //X_Notdisabled.Visibility = Visibility.Visible;

        }

      
    }
}
