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
            summary
        }

        public static bool ParseDocumentationFile(string file, out ParseNode parentNode)
        {
            parentNode = new ParseNode("misc");
            if (!File.Exists(file))
            {
                return false;
            }

            XmlReader xmlReader = XmlReader.Create(file);
            while(xmlReader.Read())
            {
                if (xmlReader.IsStartElement(XML_ELEMENT.assembly.ToString()))
                {
                    xmlReader.Read();
                    if (xmlReader.IsStartElement(XML_ELEMENT.name.ToString()))
                    {
                        parentNode.Name = xmlReader.Value;
                    }
                }
                else if (xmlReader.IsStartElement(XML_ELEMENT.member.ToString()))
                {
                    string name = xmlReader["name"];
                    string memberName = name.GetMemberName();
                    MemberTypes memberType = name.GetMemberType();
                    string[] parts = GetParts(memberName);
                    //string[] parts = name.Split('.');
                    string nodeName = parts.Length > 0 ? parts[parts.Length - 1] : "misc";
                    ParseNode node = new ParseNode(nodeName, memberType);

                    xmlReader.Read();
                    if (xmlReader.IsStartElement(XML_ELEMENT.summary.ToString()))
                    {
                        xmlReader.Read();
                        node.Description = xmlReader.Value;
                    }

                    parentNode.AddChild(memberName, node);
                }
            }

            return true;
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
            //string prefix = nameStr.Substring(0, 2);
            nameStr = nameStr.Remove(0, 2);
            //nameStr = nameStr.TrimStart(prefix.ToCharArray());

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

    /// <summary>
    /// jeiofjeof
    /// </summary>
    public struct ParseNode
    {
        public string Name { get; set; }
        public string Description { get; set; }
        Dictionary<string, ParseNode> childNodes;
        public ParseNode[] Children
        {
            get
            {
                return childNodes.Values.ToArray();
            }
        }
        MemberTypes type;
        public MemberTypes Type
        {
            get
            {
                return type;
            }
        }
        /// <summary>
        /// Blep
        /// </summary>
        /// <param name="_name"></param>
        public ParseNode(string _name)
        {
            Name = _name;
            Description = "";
            childNodes = new Dictionary<string, ParseNode>();
            type = MemberTypes.Custom;
        }
        /// <summary>
        /// Blep2
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="memberType"></param>
        public ParseNode(string _name, MemberTypes memberType) : this(_name)
        {
            type = memberType;
        }

        /// <summary>
        /// Add child node to this node or one of its children
        /// </summary>
        /// <param name="childName"></param>
        /// <param name="node"></param>
        public void AddChild(string childName, ParseNode node)
        {
            string[] parts = childName.GetParts();//childName.Split('.');
            if (parts.Length == 1)
            {
                childNodes.Add(parts.Single(), node);
            }
            else if (parts.Length > 1)
            {
                string nextName = childName.TrimStart((parts[0] + ".").ToCharArray());
                if (childNodes.ContainsKey(parts[0]))
                {
                    childNodes[parts[0]].AddChild(nextName, node);
                }
                else
                {
                    ParseNode temp = new ParseNode(parts[0]);
                    childNodes.Add(parts[0], temp);
                }
            }
        }
    }
}
/// <summary>
/// TEMP
/// </summary>
public class TEMP
{
    public void BLEP()
    {

    }
    private void BLEP2()
    {

    }
    protected void BLEP3()
    {

    }
}