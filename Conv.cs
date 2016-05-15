using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AEonAX.Shared
{

        /// <summary>
        /// Use Like 
        /// <!--<Controls:MetroWindow.Resources>
        ///         <local:BoolToStringConverter x:Key="BoolToOwnerType"
        ///         TrueValue="Owner's name"
        ///         FalseValue="Old Owner's Name" />
        ///     </Controls:MetroWindow.Resources>
        /// -->
        /// </summary>
        public class BoolToStringConverter : BoolToValueConverter<String> { }
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

        /// <summary>
        ///    <Window.Resources>
        ///    <local:PercentageConverter x:Key="PercentageConverter"/>
        ///    </Window.Resources>
        ///    <Canvas x:Name="canvas">
        ///        <TextBlock Text="Hello"
        ///                   Background="Red" 
        ///                   Width="{Binding 
        ///                       Converter={StaticResource PercentageConverter}, 
        ///                       ElementName=canvas, 
        ///                       Path=ActualWidth, 
        ///                       ConverterParameter=0.1}"/>
        ///    </Canvas>
        /// </summary>
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



        /// <summary>
        ///    <Window.Resources>
        ///    <local:SubtractConverter x:Key="SubtractConverter"/>
        ///    </Window.Resources>
        ///    <Canvas x:Name="canvas">
        ///        <TextBlock Text="Hello"
        ///                   Background="Red" 
        ///                   Width="{Binding 
        ///                       Converter={StaticResource SubtractConverter}, 
        ///                       ElementName=canvas, 
        ///                       Path=ActualWidth, 
        ///                       ConverterParameter=0.1}"/>
        ///    </Canvas>
        /// </summary>
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
