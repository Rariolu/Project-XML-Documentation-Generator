using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    public class ParsedType : ParseMemberParent
    {
        /// <summary>
        /// The full type name including both the literal name its namespace.
        /// </summary>
        public string FullName
        {
            get
            {
                return Type.Namespace + "." + Name;
            }
        }

        Dictionary<string, ParsedMethod> methods;
        public ParsedMethod[] Methods
        {
            get
            {
                return methods.Values.ToArray();
            }
        }

        Dictionary<string, ParsedVariable> variables;
        public ParsedVariable[] Variables
        {
            get
            {
                return variables.Values.ToArray();
            }
        }

        Dictionary<string, ParsedProperty> properties;
        public ParsedProperty[] Properties
        {
            get
            {
                return properties.Values.ToArray();
            }
        }

        Dictionary<string, ParsedMethod> constructors;
        public ParsedMethod[] Constructors
        {
            get
            {
                return constructors.Values.ToArray();
            }
        }

        public ParsedType(string _name, string _namespace, bool _isStatic = false)
            : base(_name, _name, _namespace, _isStatic)
        {
            methods = new Dictionary<string, ParsedMethod>();
            variables = new Dictionary<string, ParsedVariable>();
            properties = new Dictionary<string, ParsedProperty>();
            constructors = new Dictionary<string, ParsedMethod>();
        }

        public void AddMethod(ParsedMethod method, bool isConstructor = false)
        {
            if (isConstructor)
            {
                constructors.Add(method.CompleteName, method);
            }
            else
            {
                methods.Add(method.CompleteName, method);
            }
        }

        public void AddVariable(ParsedVariable variable)
        {
            variables.Add(variable.Name, variable);
        }

        public void AddProperty(ParsedProperty property)
        {
            properties.Add(property.Name, property);
            //properties.Add(property);
        }

        public static bool operator ==(ParsedType parsedType1, ParsedType parsedType2)
        {
            //Check if names are the same
            if (parsedType1.FullName != parsedType2.FullName)
            {
                return false;
            }

            //Check if their static nature, or lack of, is equal
            if (parsedType1.IsStatic != parsedType2.IsStatic)
            {
                return false;
            }

            //Check if both ParsedTypes have the same quantities of each set of member types
            bool varLengthUnEqual = parsedType1.Variables.Length != parsedType2.Variables.Length;
            bool methodLengthUnEqual = parsedType1.Methods.Length != parsedType2.Methods.Length;
            bool propertyLengthUnEqual = parsedType1.Properties.Length != parsedType2.Properties.Length;
            bool constLengthUnEqual = parsedType1.Constructors.Length != parsedType2.Constructors.Length;
            if (varLengthUnEqual || methodLengthUnEqual || propertyLengthUnEqual || constLengthUnEqual)
            {
                return false;
            }

            //Check that variables are equal
            foreach (string type1key in parsedType1.variables.Keys)
            {
                if (parsedType2.variables.ContainsKey(type1key))
                {
                    ParsedVariable var1 = parsedType1.variables[type1key];
                    ParsedVariable var2 = parsedType2.variables[type1key];

                    if (var1 != var2)
                    {
                        return false;
                    }
                }
            }

            //Check that methods are equal
            foreach (string type1Key in parsedType1.methods.Keys)
            {
                if (parsedType2.methods.ContainsKey(type1Key))
                {
                    ParsedMethod method1 = parsedType1.methods[type1Key];
                    ParsedMethod method2 = parsedType2.methods[type1Key];

                    if (method1 != method2)
                    {
                        return false;
                    }
                }
            }

            foreach (string type1Key in parsedType1.properties.Keys)
            {
                if (parsedType2.properties.ContainsKey(type1Key))
                {
                    ParsedProperty prop1 = parsedType1.properties[type1Key];
                    ParsedProperty prop2 = parsedType2.properties[type1Key];

                    if (prop1 != prop2)
                    {
                        return false;
                    }
                }
            }

            foreach (string type1Key in parsedType1.constructors.Keys)
            {
                if (parsedType2.properties.ContainsKey(type1Key))
                {
                    ParsedConstructor const1 = parsedType1.constructors[type1Key] as ParsedConstructor;
                    ParsedConstructor const2 = parsedType2.constructors[type1Key] as ParsedConstructor;

                    if (const1 != const2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(ParsedType parsedType1, ParsedType parsedType2)
        {
            return !(parsedType1 == parsedType2);
        }
    }
}
