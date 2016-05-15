using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace AEonAX.Shared
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class Extn
    {
        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parentObject);
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            ContentElement contentElement = child as ContentElement;
            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            FrameworkElement frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }


        public static DateTime LongDateToDateTime(this long Longdate)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(Longdate * 1);
            DateTime Jan1st1970 =
                new DateTime(1970, 1, 1, 2, 0, 0, DateTimeKind.Utc); //2 Hours Added to match CM in browser
            return Jan1st1970.Add(ts).ToUniversalTime();
        }


        public static Hashtable reflectionProperties = new Hashtable();
        public static Hashtable reflectionDBTypeProperties = new Hashtable();

        //public static string Pluralize(this string s)
        //{
        //    PluralizationService plural =
        //        PluralizationService.CreateService(
        //            CultureInfo.GetCultureInfo("en-us"));
        //    return plural.Pluralize(s);
        //}



        public static string ToBase64String(this FlowDocument FD)
        {
            //return XamlWriter.Save(FD);
            TextRange tr = new TextRange(FD.ContentStart, FD.ContentEnd);
            MemoryStream ms = new MemoryStream();
            tr.Save(ms, System.Windows.DataFormats.XamlPackage);

            return Convert.ToBase64String(ms.ToArray());
            //You can save this data to SQLServer 
        }






        /// <summary>
        /// Invokes Action on Ui Thread
        /// </summary>
        /// <param name="obj">Control like "Textbox1"</param>
        /// <param name="action">Action like "() => Textbox1.text="Zango";"</param>
        public static void WFUIize(this ISynchronizeInvoke obj,
                                                 MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];
                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }
        //Example:
        //richEditControl1.UIize(() =>
        //{
        //    // Do anything you want with the control here
        //    richEditControl1.RtfText = value;
        //    RtfHelpers.AddMissingStyles(richEditControl1);
        //});






        /// <summary>
        /// Simple helper extension method to marshall to correct
        /// thread if its required
        /// </summary>
        /// <param name="control">The source control like "Textbox1"</param>
        /// <param name="methodcall">The method to call like "() => Textbox1.text="Zango";"</param>
        /// <param name="priorityForCall">The thread priority</param>
        public static void WPFUIize(
            this DispatcherObject control,
            Action methodcall,
            DispatcherPriority priorityForCall = DispatcherPriority.ApplicationIdle)
        {
            //see if we need to Invoke call to Dispatcher thread
            if (control.Dispatcher.Thread != Thread.CurrentThread)
                control.Dispatcher.Invoke(priorityForCall, methodcall);
            else
                methodcall();
        }
        //this.InvokeIfRequired(() =>
        //{
        //    lstItems.Items.Add(
        //        String.Format(“Count {0}”, currentCount));
        //},
        //    DispatcherPriority.Background);




        public static DbType DataTypetoDBType(string DataType)
        {
            switch (DataType)
            {
                case "byte": return DbType.Byte;
                case "sbyte": return DbType.SByte;
                case "short": return DbType.Int16;
                case "ushort": return DbType.UInt16;
                case "int": return DbType.Int32;
                case "uint": return DbType.UInt32;
                case "long": return DbType.Int64;
                case "ulong": return DbType.UInt64;
                case "float": return DbType.Single;
                case "double": return DbType.Double;
                case "decimal": return DbType.Decimal;
                case "bool": return DbType.Boolean;
                case "string": return DbType.String;
                case "char": return DbType.StringFixedLength;
                case "Guid": return DbType.Guid;
                case "DateTime": return DbType.DateTime;
                case "DateTimeOffset": return DbType.DateTimeOffset;
                case "byte[]": return DbType.Binary;
                case "byte?": return DbType.Byte;
                case "sbyte?": return DbType.SByte;
                case "short?": return DbType.Int16;
                case "ushort?": return DbType.UInt16;
                case "int?": return DbType.Int32;
                case "uint?": return DbType.UInt32;
                case "long?": return DbType.Int64;
                case "ulong?": return DbType.UInt64;
                case "float?": return DbType.Single;
                case "double?": return DbType.Double;
                case "decimal?": return DbType.Decimal;
                case "bool?": return DbType.Boolean;
                case "char?": return DbType.StringFixedLength;
                case "Guid?": return DbType.Guid;
                case "DateTime?": return DbType.DateTime;
                case "DateTimeOffset?": return DbType.DateTimeOffset;
                case "System.Data.Linq.Binary": return DbType.Binary;
                default: return DbType.Object;
            }
        }


        /// <summary>
        /// Caches PropertyInfo of given Datatype to a HashTable for better performance.
        /// </summary>
        /// <param name="targetType">DataType who's PropertyInfo is to be Cached</param>
        public static void LoadProperties(Type targetType)
        {
            if (reflectionProperties[targetType.FullName] == null)
            {
                List<PropertyInfo> propertyList = new List<PropertyInfo>();
                PropertyInfo[] objectProperties = targetType.GetProperties(/*BindingFlags.Public*/);
                foreach (PropertyInfo currentProperty in objectProperties)
                {
                    if (reflectionDBTypeProperties[currentProperty.Name] == null)
                        reflectionDBTypeProperties.Add(currentProperty.Name,
                            DataTypetoDBType(currentProperty.PropertyType.ToString()));
                    //propertyList.Add(currentProperty);
                }
                reflectionProperties.Add(targetType.FullName, objectProperties.ToList<PropertyInfo>());
                //reflectionproperties[targetObject] = propertyList;
            }
        }

        private static TimeSpan duration = TimeSpan.FromSeconds(0.5);
        public static void SetValueSmooth(this System.Windows.Controls.ProgressBar progressBar, double value)
        {
            DoubleAnimation animation = new DoubleAnimation(value, duration);
            progressBar.BeginAnimation(System.Windows.Controls.ProgressBar.ValueProperty, animation);
        }




        public static T DeepCopy<T>(T other)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, other);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }




        public static void BringToFrontOnCanvas(this FrameworkElement element)
        {
            if (element == null) return;

            System.Windows.Controls.Canvas parent = element.Parent as Canvas;
            if (parent == null) return;

            var maxZ = parent.Children.OfType<UIElement>()
              .Where(x => x != element)
              .Select(x => System.Windows.Controls.Canvas.GetZIndex(x))
              .Max();
            System.Windows.Controls.Canvas.SetZIndex(element, maxZ + 1);
        }

    }
}
