using System;
using System.Globalization;
using System.Windows.Data;

namespace View.Converters
{
    public class TypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((Type)value).Name;

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => new NotImplementedException();
    }
}