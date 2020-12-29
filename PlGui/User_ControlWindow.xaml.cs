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
using BlAPI;
using BO;

namespace PlGui
{
    /// <summary>
    /// Logique d'interaction pour User_Control.xaml
    /// </summary>
    public partial class User_ControlWindow : Window
    {
        public User_ControlWindow()
        {
            InitializeComponent();

        }


        private void AddAdmin_Button_Click(object sender, RoutedEventArgs e)
        {
            IBL bl = BLFactory.GetBL();
            BO.User newUser = new BO.User();
            newUser.is_Admin = true;
            newUser.UserName = "test";
            newUser.Password = "ok";
            bl.AddUser(newUser);

        }
    }
}
