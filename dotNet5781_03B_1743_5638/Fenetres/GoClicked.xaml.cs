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
    /// Logique d'interaction pour GoClicked.xaml
    /// </summary>
    public partial class GoClicked : Window
    {
        Bus temp;//We will see the copy of the bus passed in parameters to make our research to his possibility to go on road
        public bool flag;
        public GoClicked(Bus bus)
        {
            InitializeComponent();
            temp = bus;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int d;
            flag = int.TryParse(Distance.Text, out d);
            if (flag == false)
            {
                MessageBox.Show("Error , you have to type an integer value !");
            }
            else if (temp.Fuel < int.Parse(Distance.Text))
            {
                MessageBox.Show("Impossible ! Not enough fuel ! Gasoil time !");
                flag = false;
            }
            else if (temp.Km + int.Parse(Distance.Text) >= 20000)
            {
                MessageBox.Show("Impossible ! Your Kilometrages will be over 20.000 km !Garage time !");
                flag = false;
            }
            else
            {
                flag = true;
                this.Close();
            }

        }
    }
}
