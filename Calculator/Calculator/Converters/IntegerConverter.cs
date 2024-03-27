using System;
using System.Globalization;
using Xamarin.Forms;

namespace Calculator.Converters
{
    public class IntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (int.TryParse(stringValue, out int result))
                {
                    return result;
                }
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}