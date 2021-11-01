using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlTools
{
    public class Model
    {
        public static string ToXML<T>(T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {                
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}
