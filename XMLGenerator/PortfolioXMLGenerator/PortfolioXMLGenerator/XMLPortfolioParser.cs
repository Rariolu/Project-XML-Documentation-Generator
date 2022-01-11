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
                    //assembly.AddType(parsedType);
                }
            }

            return true;
        }

        public static bool ParseType(string file, out ParsedType parsedType)
        {
            throw new NotImplementedException();
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
                    xmlReader.Read();

                    XmlReader variables = xmlReader.ReadSubtree();
                    while(variables.Read())
                    {
                        if (variables.IsStartElement(PORTFOLIO_XML_ELEMENT.variable))
                        {
                            string type = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.type);
                            string varName = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.name);
                            string varNamespace = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.NAMESPACE);
                            string isStatic = variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.is_static);
                            PROTECTION protection;
                            if (variables.GetAttribute(PORTFOLIO_XML_ATTRIBUTE.protection).StringContainsProtection(out protection))
                            {
                                //ParsedVariable parsedVariable = new ParsedVariable(varName, type, varNamespace, protection, isStatic);
                            }


                        }
                    }
                }
            }

            xmlReader.Close();
            return true;
        }
    }
}