using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PortfolioXMLGenerator
{
    public static class XMLPortfolioParser
    {
        public static bool ParsePortfolio(string dir, out ParsedAssembly assembly)
        {
            assembly = null;
            if (!Directory.Exists(dir))
            {
                return false;
            }

            string folderName = new DirectoryInfo(dir).Name;
            assembly = new ParsedAssembly(folderName);

            string[] xmlFiles = Directory.GetFiles(dir, "*.xml", SearchOption.TopDirectoryOnly);
            foreach(string xmlFile in xmlFiles)
            {
                ParsedType parsedType;
                if (ParseType(xmlFile, out parsedType))
                {
                    assembly.AddType(parsedType);
                }
            }

            return true;
        }

        public static bool ParseType(string file, out ParsedType parsedType)
        {
            parsedType = null;
            if (!File.Exists(file))
            {
                return false;
            }

            XmlReader xmlReader = XmlReader.Create(file);

            xmlReader.ReadToFollowing(PORTFOLIO_XML_ELEMENT.name.ToString());
            xmlReader.Read();

            string name = xmlReader.Value;

            xmlReader.ReadToFollowing(PORTFOLIO_XML_ELEMENT.NAMESPACE.ToString().ToLower());
            xmlReader.Read();

            string _namespace = xmlReader.Value;

            xmlReader.Close();

            xmlReader = XmlReader.Create(file);

            Console.WriteLine("Type name: {0}; namespace: {1};", name, _namespace);

            parsedType = new ParsedType(name, _namespace);
            
            while(xmlReader.Read())
            {
                if (xmlReader.IsStartElement(PORTFOLIO_XML_ELEMENT.description))
                {
                    xmlReader.Read();
                    parsedType.Description = xmlReader.Value;
                }

                if (xmlReader.IsStartElement(PORTFOLIO_XML_ELEMENT.variables))
                {
                    //xmlReader.Read();

                    XmlReader variables = xmlReader.ReadSubtree();
                    while(variables.Read())
                    {
                        if (variables.IsStartElement(PORTFOLIO_XML_ELEMENT.variable))
                        {
                            string type = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.type);
                            string varName = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.name);
                            string varNamespace = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE);
                            string isStatic = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.is_static);
                            string protStr = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.protection);
                            PROTECTION protection;
                            bool isStaticBool;
                            if (!protStr.StringContainsProtection(out protection))
                            {
                                protection = PROTECTION.PRIVATE;
                            }
                            if (!isStatic.StringContainsBool(out isStaticBool))
                            {
                                isStaticBool = false;
                            }

                            ParsedVariable parsedVariable = new ParsedVariable(varName, type, varNamespace, protection, isStaticBool);

                            string description = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.description);

                            if (!string.IsNullOrEmpty(description))
                            {
                                parsedVariable.Description = description;
                            }
                            
                            parsedType.AddVariable(parsedVariable);
                        }
                    }
                }
                else if (xmlReader.IsStartElement(PORTFOLIO_XML_ELEMENT.methods))
                {
                    //xmlReader.Read();

                    XmlReader methods = xmlReader.ReadSubtree();
                    while(methods.Read())
                    {
                        if (methods.IsStartElement(PORTFOLIO_XML_ELEMENT.classconst))
                        {
                            string protStr = methods.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.protection);
                            PROTECTION protection;

                            if (!protStr.StringContainsProtection(out protection))
                            {
                                protection = PROTECTION.PRIVATE;
                            }

                            ParsedConstructor parsedConstructor = new ParsedConstructor(protection);

                            //methods.Read();

                            XmlReader parameters = methods.ReadSubtree();

                            ParseParameters(parameters, parsedConstructor);

                            parsedType.AddMethod(parsedConstructor, true);
                        }
                        else if (methods.IsStartElement(PORTFOLIO_XML_ELEMENT.method))
                        {
                            string type = methods.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.type);
                            string methodName = methods.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.name);
                            string methodNamespace = methods.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE);
                            string protStr = methods.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.protection);
                            string staticStr = methods.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.is_static);

                            PROTECTION protection;
                            bool isStatic;
                            if (!protStr.StringContainsProtection(out protection))
                            {
                                protection = PROTECTION.PRIVATE;
                            }
                            if (!staticStr.StringContainsBool(out isStatic))
                            {
                                isStatic = false;
                            }

                            ParsedMethod parsedMethod = new ParsedMethod(methodName, type, methodNamespace, protection, isStatic);

                            string description = methods.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.description);
                            parsedMethod.Description = description;

                            //methods.Read();

                            XmlReader parameters = methods.ReadSubtree();

                            ParseParameters(parameters, parsedMethod);

                            parsedType.AddMethod(parsedMethod);
                        }
                    }
                }
                else if (xmlReader.IsStartElement(PORTFOLIO_XML_ELEMENT.properties))
                {
                    //xmlReader.Read();

                    XmlReader properties = xmlReader.ReadSubtree();
                    
                    while(properties.Read())
                    {
                        if (properties.IsStartElement(PORTFOLIO_XML_ELEMENT.property))
                        {
                            string type = properties.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.type);
                            string propertyName = properties.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.name);
                            string propertyNamespace = properties.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE);

                            string getterStr = properties.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.getter);
                            string getterStaticStr = properties.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.getterStatic);
                            string setterStr = properties.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.setter);
                            string setterStaticStr = properties.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.setterStatic);

                            ParsedProperty parsedProperty = new ParsedProperty(propertyName, type, propertyNamespace);
                            PROTECTION getter;
                            if (getterStr.StringContainsProtection(out getter))//StringContainsEnum(out getter))
                            {
                                bool getterStatic;
                                getterStaticStr.StringContainsBool(out getterStatic);

                                parsedProperty.AddAccessor(ACCESSOR_TYPE.GETTER, getter, getterStatic);
                            }

                            PROTECTION setter;
                            if (setterStr.StringContainsProtection(out setter))
                            {
                                bool setterStatic;
                                setterStaticStr.StringContainsBool(out setterStatic);

                                parsedProperty.AddAccessor(ACCESSOR_TYPE.SETTER, setter, setterStatic);
                            }

                            parsedType.AddProperty(parsedProperty);
                        }
                    }
                }
            }

            xmlReader.Close();
            return true;
        }

        static void ParseParameters(XmlReader parameters, ParsedMethod parsedMethod)
        {
            while (parameters.Read())
            {
                if (parameters.IsStartElement(PORTFOLIO_XML_ELEMENT.parameter))
                {
                    string paramType = parameters.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.type);
                    string paramName = parameters.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.name);
                    string paramNamespace = parameters.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE);
                    string paramDescription = parameters.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.description);

                    ParsedParameter parsedParameter = new ParsedParameter(paramName, paramType, paramNamespace);
                    parsedParameter.Description = paramDescription;

                    parsedMethod.AddParameter(parsedParameter);
                }
            }
        }
    }
}