﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
//using System.Threading.Tasks;
using System.Xml;

namespace PortfolioGeneratorBackend
{
    public enum MemberType
    {
        Variable,
        Property,
        Method,
        Constructor,
        Type,
        Other
    }

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
                    //MemberTypes memberType = name.GetMemberType();
                    MemberType memberType = name.GetMemberType();

                    bool isMethod = memberType == MemberType.Method || memberType == MemberType.Constructor;

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

        static MemberType GetMemberType(this string nameStr)
        {
            char c = nameStr[0];
            switch(c)
            {
                case 'M':
                {
                    string memberName = nameStr.GetMemberName();
                    if (memberName.Contains("#ctor"))
                    {
                        return MemberType.Constructor;
                    }
                    return MemberType.Method;
                    //return MemberType.Method;
                }
                case 'F':
                    return MemberType.Variable;
                case 'P':
                    return MemberType.Property;
                case 'T':
                    return MemberType.Type;
                default:
                    Console.WriteLine("Identifying character was {0}. Text was {1}.", c, nameStr);
                    return MemberType.Other;
            }
        }

        //static MemberTypes GetMemberType(this string nameStr)
        //{
        //    char c = nameStr[0];
        //    switch(c)
        //    {
        //        case 'T':
        //            return MemberTypes.TypeInfo;
        //        case 'M':
        //            return MemberTypes.Method;
        //        case 'F':
        //            return MemberTypes.Field;
        //        case 'P':
        //            return MemberTypes.Property;
        //        default:
        //            return MemberTypes.Custom;
        //    }
        //}
        static string GetMemberName(this string nameStr)
        {
            nameStr = nameStr.Remove(0, 2);

            nameStr = nameStr.Replace("@", "&");

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

        MemberType type;
        public MemberType MemberType
        {
            get
            {
                return type;
            }
        }
        //MemberTypes type;
        //public MemberTypes MemberType
        //{
        //    get
        //    {
        //        return type;
        //    }
        //}

        public string Description { get; set; }

        /// <summary>
        /// Temp
        /// </summary>
        /// <param name="name"></param>
        /// <param name="memberType"></param>
        //public ParsedMember(string name, MemberTypes memberType)
        public ParsedMember(string name, MemberType memberType)
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
        //public ParsedMemberMethod(string name, MemberTypes memberType)
        public ParsedMemberMethod(string name, MemberType memberType)
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