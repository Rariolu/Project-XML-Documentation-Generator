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
            parsedTypes.Add(type.FullName, type);
        }
    }

    public abstract class ParseMemberParent
    {
        string name = "";
        public string Name
        {
            get
            {
                return name;
            }
        }

        TypeStruct type;
        public TypeStruct Type
        {
            get
            {
                return type;
            }
        }

        public string Description { get; set; }

        bool isStatic;
        public bool IsStatic
        {
            get
            {
                return isStatic;
            }
        }

        public ParseMemberParent(string _name, string _type, string _namespace, bool _isStatic = false)
        {
            name = _name;
            type = new TypeStruct();
            type.TypeName = _type;
            type.Namespace = _namespace;
            isStatic = _isStatic;
        }
    }
    public class ParsedType : ParseMemberParent
    {
        public string FullName
        {
            get
            {
                return Type.Namespace + "." + Name;
            }
        }

        List<ParsedMethod> methods;
        public ParsedMethod[] Methods
        {
            get
            {
                return methods.ToArray();
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

        List<ParsedMethod> constructors;
        public ParsedMethod[] Constructors
        {
            get
            {
                return constructors.ToArray();
            }
        }

        public ParsedType(string _name, string _namespace, bool _isStatic = false)
            :base(_name, _name, _namespace, _isStatic)
        {
            methods = new List<ParsedMethod>();
            variables = new List<ParsedVariable>();
            properties = new List<ParsedProperty>();
            constructors = new List<ParsedMethod>();
        }

        public void AddMethod(ParsedMethod method, bool isConstructor = false)
        {
            if (isConstructor)
            {
                constructors.Add(method);
            }
            else
            {
                methods.Add(method);
            }
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

    public class ParsedVariable : ParseMemberParent
    {

        PROTECTION protectionLevel;
        public PROTECTION ProtectionLevel
        {
            get
            {
                return protectionLevel;
            }
        }

        public ParsedVariable(string _name, string _type, string _namespace, PROTECTION protection, bool _isStatic = false)
            : base(_name, _type, _namespace, _isStatic)
        {
            protectionLevel = protection;
            Description = "";
        }
    }

    public class ParsedMethod : ParseMemberParent
    {
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

        public virtual string CompleteName
        {
            get
            {
                string completeName = Name;

                if (Parameters.Length > 0)
                {
                    completeName += GetParamText();
                }

                return completeName;
            }
        }

        public string GetParamText()
        {
            string paramms = "";
            for (int i = 0; i < Parameters.Length; i++)
            {
                paramms += Parameters[i].Type.Namespace+"."+Parameters[i].Type.TypeName;
                if (i < Parameters.Length - 1)
                {
                    paramms += ",";
                }
            }
            return "(" + paramms + ")";
        }

        public ParsedMethod(string _name, string _returnType, string _namespace, PROTECTION protection, bool _isStatic = false)
            : base(_name, _returnType, _namespace, _isStatic)
        {
            parameters = new List<ParsedParameter>();
            protectionLevel = protection;
        }

        public void AddParameter(ParsedParameter parameter)
        {
            parameters.Add(parameter);
        }
    }

    public class ParsedConstructor : ParsedMethod
    {
        public ParsedConstructor(PROTECTION protection, bool isStatic = false)
            : base ("","","",protection, isStatic)
        {

        }
        public override string CompleteName
        {
            get
            {
                string completeName = "#ctor";
                if (Parameters.Length > 0)
                {
                    completeName += GetParamText();
                }
                return completeName;
            }
        }
    }

    public class ParsedParameter : ParseMemberParent
    {
        public ParsedParameter(string _name, string _type, string _namespace)
            :base(_name, _type, _namespace)
        {

        }
    }

    public class ParsedProperty : ParseMemberParent
    {

        Dictionary<ACCESSOR_TYPE, ParsedPropertyAccessor> accessors;

        public ParsedProperty(string _name, string _type, string _namespace)
            : base(_name, _type, _namespace)
        {
            accessors = new Dictionary<ACCESSOR_TYPE, ParsedPropertyAccessor>();
        }

        public void AddAccessor(ACCESSOR_TYPE type, PROTECTION protection, bool isStatic)//(ParsedAccessor accessor)
        {
            accessors.Add(type, new ParsedPropertyAccessor(type, protection, isStatic));
        }
        public bool HasAccessor(ACCESSOR_TYPE type, out ParsedPropertyAccessor propertyAccessor)
        {
            if (accessors.ContainsKey(type))
            {
                propertyAccessor = accessors[type];
                return true;
            }
            propertyAccessor = default(ParsedPropertyAccessor);
            return false;
        }
    }

    public struct ParsedPropertyAccessor
    {
        ACCESSOR_TYPE type;
        public ACCESSOR_TYPE Type
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

        bool isStatic;
        public bool IsStatic
        {
            get
            {
                return isStatic;
            }
        }

        public ParsedPropertyAccessor(ACCESSOR_TYPE _type, PROTECTION _protectionLevel, bool _isStatic)
        {
            type = _type;
            protectionLevel = _protectionLevel;
            isStatic = _isStatic;
        }
    }

    public struct TypeStruct
    {
        public string TypeName { get; set; }
        public string Namespace { get; set; }
        public override string ToString()
        {
            return TypeName;
        }
    }

    public enum ACCESSOR_TYPE
    {
        GETTER,
        SETTER
    }
}