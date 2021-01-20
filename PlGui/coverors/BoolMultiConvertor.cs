using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlGui.coverors
{
    class BoolMultiConvertor : IMultiValueConverter
    {
      

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = true;
            foreach (object item in values)
            {
                if (item is bool)
                    flag = flag && (bool)item;
                else if (item is string)
                    flag = flag && ((string)item != "");
                else
                    flag = flag && (item != null);                         
            }
            return flag;
        }


        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
