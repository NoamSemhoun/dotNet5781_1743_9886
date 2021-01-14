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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for DoubleTextBox.xaml
    /// </summary>
    public partial class DoubleTextBox : UserControl
    {
        public string Text
        {
            get => (string)GetValue(DoubleTextBoxTextProperty);
            set => SetValue(DoubleTextBoxTextProperty, value);
        }

        public static readonly DependencyProperty DoubleTextBoxTextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DoubleTextBox));


        public DoubleTextBox()
        {
            InitializeComponent();
            mainTextBox.DataContext = this;
        }

        private void mainTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Key <= 43 && (int)e.Key >= 34 || (int)e.Key >= 74 && (int)e.Key <= 84 || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right)
            {
                e.Handled = false;
            }
            else if ((e.Key == Key.Decimal || (InputLanguageManager.Current.CurrentInputLanguage.Name == "en-US" && e.Key == Key.OemPeriod)) && !mainTextBox.Text.Contains(".") && mainTextBox.SelectionStart != 0)
            {
                e.Handled = false;
                //if(mainTextBox.SelectionStart == mainTextBox.Text.Count())
                //{
                //    mainTextBox.Text += ".0";
                //    e.Handled = true;
                //}
            }
            else
                e.Handled = true;                              
        }

        
    }
}
