using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace XmlTools
{
    public static class XDocumentExtensions
    {
        public static IEnumerable<ModelContent> ModelClasses(this XDocument xDocument, Func<XElement, string> func) =>
            new ModelContentFactory(xDocument).ModelClasses(func);
    }

    public class ModelContentFactory : ModelContent
    {
        public XDocument xDocument;
        public ModelContentFactory(XDocument xDocument) =>
            this.xDocument = xDocument;

        public IEnumerable<ModelContent> ModelClasses(Func<XElement, string> func) =>
            modelClass(xDocument.Root, func);

        private IEnumerable<ModelContent> modelClass(XElement xElement, Func<XElement, string> func)
        {
            var sb = new StringBuilder();
            var namespaceName = CapitalizeFirstLetter(xDocument.Root.Name.LocalName);
            var className = CapitalizeFirstLetter(xElement.Name.LocalName);
            sb.AppendLine("using System.Xml.Serialization;");
            sb.AppendLine($"namespace Model.CLI.{namespaceName}");
            sb.AppendLine("{");
            if (xElement.Equals(xDocument.Root))
                sb.AppendLine($"\t[XmlRoot(ElementName = \"{xElement.Name.LocalName}\")]");

            sb.AppendLine($"\tpublic class {className}");
            sb.AppendLine("\t{");
            foreach (var element in xElement.Elements())
            {
                sb.AppendLine($"\t\t[XmlElement(\"{element.Name.LocalName}\")]");
                sb.AppendLine($"\t\tpublic {func(element)} {CapitalizeFirstLetter(element.Name.LocalName)} {{ get; set; }}");
                if (element.Elements().Count() != 0)
                    foreach (var i in modelClass(element, func))
                        yield return i;
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            yield return new ModelContent()
            {
                Class = className,
                Content = sb.ToString(),
                Name = CapitalizeFirstLetter(xDocument.Root.Name.LocalName)
            };
        }
        public string CapitalizeFirstLetter(string text) =>
            new string(new[] { char.ToUpper(text.First()) }.Concat(text.Skip(1)).ToArray());
    }
}
