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

                //<namespace>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.NAMESPACE);

                xmlWriter.WriteValue(parsedType.Type.Namespace);

                //</namespace>
                xmlWriter.WriteEndElement();

                if (!string.IsNullOrEmpty(parsedType.Description))
                {

                    //<description>
                    xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.description);

                    xmlWriter.WriteValue(parsedType.Description);

                    //</description>
                    xmlWriter.WriteEndElement();
                }

                //<variables>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.variables);

                foreach(ParsedVariable parsedVariable in parsedType.Variables)
                {
                    //<variable>
                    xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.variable);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.type, parsedVariable.Type);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.name, parsedVariable.Name);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE, parsedVariable.Type.Namespace);

                    if (!string.IsNullOrEmpty(parsedVariable.Description))
                    {
                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.description, parsedVariable.Description);
                    }

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.is_static, parsedVariable.IsStatic);

                    //</variable>
                    xmlWriter.WriteEndElement();
                }

                //</variables>
                xmlWriter.WriteEndElement();

                //<methods>
                xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.methods);

                foreach(ParsedConstructor parsedConstructor in parsedType.Constructors)
                {
                    //<classconst>
                    xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.classconst);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.protection, parsedConstructor.ProtectionLevel.ToString().ToLower());

                    if (!string.IsNullOrEmpty(parsedConstructor.Description))
                    {
                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.description, parsedConstructor.Description);
                    }

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.is_static, parsedConstructor.IsStatic);

                    foreach (ParsedParameter parsedParameter in parsedConstructor.Parameters)
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

                    //</classconst>
                    xmlWriter.WriteEndElement();
                }

                foreach(ParsedMethod parsedMethod in parsedType.Methods)
                {
                    //<method>
                    xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.method);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.type, parsedMethod.Type);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.name, parsedMethod.Name);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE, parsedMethod.Type.Namespace);

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.protection, parsedMethod.ProtectionLevel.ToString().ToLower());

                    if (!string.IsNullOrEmpty(parsedMethod.Description))
                    {
                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.description, parsedMethod.Description);
                    }

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.is_static, parsedMethod.IsStatic);

                    foreach(ParsedParameter parsedParameter in parsedMethod.Parameters)
                    {
                        xmlWriter.WriteStartElementEnum(PORTFOLIO_XML_ELEMENT.parameter);

                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.type, parsedParameter.Type);

                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.name, parsedParameter.Name);

                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE, parsedParameter.Type.Namespace);

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

                    xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE, parsedProperty.Type.Namespace);

                    if (!string.IsNullOrEmpty(parsedProperty.Description))
                    {
                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.description, parsedProperty.Description);
                    }

                    ParsedPropertyAccessor getAccessor;
                    if (parsedProperty.HasAccessor(ACCESSOR_TYPE.GETTER, out getAccessor))//out getProt))
                    {
                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.getter, getAccessor.ProtectionLevel.ToString().ToLower());//getProt.ToString().ToLower());

                        if (getAccessor.IsStatic)
                        {
                            xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.getterStatic, "true");
                        }
                    }

                    ParsedPropertyAccessor setAccessor;
                    if (parsedProperty.HasAccessor(ACCESSOR_TYPE.SETTER, out setAccessor))//out setProt))
                    {
                        xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.setter, setAccessor.ProtectionLevel.ToString());//setProt.ToString().ToLower());

                        if (setAccessor.IsStatic)
                        {
                            xmlWriter.WriteAttributeEnum(PORTFOLIO_XML_ATTRIBUTE.setterStatic, "true");
                        }
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
            writer.WriteStartElement(element.ToString().ToLower());
        }

        static void WriteAttributeEnum(this XmlWriter writer, PORTFOLIO_XML_ATTRIBUTE attribute, object obj)
        {
            writer.WriteAttributeString(attribute.ToString().ToLower(), obj.ToString());
        }
    }
}