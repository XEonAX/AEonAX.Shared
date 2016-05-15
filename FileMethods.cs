﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace AEonAX.Shared
{
    public static class FileMethods
    {
        public static void WriteTofile<T>(this T obj, string filename, bool Throw = true)
        {
            try
            {
                XmlSerializer XMLSrlzr = new XmlSerializer(typeof(T));
                using (TextWriter writer = new StreamWriter(filename))
                    XMLSrlzr.Serialize(writer, obj);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Following error occured:"
                                + Environment.NewLine + Ex.Message 
                                + (Ex.InnerException != null ? Environment.NewLine + Ex.InnerException.Message : "")

                    //+ Environment.NewLine + @"Try to repair the file """ + filename + @""" manually"
                    //+ Environment.NewLine + @"Or make a backup and delete it"
                                , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Throw) throw;
            }
        }

        public static T ReadFromfile<T>(this T obj, string filename, bool Throw = true)
        {
            try
            {
                XmlSerializer XMLSrlzr = new XmlSerializer(typeof(T));
                if (File.Exists(filename))
                {
                    using (TextReader reader = new StreamReader(filename))
                    {
                        object objx = XMLSrlzr.Deserialize(reader);
                        obj = (T)objx;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Following error occured:"
                                + Environment.NewLine + Ex.Message
                                + (Ex.InnerException != null ? Environment.NewLine + Ex.InnerException.Message : "")
                                + Environment.NewLine + @"Try to repair the file """ + filename + @""" manually"
                                + Environment.NewLine + @"Or make a backup and delete it", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Throw) throw;
            }
            return obj;
        }
    }
}
