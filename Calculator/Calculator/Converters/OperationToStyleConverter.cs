using System;
using System.Globalization;
using Xamarin.Forms;

namespace Calculator.Converters
{
    public class OperationToStyleConverter : IValueConverter
    {
        public Style DefaultStyle { get; set; }
        public Style SelectedStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Operation operation && parameter is Operation targetOperation)
            {
                return operation == targetOperation ? SelectedStyle : DefaultStyle;
            }
            return DefaultStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ConvertBack is not needed for this scenario
            throw new NotImplementedException();
        }
    }

}