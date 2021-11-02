using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlTools
{
    public static class XElementExtensions
    {
        public static string XmlXPath(this XElement xElement) =>
            //string.Join("/", xElement.AncestorsAndSelf().Select(i => i.Name.LocalName));
            xmlxPath(xElement);

        public static string XsdXPath(this XElement xElement) =>
            xsdxPath(xElement);


        private static string xmlxPath(XElement element) =>        
            element.Parent == null ? 
                element.Name.LocalName :
                $"{xmlxPath(element.Parent)}/{element.Name.LocalName}";

        private static string xsdxPath(XElement element)
        {
            if (element.Parent == null)
                return null;
            var parentXPath = xsdxPath(element.Parent);
            var path = element.Attribute("name")?.Value;
            var prefix = path == null || parentXPath == null ? null : "/";
            return $"{parentXPath}{prefix}{path}";
        }
    }
}
