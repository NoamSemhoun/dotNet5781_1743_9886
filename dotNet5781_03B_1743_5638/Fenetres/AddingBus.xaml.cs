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

namespace dotNet5781_03B_1743_5638.Fenetres
{
    /// <summary>
    /// Logique d'interaction pour AddingBus.xaml
    /// </summary>
    public partial class AddingBus : Window
    {
        public Bus b;//Declare this bus to try our info of the user on a bus

        public AddingBus()
        {
            InitializeComponent();

        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                b = new Bus(StartingD.Text, LicenseN.Text, newOneorNot.Text, Kilometrages.Text, CheckupD.Text, Seat.Text, Chauffeurname.Text, kmafterMaintenance.Text);
                this.Close();
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }

            if (LicenseN.Text.Length < 1 && StartingD.Text.Length < 1 && newOneorNot.Text.Length < 1)
            {

                MessageBox.Show("You didnt input all the needed data !");
            }
        }

        private void newOneorNot_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (newOneorNot.Text == "O")//Its only when the Bus is Old that we have to add data like Kilometrages and his last CheckupDate
            {
                Kilometrages.IsReadOnly = false;
                CheckupD.IsReadOnly = false;
                kmafterMaintenance.IsReadOnly = false;
            }
            else//To make sure that even if we put O then after N , the field will stay blocked 
            {
                Kilometrages.IsReadOnly = true;
                CheckupD.IsReadOnly = true;
                kmafterMaintenance.IsReadOnly = true;
            }

        }
    }
}
