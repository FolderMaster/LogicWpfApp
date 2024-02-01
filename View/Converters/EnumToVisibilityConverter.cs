using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace View.Converters
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                throw new NotImplementedException();
            }
            if (parameter == null)
            {
                throw new NotImplementedException();
            }
            var parameterString = parameter.ToString();
            var isInverse = parameterString.StartsWith("!");
            if (isInverse)
            {
                parameterString = parameterString.Substring(1);
            }
            var valueString = value.ToString();
            var isVisible = isInverse ? valueString != parameterString :
                valueString == parameterString;
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}
