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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public Window1()
        {
            InitializeComponent();
            MyTime myTime = new MyTime();
            aa.DataContext = myTime;
        }

    }

    class MyTime
    {
        private TimeSpan time;
        
        public MyTime() { time = new TimeSpan(2, 2, 4); }

        public TimeSpan Time { get => time; set => time = value; }
    }
}
