using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProBaumkontrollen.Models.Datenbank;

namespace ProBaumkontrollen.Converters
{
    public class VerkehrssicherheitToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Verkehrssicherheit)value)
            {
                case Verkehrssicherheit.ja:
                    return "Ja";
                case Verkehrssicherheit.nein:
                    return "Nein";
                case Verkehrssicherheit.ungesetzt:
                    return "Ungesetzt";
                default:
                    throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
