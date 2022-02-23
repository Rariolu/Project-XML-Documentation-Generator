using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
//using System.Threading.Tasks;
using System.Xml;

namespace PortfolioXMLGenerator
{
    /// <summary>
    /// A utility for parsing XML documentation created by Visual Studio.
    /// </summary>
    public static class XMLDocumentationParser
    {
        enum XML_ELEMENT
        {
            assembly,
            member,
            name,
            summary,
            param
        }

        /// <summary>
        /// Returns a dictionary of parsed documentation members from the given file.
        /// </summary>
        /// <param name="file">Location of documentation XML file.</param>
        /// <returns></returns>
        public static MemberDict ParseDocumentationFile(string file)
        {
            MemberDict members = new MemberDict();
            if (!File.Exists(file))
            {
                return members;
            }

            XmlReader xmlReader = XmlReader.Create(file);
            while(xmlReader.Read())
            {
                if (xmlReader.IsStartElement(XML_ELEMENT.member.ToString()))
                {
                    string name = xmlReader["name"];
                    string memberName = name.GetMemberName();
                    MemberTypes memberType = name.GetMemberType();

                    bool isMethod = memberType == MemberTypes.Method || memberType == MemberTypes.Constructor;

                    ParsedMember member = isMethod ? new ParsedMemberMethod(memberName, memberType) : new ParsedMember(memberName, memberType);

                    XmlReader subtree = xmlReader.ReadSubtree();

                    while(subtree.Read())
                    {
                        if (subtree.IsStartElement(XML_ELEMENT.summary.ToString()))
                        {
                            xmlReader.Read();
                            member.Description = xmlReader.Value;
                        }

                        if (isMethod)
                        {
                            ParsedMemberMethod parsedMethod = member as ParsedMemberMethod;

                            if (subtree.IsStartElement(XML_ELEMENT.param.ToString()))
                            {
                                string paramName = xmlReader["name"];
                                xmlReader.Read();
                                string paramDescription = xmlReader.Value;
                                xmlReader.Read();

                                parsedMethod.AddParam(paramName, paramDescription);
                            }
                        }
                    }

                    members.Add(member.FullName, member);
                }
            }

            xmlReader.Close();

            return members;
        }

        static MemberTypes GetMemberType(this string nameStr)
        {
            char c = nameStr[0];
            switch(c)
            {
                case 'T':
                    return MemberTypes.TypeInfo;
                case 'M':
                    return MemberTypes.Method;
                case 'F':
                    return MemberTypes.Field;
                case 'P':
                    return MemberTypes.Property;
                default:
                    return MemberTypes.Custom;
            }
        }
        static string GetMemberName(this string nameStr)
        {
            nameStr = nameStr.Remove(0, 2);

            return nameStr;
        }

        /// <summary>
        /// Get the distinct components of the member name.
        /// </summary>
        /// <param name="nameStr"></param>
        /// <returns></returns>
        public static string[] GetParts(this string nameStr)
        {
            int bracketIndex = nameStr.IndexOf('(');
            string bracketSub = "";
            string newBracketSub = "";
            bool bracketed = false;
            if (bracketIndex > -1)
            {
                int length = (nameStr.Length) - bracketIndex;
                bracketSub = nameStr.Substring(bracketIndex, length);
                newBracketSub = bracketSub.Replace('.', '_');
                nameStr = nameStr.Replace(bracketSub, newBracketSub);
                bracketed = true;
            }

            
            string[] parts = nameStr.Split('.');

            if (bracketed)
            {

                parts[parts.Length - 1] = parts[parts.Length - 1].Replace(newBracketSub, bracketSub);

            }
            return parts;
        }
    }

    public class MemberDict : Dictionary<string, ParsedMember>
    {

    }

    public class ParsedMember
    {
        string fullName;
        public string FullName
        {
            get
            {
                return fullName;
            }
        }

        MemberTypes type;
        public MemberTypes MemberType
        {
            get
            {
                return type;
            }
        }
        
        public string Description { get; set; }

        public ParsedMember(string name, MemberTypes memberType)
        {
            fullName = name;
            type = memberType;

        }
    }
    public class ParsedMemberMethod : ParsedMember
    {
        List<ParsedMemberMethodParam> parameters = new List<ParsedMemberMethodParam>();
        public ParsedMemberMethodParam[] Parameters
        {
            get
            {
                return parameters.ToArray();
            }
        }
        public ParsedMemberMethod(string name, MemberTypes memberType)
            : base(name, memberType)
        {

        }

        public void AddParam(string name ,string description)
        {
            ParsedMemberMethodParam param = new ParsedMemberMethodParam();
            param.Name = name;
            param.Description = description;
            parameters.Add(param);
        }
    }

    public struct ParsedMemberMethodParam
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}