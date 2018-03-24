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

}
