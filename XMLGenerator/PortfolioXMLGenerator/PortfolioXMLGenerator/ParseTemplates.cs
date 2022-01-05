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
    public struct ParsedAssembly
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

    public struct ParsedType
    {
        string name;
        public string Name
        {
            get
            {
                return name;
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

        public ParsedType(string _name)
        {
            name = _name;
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

    public struct ParsedVariable
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

        public ParsedVariable(string _name, string _type, PROTECTION protection)
        {
            name = _name;
            type = _type;
            protectionLevel = protection;
        }
    }

    public struct ParsedMethod
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

        public ParsedMethod(string _name, string _returnType, PROTECTION protection)
        {
            name = _name;
            returnType = _returnType;
            parameters = new List<ParsedParameter>();
            protectionLevel = protection;
        }

        public void AddParameter(ParsedParameter parameter)
        {
            parameters.Add(parameter);
        }
    }

    public struct ParsedParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public struct ParsedProperty
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description { get; set; }

        //List<ParsedAccessor> accessors;
        Dictionary<ACCESSOR_TYPE, PROTECTION> accessors;

        public ParsedProperty(string _name)
        {
            name = _name;
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