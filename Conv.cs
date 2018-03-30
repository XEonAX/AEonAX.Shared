using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AEonAX.Shared
{

    /// <summary>
    /// Use Like
    /// </summary>
    /// <example>
    /// <code>
    ///     &lt;Controls:MetroWindow.Resources&gt;
    ///         &lt;local:BoolToStringConverter x:Key=&quot;BoolToOwnerType&quot;
    ///          TrueValue=&quot;Owner's name&quot;
    ///          FalseValue=&quot;Old Owner's Name&quot; /&gt;
    ///     &lt;/Controls:MetroWindow.Resources&gt;
    /// </code>
    /// </example>
    public class BoolToStringConverter : BoolToValueConverter<string> { }
    public class BoolToBrushConverter : BoolToValueConverter<Brush> { }
    public class BoolToVisibilityConverter : BoolToValueConverter<Visibility> { }
    public class BoolToObjectConverter : BoolToValueConverter<Object> { }

    public class BoolToValueConverter<T> : IValueConverter
    {
        public T FalseValue { get; set; }
        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return FalseValue;
            else
                return (bool)value ? TrueValue : FalseValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }
    }

    /// <summary/>
    /// <example>
    /// <code>
    ///    &lt;Window.Resources&gt;
    ///    &lt;local:PercentageConverter x:Key=&quot;PercentageConverter&quot;/&gt;
    ///    &lt;/Window.Resources&gt;
    ///    &lt;Canvas x:Name=&quot;canvas&quot;&gt;
    ///        &lt;TextBlock Text=&quot;Hello&quot;
    ///                   Background=&quot;Red&quot; 
    ///                   Width=&quot;{Binding 
    ///                       Converter={StaticResource PercentageConverter}, 
    ///                       ElementName=canvas, 
    ///                       Path=ActualWidth, 
    ///                       ConverterParameter=0.1}&quot;/&gt;
    ///    &lt;/Canvas&gt;
    /// </code>
    /// </example>
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToDouble(value) *
                   System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    /// <example>
    /// <code>
    ///    &lt;Window.Resources&gt;
    ///    &lt;local:SubtractConverter x:Key=&quot;SubtractConverter&quot;/&gt;
    ///    &lt;/Window.Resources&gt;
    ///    &lt;Canvas x:Name=&quot;canvas&quot;&gt;
    ///        &lt;TextBlock Text=&quot;Hello&quot;
    ///                   Background=&quot;Red&quot; 
    ///                   Width=&quot;{Binding 
    ///                       Converter={StaticResource SubtractConverter}, 
    ///                       ElementName=canvas, 
    ///                       Path=ActualWidth, 
    ///                       ConverterParameter=0.1}&quot;/&gt;
    ///    &lt;/Canvas&gt;
    /// </code>
    /// </example>
    public class SubtractConverter : IValueConverter
    {
        public object Convert(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToDouble(value) -
                   System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }




    public class ByteSizeConverter : IValueConverter
    {
        static readonly string[] SizeSuffixes =
                   { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
        public object Convert(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            return SizeSuffix((long)value);
        }

        public object ConvertBack(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
