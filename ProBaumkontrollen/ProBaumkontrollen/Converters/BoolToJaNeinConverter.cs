using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProBaumkontrollen.Converters
{
    public class BoolToJaNeinConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "Ja";
            }
            else
            {
                return "Nein";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value=="Ja")
            {
                return true;
            }
            else
            {
                if ((string)value == "Nein")
                {
                    return false;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
