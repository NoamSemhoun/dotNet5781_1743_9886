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
                int d;
                float i;
                flag = int.TryParse(Distance.Text,out d);
                checkfloat = float.TryParse(Distance.Text, out i);
                if (flag == false && checkfloat == false)
                {
                    MessageBox.Show("Error , you can only type numbers !");
                }
                else if(checkfloat==true)//Verif i the distance is a floatValue
                 {   if (temp.Fuel < float.Parse(Distance.Text))
                    {
                        MessageBox.Show("Impossible ! Not enough fuel ! Gasoil time !");
                        flag = false;
                    }
                    else if  (temp.KmAfterLastMaintenance + float.Parse(Distance.Text) > 2000)
                    {
                        MessageBox.Show("Impossible ! Your Kilometrages will be over 2000 km !Garage time !");
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                        this.Close();
                    }

                }
                else if(checkfloat==false)//Verify if the distance is a intValue
                {
                    if (temp.Fuel < int.Parse(Distance.Text))
                    {
                        MessageBox.Show("Impossible ! Not enough fuel ! Gasoil time !");
                        flag = false;
                    }
                    else if (temp.KmAfterLastMaintenance + int.Parse(Distance.Text) >= 2000)
                    {
                        MessageBox.Show("Impossible ! Your Kilometrages will be over 2000 km !Garage time !");
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
