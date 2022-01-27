using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PortfolioXMLGenerator
{
    public static class XML_Util
    {
        static Dictionary<string, PROTECTION> protectionLevels = new Dictionary<string, PROTECTION>()
        {
            {"private",PROTECTION.PRIVATE},
            {"protected",PROTECTION.PROTECTED},
            {"internal",PROTECTION.INTERNAL},
            {"public",PROTECTION.PUBLIC}
        };

        public static bool StringContainsProtection(this string str, out PROTECTION protection)
        {
            string key = str.ToLower();
            if (protectionLevels.ContainsKey(key))
            {
                protection = protectionLevels[key];
                return true;
            }
            protection = default(PROTECTION);
            return false;
        }

        public static bool IsStartElement(this XmlReader xmlReader, PORTFOLIO_XML_ELEMENT element)
        {
            return xmlReader.IsStartElement(element.ToString().ToLower());
        }

        public static string GetAttribute(this XmlReader xmlReader, PORTFOLIO_XML_ATTRIBUTE attr)
        {
            return xmlReader[attr.ToString().ToLower()] ?? string.Empty;
        }

        public static void WriteStartElementEnum(this XmlWriter writer, PORTFOLIO_XML_ELEMENT element)
        {
            writer.WriteStartElement(element.ToString().ToLower());
        }

        public static void WriteAttributeEnum(this XmlWriter writer, PORTFOLIO_XML_ATTRIBUTE attribute, object obj)
        {
            writer.WriteAttributeString(attribute.ToString().ToLower(), obj.ToString());
        }
    }
    public enum PORTFOLIO_XML_ELEMENT
    {
        root,
        name,
        description,
        variables,
        variable,
        methods,
        method,
        parameter,
        properties,
        property,
        classconst,
        NAMESPACE
    }
    public enum PORTFOLIO_XML_ATTRIBUTE
    {
        name,
        type,
        protection,
        description,
        getter,
        setter,
        is_static,
        getterStatic,
        setterStatic,
        NAMESPACE
    }
}