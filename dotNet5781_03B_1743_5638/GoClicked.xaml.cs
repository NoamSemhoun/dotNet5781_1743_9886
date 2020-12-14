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

namespace dotNet5781_03B_1743_5638
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class GoClicked : Window
    {
        Bus temp;//We will see the copy of the bus passed in parameters to make our research to his possibility to go on road
        public bool flag;
        public bool checkfloat;
        public GoClicked(Bus bus)
        {
            InitializeComponent();
            temp = bus;

        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                 checkfloat = float.TryParse(Distance.Text, out float distance);
                if (!checkfloat)
                {
                    MessageBox.Show("Error , you can only type numbers (int or float) !");
                }
                else
                {
                    if (temp.Fuel < float.Parse(Distance.Text))
                    {
                        MessageBox.Show("Impossible ! Not enough fuel ! you need Gasoil before !");
                        flag = false;
                    }
                    else if (temp.Km_remaining < distance) // the distance is too much => need maintenance
                    {
                        MessageBox.Show("Impossible ! Your Kilometrages will be over 2 000 km ! You need Maintenance before this trip !");
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


    }
}

