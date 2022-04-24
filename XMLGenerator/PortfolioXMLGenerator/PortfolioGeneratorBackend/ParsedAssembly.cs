using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    public class ParsedAssembly
    {
        string name;
        /// <summary>
        /// The name of this assembly.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        Dictionary<string, ParsedType> parsedTypes = new Dictionary<string, ParsedType>();
        public ParsedType[] ParsedTypes
        {
            get
            {
                return parsedTypes.Values.ToArray();
            }
        }

        Dictionary<string, ParsedEnum> parsedEnums = new Dictionary<string, ParsedEnum>();
        public ParsedEnum[] ParsedEnums
        {
            get
            {
                return parsedEnums.Values.ToArray();
            }
        }

        List<string> declaredNamespaces = new List<string>();
        public string[] DeclaredNamespaces
        {
            get
            {
                return declaredNamespaces.ToArray();
            }
        }

        /// <summary>
        /// ParsedAssembly constructor
        /// </summary>
        /// <param name="_name"></param>
        public ParsedAssembly(string _name)
        {
            name = _name;
        }

        public void AddDeclaredNamespace(string _namespace)
        {
            declaredNamespaces.Add(_namespace);
        }

        public void AddType(ParsedType type)
        {
            parsedTypes.Add(type.FullName, type);
        }

        public void AddEnum(ParsedEnum parsedEnum)
        {
            parsedEnums.Add(parsedEnum.Type.FullName, parsedEnum);
        }

        public bool HasType(string typeName, out ParsedType parsedType)
        {
            if (parsedTypes.ContainsKey(typeName))
            {
                parsedType = parsedTypes[typeName];
                return true;
            }
            parsedType = null;
            return false;
        }

        public bool HasEnum(string enumName, out ParsedEnum parsedEnum)
        {
            if (parsedEnums.ContainsKey(enumName))
            {
                parsedEnum = parsedEnums[enumName];
                return true;
            }
            parsedEnum = null;
            return false;
        }
        
        public void IntegrateParsedDocumentation(MemberDict memberDict)
        {
            foreach(string memberName in memberDict.Keys)
            {
                ParsedMember member = memberDict[memberName];

                string[] parts = memberName.GetParts();
                //string[] parts = memberName.Split('.');
                string potentialNamespace = parts[0];
                int partIndex = -1;
                for (int i = 1; i < parts.Length && DeclaredNamespaces.Contains(potentialNamespace); i++)
                {
                    potentialNamespace += "." + parts[i];
                    partIndex++;
                }

                if (partIndex > -1)
                {
                    
                    int typeIndex = partIndex + 1;
                    string typeName = parts[typeIndex];
                    string completeTypeName = potentialNamespace;
                    Console.WriteLine("typeName: {0};", typeName);

                    if (HasType(completeTypeName, out ParsedType parsedType))
                    {
                        
                        if (typeIndex == parts.Length -1)
                        {
                            parsedType.Description = member.Description;
                        }
                        else
                        {
                            int nextMemberIndex = typeIndex + 1;

                            string nextMemberName = parts[nextMemberIndex];
                            ParseMemberParent nextMember = null;

                            switch (member.MemberType)
                            {
                                case MemberType.Variable:
                                {
                                    if (parsedType.HasVariable(nextMemberName, out ParsedVariable parsedVariable))
                                    {
                                        nextMember = parsedVariable;
                                    }
                                    break;
                                }
                                case MemberType.Property:
                                {
                                    if (parsedType.HasProperty(nextMemberName, out ParsedProperty parsedProperty))
                                    {
                                        nextMember = parsedProperty;
                                    }
                                    break;
                                }
                                case MemberType.Method:
                                {
                                    if (parsedType.HasMethod(nextMemberName,out ParsedMethod parsedMethod))
                                    {
                                        nextMember = parsedMethod;
                                    }
                                    break;
                                }
                                case MemberType.Constructor:
                                {
                                    if (parsedType.HasConstructor(nextMemberName, out ParsedConstructor parsedConstructor))
                                    {
                                        nextMember = parsedConstructor;
                                    }
                                    break;
                                }
                            }

                            if (nextMember != null)
                            {
                                if (nextMemberIndex == parts.Length-1)
                                {
                                    nextMember.Description = member.Description;
                                    if (member.MemberType == MemberType.Method || member.MemberType == MemberType.Constructor)
                                    {
                                        ParsedMemberMethod memberMethod = member as ParsedMemberMethod;
                                        ParsedMethod parsedMethod = nextMember as ParsedMethod;
                                        int documentationParams = memberMethod.Parameters.Length;
                                        int reflectionParams = parsedMethod.Parameters.Length;
                                        for (int i = 0; i < documentationParams && i < reflectionParams; i++)
                                        {
                                            parsedMethod.Parameters[i].Description = memberMethod.Parameters[i].Description;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Idk what to do for this bit yet, rip.");
                                }
                            }
                        }

           
                    }
                    else if (HasEnum(completeTypeName, out ParsedEnum parsedEnum))
                    {
                        if (typeIndex == parts.Length - 1)
                        {
                            parsedEnum.Description = member.Description;
                        }
                        else
                        {
                            int valueIndex = typeIndex + 1;
                            string valueName = parts[valueIndex];
                            
                            if (parsedEnum.HasValue(valueName,out EnumValue enumValue))
                            {
                                enumValue.Description = member.Description;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Type {0} not found.", typeName);
                    }
                }
            }
        }

        public static bool operator ==(ParsedAssembly parsedAssembly1, ParsedAssembly parsedAssembly2)
        {
            bool pa1Null = ReferenceEquals(parsedAssembly1, null);
            bool pa2Null = ReferenceEquals(parsedAssembly2, null);

            if (pa1Null != pa2Null)
            {
                return false;
            }
            
            if (pa1Null)
            {
                return true;
            }

            if (parsedAssembly1.Name != parsedAssembly2.Name)
            {
                return false;
            }

            if (parsedAssembly1.ParsedTypes.Length != parsedAssembly2.ParsedTypes.Length)
            {
                return false;
            }

            foreach (string typeKey1 in parsedAssembly1.parsedTypes.Keys)
            {
                if (!parsedAssembly2.parsedTypes.ContainsKey(typeKey1))
                {
                    return false;
                }

                ParsedType type1 = parsedAssembly1.parsedTypes[typeKey1];
                ParsedType type2 = parsedAssembly2.parsedTypes[typeKey1];

                if (type1 != type2)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(ParsedAssembly parsedAssembly1, ParsedAssembly parsedAssembly2)
        {
            return !(parsedAssembly1 == parsedAssembly2);
        }
    }
}