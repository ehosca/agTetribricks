using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace agTetribricks
{
    [ValueConversion(typeof(object), typeof(string))]
    public class FormattingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string formatString = parameter as string;
            if (formatString != null)
                return string.Format(culture, formatString, value);
            else
                return value.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            // we don't intend this to ever be called
            return null;
        }
    }

    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value,Type targetType,object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString("g");
        }

        public object ConvertBack(object value,Type targetType, object parameter,CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return value;
        }
    }
}
