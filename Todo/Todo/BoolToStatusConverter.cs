using System;
using System.Globalization;
using Xamarin.Forms;

namespace Todo
{
	public class BoolToStatusConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Done" : "Pending";
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == "Done";
        }
    }
}

