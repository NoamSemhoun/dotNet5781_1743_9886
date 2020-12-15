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
        bool OldBus;
        bool changed = false;
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
                    throw new Exception("invalid StartDate string format");
                }
                bool result1 = int.TryParse(busNumber.Text, out int liscence);
                if (result1 == false)
                {
                    throw new Exception("invalid license format");
                }
                if (changed == false)
                {
                    throw new Exception("You have to precise if the bus is new or not !");
                }
             if(OldBus==true&&(kmTotal.Text.Length==0||KmAfterLastMaintenance.Text.Length==0))
                    {
                    throw new Exception("You have to type all the data of your old bus !");
                }
                double myfuel = FUEL.Value;
                


                //bool result3 = Status.TryParse(statusComboBox.Text, out Status mystatus);  

                bool result4 = int.TryParse(seat.Text, out int myseat);
                if (result4 == false)
                {
                    throw new Exception("invalid seat input ");
                }

                /*this.dateStartDatePicker.DisplayDate*/
                if (OldBus == true)
                {
                    myNewBus = new Bus(date, liscence, myfuel, Driver.Text,int.Parse(kmTotal.Text),float.Parse(KmAfterLastMaintenance.Text),DateTime.Parse(LastCheckupD.Text),myseat);
                    MainWindow.ListBuses.Add(myNewBus);
          
                    myNewBus.checkStatus();
                    this.Close();
                    MessageBox.Show("Bus added successfully !");
                }
                else
                {
                    myNewBus = new Bus(date, liscence, myfuel, Driver.Text, 0, 0, DateTime.Now, myseat);
                    MainWindow.ListBuses.Add(myNewBus);

                    myNewBus.checkStatus();
                    this.Close();
                    MessageBox.Show("Bus added successfully !");
                }
              

            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }
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

        private void Old_Checked(object sender, RoutedEventArgs e)
        {
            KmAfterLastMaintenance.IsReadOnly = false;
            kmTotal.IsReadOnly = false;
            KmAfterLastMaintenance.Visibility = Visibility.Visible;
            kmTotal.Visibility = Visibility.Visible;
            LastCheckupD.Visibility = Visibility.Visible;
            OldBus = true;
            changed = true;
        }

        private void New_Checked(object sender, RoutedEventArgs e)
        {
            LastCheckupD.Visibility = Visibility.Hidden;
            OldBus = false;
            KmAfterLastMaintenance.IsReadOnly = true;
            KmAfterLastMaintenance.Visibility = Visibility.Hidden;
            kmTotal.IsReadOnly = true;
            kmTotal.Visibility = Visibility.Hidden;
            changed = true;
        }
    }
}
