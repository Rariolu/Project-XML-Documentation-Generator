using System;
using System.Collections.Generic;

namespace PortfolioGeneratorBackend
{
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

        public static bool operator ==(ParsedVariable pv1, ParsedVariable pv2)
        {
            bool namesUnequal = pv1.Name != pv2.Name;
            bool typesUnequal = pv1.Type == pv2.Type;
            bool protectionUnequal = pv1.ProtectionLevel != pv2.ProtectionLevel;
            bool staticnessUnequal = pv1.IsStatic != pv2.IsStatic;
            if (namesUnequal || typesUnequal || protectionUnequal || staticnessUnequal)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(ParsedVariable pv1, ParsedVariable pv2)
        {
            return !(pv1 == pv2);
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
                paramms += Parameters[i].Type.Namespace + "." + Parameters[i].Type.TypeName;
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

        public static bool operator ==(ParsedMethod pm1, ParsedMethod pm2)
        {
            bool namesUnequal = pm1.CompleteName != pm2.CompleteName;

            bool typesUnequal = pm1.Type != pm2.Type;

            bool protectionUnequal = pm1.ProtectionLevel != pm2.ProtectionLevel;

            bool staticnessUnequal = pm1.IsStatic != pm2.IsStatic;

            if (namesUnequal || typesUnequal || protectionUnequal || staticnessUnequal)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(ParsedMethod pm1, ParsedMethod pm2)
        {
            return !(pm1 == pm2);
        }
    }

    public class ParsedConstructor : ParsedMethod
    {
        public ParsedConstructor(PROTECTION protection, bool isStatic = false)
            : base("", "", "", protection, isStatic)
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
            : base(_name, _type, _namespace)
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

        public static bool operator ==(TypeStruct typeStruct1, TypeStruct typeStruct2)
        {
            bool typesEqual = typeStruct1.TypeName == typeStruct2.TypeName;
            bool namespacesEqual = typeStruct1.Namespace == typeStruct2.Namespace;
            return typesEqual && namespacesEqual;
        }
        public static bool operator !=(TypeStruct typeStruct1, TypeStruct typeStruct2)
        {
            return !(typeStruct1 == typeStruct2);
        }
    }

    public enum ACCESSOR_TYPE
    {
        GETTER,
        SETTER
    }
}