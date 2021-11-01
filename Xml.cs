using System.IO;
using System.Text;
using System.Xml.Serialization;
namespace XmlTools
{
    public class Xml
    {
        public static T ToModel<T>(string xml)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (var sr = new StringReader(xml))
            {
                return (T)xsSubmit.Deserialize(sr);
            }
        }
    }
}
