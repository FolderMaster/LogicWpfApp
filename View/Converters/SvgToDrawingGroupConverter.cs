using SharpVectors.Converters;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace View.Converters
{
    public class SvgToDrawingGroupConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stream = Application.GetResourceStream(new Uri(value.ToString(),
                UriKind.RelativeOrAbsolute));
            
            DrawingGroup drawingGroup;
            using (var reader = new FileSvgReader(new()))
            {
                drawingGroup = reader.Read(stream.Stream);
            }
            return drawingGroup;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) => throw new NotImplementedException();
    }
}