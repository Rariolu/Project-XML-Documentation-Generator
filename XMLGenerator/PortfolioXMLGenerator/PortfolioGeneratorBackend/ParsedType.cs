using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    /// <summary>
    /// The information regarding a parsed class, storing members and descriptions.
    /// </summary>
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

        Dictionary<string, ParsedMethod> methods = new Dictionary<string, ParsedMethod>();
        /// <summary>
        /// The (non-constructor) methods of this parsed type.
        /// </summary>
        public ParsedMethod[] Methods
        {
            get
            {
                return methods.Values.ToArray();
            }
        }

        Dictionary<string, ParsedVariable> variables = new Dictionary<string, ParsedVariable>();
        /// <summary>
        /// The variables of this parsed type.
        /// </summary>
        public ParsedVariable[] Variables
        {
            get
            {
                return variables.Values.ToArray();
            }
        }

        Dictionary<string, ParsedProperty> properties = new Dictionary<string, ParsedProperty>();
        /// <summary>
        /// The properties of this parsed type.
        /// </summary>
        public ParsedProperty[] Properties
        {
            get
            {
                return properties.Values.ToArray();
            }
        }

        Dictionary<string, ParsedMethod> constructors = new Dictionary<string, ParsedMethod>();
        /// <summary>
        /// The constructors of this parsed type.
        /// </summary>
        public ParsedMethod[] Constructors
        {
            get
            {
                return constructors.Values.ToArray();
            }
        }

        List<string> genericTypeParameters = new List<string>();
        /// <summary>
        /// The names of the generic types that are incorporated in this type.
        /// </summary>
        public string[] GenericTypeParameters
        {
            get
            {
                return genericTypeParameters.ToArray();
            }
        }

        public ParsedType(string _name, string _namespace, bool _isStatic = false)
            : base(_name, _name, _namespace, _isStatic)
        {
            
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

        public bool HasVariable(string variableName, out ParsedVariable parsedVariable)
        {
            if (variables.ContainsKey(variableName))
            {
                parsedVariable = variables[variableName];
                return true;
            }
            parsedVariable = null;
            return false;
        }

        public bool HasMethod(string methodName, out ParsedMethod parsedMethod)
        {
            if (methods.ContainsKey(methodName))
            {
                parsedMethod = methods[methodName];
                return true;
            }
            parsedMethod = null;
            return false;
        }

        public bool HasConstructor(string constructorName, out ParsedConstructor parsedConstructor)
        {
            if (constructors.ContainsKey(constructorName))
            {
                parsedConstructor = constructors[constructorName] as ParsedConstructor;
                return true;
            }
            parsedConstructor = null;
            return false;
        }

        public bool HasProperty(string propertyName, out ParsedProperty parsedProperty)
        {
            if (properties.ContainsKey(propertyName))
            {
                parsedProperty = properties[propertyName];
                return true;
            }
            parsedProperty = null;
            return false;
        }

        public void AddGenericParameterType(string typeName)
        {
            genericTypeParameters.Add(typeName);
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
