using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PO.Utilities
{
    public class ExtensionXml<T> where T : class
    {
        private static string stringToXml(T obj)
        {

            string _result = string.Empty;
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            _result += "<item";
            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.GetValue(obj) != null)
                {
                    //_result += string.Format(" {0}{1}{2}{3}", prop.Name, "='", prop.GetValue(obj) ?? DBNull.Value, "'");
                    _result += string.Format(" {0}{1}{2}{3}", prop.Name, "='", prop.GetValue(obj), "'");
                }

            }
            _result = _result.TrimEnd() + "></item>";

            return _result;
        }

        public static string ToXml(T item)
        {
            if (item != null)
            {
                return "<items>" + stringToXml(item) + "</items>";
            }
            return string.Empty;
        }


        public static string ToXml(IList<T> items)
        {
            string _result = string.Empty;
            if (items != null && items.Count > 0)
            {
                _result += "<items>";
                foreach (T item in items) { _result += stringToXml(item); }
                _result += "</items>";
            }
            return _result;
        }

    }
}
