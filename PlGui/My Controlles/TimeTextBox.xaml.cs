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
    /// Interaction logic for TimeTextBox.xaml
    /// </summary>
    public partial class TimeTextBox : UserControl
    {
        int index;



        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TimeTextBox));

        public TimeTextBox()
        {
            InitializeComponent();
            mainTextBox.DataContext = this;
        }

        private void mainTextBox_KeyDown(object sender, KeyEventArgs e)
        {



            if ((int)e.Key <= 43 && (int)e.Key >= 34 || (int)e.Key >= 74 && (int)e.Key <= 84)
            {
                //mainTextBox.SelectionStart = 0;
                mainTextBox.Select(index++, 1);
                if (index == 2 || index == 5)
                    index++;
                if (index == 8)
                    index = 0;
                e.Handled = false;
            }
       
            else
                e.Handled = true;

        }

        private void mainTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            index = 0;
        }
    }

    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            int fontSize = (int)(actualHeight * .5);
            return fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
