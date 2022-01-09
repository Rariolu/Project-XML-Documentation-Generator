using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioXMLGenerator
{
    public enum PROTECTION
    {
        PRIVATE,
        PROTECTED,
        PUBLIC,
        INTERNAL
    }
    public class ParsedAssembly
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        Dictionary<string, ParsedType> parsedTypes;
        public ParsedType[] ParsedTypes
        {
            get
            {
               return parsedTypes.Values.ToArray();
            }
        }

        public ParsedAssembly(string _name)
        {
            name = _name;
            parsedTypes = new Dictionary<string, ParsedType>();
        }

        public void AddType(ParsedType type)
        {
            parsedTypes.Add(type.Name, type);
        }
    }

    public class ParsedType
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }
        string fullName;
        public string FullName
        {
            get
            {
                return fullName;
            }
        }
        public string Description { get; set; }

        List<ParsedMethod> methods;
        //Dictionary<string, ParsedMethod> methods;
        public ParsedMethod[] Methods
        {
            get
            {
                return methods.ToArray();
                //return methods.Values.ToArray();
            }
        }

        List<ParsedVariable> variables;
        public ParsedVariable[] Variables
        {
            get
            {
                return variables.ToArray();
            }
        }

        List<ParsedProperty> properties;
        public ParsedProperty[] Properties
        {
            get
            {
                return properties.ToArray();
            }
        }

        public ParsedType(string _name, string _fullName)
        {
            name = _name;
            fullName = _fullName;
            Description = "";
            methods = new List<ParsedMethod>();
            variables = new List<ParsedVariable>();
            properties = new List<ParsedProperty>();
        }

        public void AddMethod(ParsedMethod method)
        {
            methods.Add(method);
            //methods.Add(method.Name, method);
        }

        public void AddVariable(ParsedVariable variable)
        {
            variables.Add(variable);
        }

        public void AddProperty(ParsedProperty property)
        {
            properties.Add(property);
        }
    }

    public class ParsedVariable
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        string type;
        public string Type
        {
            get
            {
                return type;
            }
        }

        PROTECTION protectionLevel;
        public PROTECTION ProtectionLevel
        {
            get
            {
                return protectionLevel;
            }
        }

        public string Description { get; set; }

        public ParsedVariable(string _name, string _type, PROTECTION protection)
        {
            name = _name;
            type = _type;
            protectionLevel = protection;
            Description = "";
        }
    }

    public class ParsedMethod
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        string returnType;
        public string ReturnType
        {
            get
            {
                return returnType;
            }
        }

        PROTECTION protectionLevel;
        public PROTECTION ProtectionLevel
        {
            get
            {
                return protectionLevel;
            }
        }

        List<ParsedParameter> parameters;
        public ParsedParameter[] Parameters
        {
            get
            {
                return parameters.ToArray();
            }
        }

        public string Description { get; set; }

        public ParsedMethod(string _name, string _returnType, PROTECTION protection)
        {
            name = _name;
            Description = "";
            returnType = _returnType;
            parameters = new List<ParsedParameter>();
            protectionLevel = protection;
        }

        public void AddParameter(ParsedParameter parameter)
        {
            parameters.Add(parameter);
        }
    }

    public class ParsedParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class ParsedProperty
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        string type;
        /// <summary>
        /// The type name of this parsed property.
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
        }

        public string Description { get; set; }

        Dictionary<ACCESSOR_TYPE, PROTECTION> accessors;

        public ParsedProperty(string _name, string _type)
        {
            name = _name;
            type = _type;
            Description = "";
            accessors = new Dictionary<ACCESSOR_TYPE, PROTECTION>();
        }

        public void AddAccessor(ACCESSOR_TYPE type, PROTECTION protection)//(ParsedAccessor accessor)
        {
            accessors.Add(type, protection);
            //accessors.Add(accessor);
        }
        public bool HasAccessor(ACCESSOR_TYPE type, out PROTECTION protection)
        {
            if (accessors.ContainsKey(type))
            {
                protection = accessors[type];
                return true;
            }
            protection = default(PROTECTION);
            return false;
        }
    }

    public enum ACCESSOR_TYPE
    {
        GETTER,
        SETTER
    }
}