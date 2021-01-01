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

namespace PlGui
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            //AddLine_window win = new AddLine_window();
            LinesWindow win = new LinesWindow();
            win.Show();
            this.Close();
        }




        private void Login_Click(object sender, RoutedEventArgs e)
        {

            //if (UsernameEntrance.Text == "Noam" || UsernameEntrance.Text == "Yair")
            //{
                //if (MyTextBox.Visibility = System.Windows.Visibility.Visible && Checkbox_passeword.IsChecked)
                //{
                //    MyTextBox.Text = PassewordEntrance.Password;   // To make sur recuperate the passeword of the TextBox
                //}
                    MenuWindow Menu = new MenuWindow();
                Menu.Show();
                this.Close();
            //}
            //else
            //{
            //    UsernameEntrance.Clear(); 
            //    UsernameEntrance.Focus();
            //    PassewordEntrance.Clear() ;
            //    MessageBox.Show("This Username no exist (Try Noam or Yair)");
            //    //MessageBox.Show("incorrect Username Passeword ");

            //}
        }

 

        private void DisplayPasseword(object sender, RoutedEventArgs e)
        {
            PassewordEntrance.Visibility = System.Windows.Visibility.Collapsed;
            MyTextBox.Visibility = System.Windows.Visibility.Visible;
            MyTextBox.Text = PassewordEntrance.Password ;

            MyTextBox.Focus();

        }
        private void NotDisplayPasseword(object sender, RoutedEventArgs e)
        {
            PassewordEntrance.Visibility = System.Windows.Visibility.Visible;
            MyTextBox.Visibility = System.Windows.Visibility.Collapsed;
            PassewordEntrance.Password = MyTextBox.Text;
            PassewordEntrance.Focus();
        }

        private void LicencePasswordBox_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
