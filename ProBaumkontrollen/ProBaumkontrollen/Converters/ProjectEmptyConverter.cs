﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProBaumkontrollen.Converters
{
    public class ProjectEmptyConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string var = System.Convert.ToString(value);
                if (string.IsNullOrWhiteSpace(var))
                {
                    return "Kein Projekt gewählt";

                }
                else
                {
                    return var;
                }
            }
            else
            {
                return "Kein Projekt gewählt";
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if  (value is string)
            {
                return System.Convert.ToString(value);
            }else
            {
                return "";
            }

        }
    }
}
