using System;
using System.Globalization;
using System.Windows.Data;

namespace agTetribricks
{
    public class ShortDateTimeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime) value;
            return string.Format("{0} {1}", date.ToString("M/dd/yy"), date.ToShortTimeString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            return DateTime.TryParse(strValue, out resultDateTime) ? resultDateTime : value;
        }

        #endregion
    }
}