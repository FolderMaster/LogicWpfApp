using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shell;
using Model.Calculating;

namespace View.Converters
{
    public class CalculatingStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = CalculatingState.None;
            if(value is CalculatingState calculatingState)
            {
                state = calculatingState;
            }

            if(targetType == typeof(TaskbarItemProgressState))
            {
                return state switch
                {
                    CalculatingState.None => TaskbarItemProgressState.None,
                    CalculatingState.Run => TaskbarItemProgressState.Normal,
                    CalculatingState.Pause => TaskbarItemProgressState.Paused,
                    CalculatingState.Error => TaskbarItemProgressState.Error,
                    CalculatingState.Solution => TaskbarItemProgressState.Indeterminate,
                    _ => throw new NotImplementedException()
                };
            }
            else if (targetType == typeof(Brush))
            {
                return state switch
                {
                    CalculatingState.None => new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                    CalculatingState.Run => new SolidColorBrush(Color.FromRgb(0, 255, 0)),
                    CalculatingState.Pause => new SolidColorBrush(Color.FromRgb(255, 255, 0)),
                    CalculatingState.Error => new SolidColorBrush(Color.FromRgb(255, 0, 0)),
                    CalculatingState.Solution => new SolidColorBrush(Color.FromRgb(0, 255, 0)),
                    _ => throw new NotImplementedException()
                };
            }
            else if (targetType == typeof(bool))
            {
                return state switch
                {
                    CalculatingState.Solution => true,
                    _ => false
                };
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
