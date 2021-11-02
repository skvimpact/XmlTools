using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace XmlTools
{
    public class Xsd
    {
        public static string Infer(string xml)
        {
            XmlSchemaSet schemaSet = new XmlSchemaInference().InferSchema(XmlReader.Create(new StringReader(xml)));

            foreach (XmlSchema s in schemaSet.Schemas())
                using (var stringWriter = new StringWriter())
                {
                    using (var xmlWriter = XmlWriter.Create(stringWriter))
                        s.Write(xmlWriter);
                    //return $"{stringWriter}";
                    return $"{XDocument.Load(new StringReader(stringWriter.ToString()))}";
                }
            return null;
        }
    }
}
