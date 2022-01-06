using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PortfolioXMLGenerator
{
    public static class XMLPortfolioSerialiser
    {
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
            property
        }
        public enum PORTFOLIO_XML_ATTRIBUTE
        {
            name,
            type,
            protection,
            description,
            getter,
            setter
        }
        public static void SerialiseParsedElements(this ParsedAssembly assembly, string dir)
        {
            if (!Directory.Exists(dir))
            {
                return;
            }

            foreach(ParsedType parsedType in assembly.ParsedTypes)
            {
                string path = Path.Combine(dir, parsedType.Name + ".xml");

                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.IndentChars = "\t";

                XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings);

                xmlWriter.WriteStartDocument();

                //<root>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.root);

                //<name>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.name);

                xmlWriter.WriteValue(parsedType.Name);

                //</name>
                xmlWriter.WriteEndElement();

                //<description>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.description);

                xmlWriter.WriteValue(parsedType.Description);

                //</description>
                xmlWriter.WriteEndElement();

                //<variables>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.variables);

                foreach(ParsedVariable parsedVariable in parsedType.Variables)
                {
                    //<variable>
                    xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.variable);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.type, parsedVariable.Type);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.name, parsedVariable.Name);

                    //</variable>
                    xmlWriter.WriteEndElement();
                }

                //</variables>
                xmlWriter.WriteEndElement();

                //<methods>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.methods);

                foreach(ParsedMethod parsedMethod in parsedType.Methods)
                {
                    //<method>
                    xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.method);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.type, parsedMethod.ReturnType);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.name, parsedMethod.Name);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.protection, parsedMethod.ProtectionLevel.ToString().ToLower());

                    foreach(ParsedParameter parsedParameter in parsedMethod.Parameters)
                    {
                        xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.parameter);

                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.type, parsedParameter.Type);

                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.name, parsedParameter.Name);

                        if (!string.IsNullOrEmpty(parsedParameter.Description))
                        {
                            xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.description, parsedParameter.Description);
                        }

                        xmlWriter.WriteEndElement();
                    }

                    //</method>
                    xmlWriter.WriteEndElement();
                }

                //</methods>
                xmlWriter.WriteEndElement();

                //<properties>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.properties);

                foreach (ParsedProperty parsedProperty in parsedType.Properties)
                {
                    //<property>
                    xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.property);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.type, parsedProperty.Type);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.name, parsedProperty.Name);

                    PROTECTION getProt;
                    if (parsedProperty.HasAccessor(ACCESSOR_TYPE.GETTER, out getProt))
                    {
                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.getter, getProt.ToString().ToLower());
                    }

                    PROTECTION setProt;
                    if (parsedProperty.HasAccessor(ACCESSOR_TYPE.SETTER, out setProt))
                    {
                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.setter, setProt.ToString().ToLower());
                    }

                    //</property>
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                //</properties>

                //</root>
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                xmlWriter.Close();
            }
        }
        
        static void WriteStartElementEnum(this XmlWriter writer, PORTFOLIO_XML_ELEMENT element)
        {
            writer.WriteStartElement(element.ToString());
        }

        static void WriteAttributeEnum(this XmlWriter writer, PORTFOLIO_XML_ATTRIBUTE attribute, object obj)
        {
            writer.WriteAttributeString(attribute.ToString(), obj.ToString());
        }
    }
}