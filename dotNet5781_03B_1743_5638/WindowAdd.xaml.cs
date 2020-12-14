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
using dotNet5781_03B_1743_5638;
namespace dotNet5781_03B_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        private Bus myNewBus;
        public WindowAdd()
        {
            InitializeComponent();
            // this.DataContext = myNewBus;  modifier

        }

        public string MyData { get; set; }
        public Bus NewBus { get { return myNewBus; } }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e) // OK validate the entrance
        {
            try
            {

                bool result = DateTime.TryParse(dateStartDatePicker.Text, out DateTime date);
                if (result == false)
                {
                    throw new Exception("invalid Star tDate string format");
                }
                bool result1 = int.TryParse(busNumber.Text, out int liscence);
                if (result1 == false)
                {
                    throw new Exception("invalid liscence format");
                }
                bool result2 = int.TryParse(kmTotal.Text, out int Km);
                if (result2 == false)
                {
                    throw new Exception("invalid Kilometre input ");
                }
                double myfuel = FUEL.Value;
                if (myfuel < 8 && (statusComboBox.Text != "NEED_REFUEL"))
                {
                    throw new Exception("invalid Status,\n you have a lot of gasoil so you NEED REFUEL ");
                }

                Status mystatus = (Status)Enum.Parse(typeof(Status), statusComboBox.Text);
                //bool result3 = Status.TryParse(statusComboBox.Text, out Status mystatus);  

                bool result4 = int.TryParse(seat.Text, out int myseat);
                if (result4 == false)
                {
                    throw new Exception("invalid seat input ");
                }

                /*this.dateStartDatePicker.DisplayDate*/

                myNewBus = new Bus(date, liscence, myfuel, mystatus, Driver.Text, Km, myseat);
                MainWindow.ListBuses.Add(myNewBus);

                myNewBus.checkStatus();
                this.Close();
                MessageBox.Show("Bus added successfully !");

            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }
        }


        private void statusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Driver_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Charger les données en définissant la propriété CollectionViewSource.Source :
            // busViewSource.Source = [source de données générique]
        }

        private void Fuel(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }


    }
}
