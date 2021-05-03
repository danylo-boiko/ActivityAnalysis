using System;
using System.Globalization;
using System.Windows.Data;

namespace ActivityAnalysis.WPF.Converters
{
    public class EqualValueToParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            string.Equals(value.ToString(),parameter.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            !string.Equals(value.ToString(),parameter.ToString());
    }
}