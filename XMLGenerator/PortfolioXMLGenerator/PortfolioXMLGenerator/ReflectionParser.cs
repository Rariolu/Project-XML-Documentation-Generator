using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioXMLGenerator
{
    /// <summary>
    /// A utility for "parsing" assemblies.
    /// </summary>
    public static class ReflectionParser
    {
        public static bool ParseAssembly(string file, out ParsedAssembly parsedAssembly)
        {
            parsedAssembly = null;
            if (!File.Exists(file))
            {
                return false;
            }
            
            try
            {
                Assembly assembly = Assembly.LoadFrom(file);

                parsedAssembly = new ParsedAssembly(assembly.FullName);

                Type[] types = assembly.GetTypes();

                foreach(Type type in types)
                {
                    if (!type.IsEnum)
                    {
                        string typeName;
                        string typeNamespace;
                        GetNamespaceAndType(type.FullName, out typeNamespace, out typeName);
                        //string _namespace = type.FullName.TrimEnd(("." + type.Name).ToCharArray());
                        ParsedType parsedType = new ParsedType(typeName, typeNamespace);//type.FullName);
                        

                        foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
                        {
                            string fieldType;
                            string fieldNamespace;
                            GetNamespaceAndType(field.FieldType.ToString(), out fieldNamespace, out fieldType);

                            ParsedVariable parsedVariable = new ParsedVariable(field.Name, fieldType, fieldNamespace, field.GetProtectionLevel(), field.IsStatic);

                            parsedType.AddVariable(parsedVariable);
                        }

                        foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                        {
                            string methodType;
                            string methodNamespace;
                            GetNamespaceAndType(method.ReturnType.ToString(), out methodNamespace, out methodType);
                            //ParsedMethod parsedMethod = new ParsedMethod(method.Name, method.ReturnType.ToString(), method.GetProtectionLevel(), method.IsStatic);
                            ParsedMethod parsedMethod = new ParsedMethod(method.Name, methodType, methodNamespace, method.GetProtectionLevel(), method.IsStatic);

                            ParameterInfo[] parameters = method.GetParameters();



                            foreach (ParameterInfo parameter in parameters)
                            {
                                string parameterType;
                                string parameterNamespace;
                                GetNamespaceAndType(parameter.ParameterType.ToString(), out parameterNamespace, out parameterType);
                                ParsedParameter parsedParameter = new ParsedParameter(parameter.Name,parameterType, parameterNamespace);
                                //parsedParameter.Name = parameter.Name;
                                //parsedParameter.Type = parameter.ParameterType.ToString();
                                parsedMethod.AddParameter(parsedParameter);
                            }

                            parsedType.AddMethod(parsedMethod);
                        }

                        foreach(PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                        {
                            string propertyType;
                            string propertyNamespace;
                            GetNamespaceAndType(property.PropertyType.ToString(), out propertyNamespace, out propertyType);
                            //ParsedProperty parsedProperty = new ParsedProperty(property.Name, property.PropertyType.ToString());
                            ParsedProperty parsedProperty = new ParsedProperty(property.Name, propertyType,propertyNamespace);

                            MethodInfo getter = property.GetGetMethod(true);

                            if (getter != null)
                            {
                                parsedProperty.AddAccessor(ACCESSOR_TYPE.GETTER, getter.GetProtectionLevel(), getter.IsStatic);
                            }

                            MethodInfo setter = property.GetSetMethod(true);

                            if (setter != null)
                            {

                                //ParsedAccessor accessor = new ParsedAccessor(ACCESSOR_TYPE.SETTER, setter.GetProtectionLevel());
                                //parsedProperty.AddAccessor(ACCESSOR_TYPE.SETTER, setter.GetProtectionLevel());//(accessor);
                                parsedProperty.AddAccessor(ACCESSOR_TYPE.SETTER, setter.GetProtectionLevel(), setter.IsStatic);
                            }

                            parsedType.AddProperty(parsedProperty);
                        }

                        foreach (ConstructorInfo constructor in type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            ParsedConstructor parsedConstructor = new ParsedConstructor(constructor.GetProtectionLevel(), constructor.IsStatic);

                            ParameterInfo[] parameters = constructor.GetParameters();

                            foreach (ParameterInfo parameter in parameters)
                            {
                                string parameterType;
                                string parameterNamespace;
                                GetNamespaceAndType(parameter.ParameterType.ToString(), out parameterNamespace, out parameterType);
                                ParsedParameter parsedParameter = new ParsedParameter(parameter.Name, parameterType, parameterNamespace);
                                //ParsedParameter parsedParameter = new ParsedParameter();
                                //parsedParameter.Name = parameter.Name;
                                //parsedParameter.Type = parameter.ParameterType.ToString();
                                parsedConstructor.AddParameter(parsedParameter);
                            }

                            parsedType.AddMethod(parsedConstructor, true);
                        }

                        parsedAssembly.AddType(parsedType);
                    }
                }

                return true;
            }
            catch(Exception err)
            {
                Console.WriteLine("ERROR: "+err.Message);
                return false;
            }
        }

        public static void GetNamespaceAndType(this string str, out string nameSpace, out string type)
        {
            int dotIndex = str.LastIndexOf('.');
            if (dotIndex < 0)
            {
                nameSpace = "";
                type = str;
                return;
            }
            string n = str.Substring(0, dotIndex);
            nameSpace = n;

            string t = str.Substring(dotIndex + 1, str.Length - (dotIndex+1));
            type = t;
        }

        public static PROTECTION GetProtectionLevel(this MethodInfo method)
        {
            PROTECTION protection = method.IsPublic ? PROTECTION.PUBLIC :
            (
                method.IsPrivate ? PROTECTION.PRIVATE : PROTECTION.PROTECTED
            );
            return protection;
        }

        public static PROTECTION GetProtectionLevel(this FieldInfo field)
        {
            PROTECTION protection = field.IsPublic ? PROTECTION.PUBLIC :
            (
                field.IsPrivate ? PROTECTION.PRIVATE : PROTECTION.PROTECTED
            );
            return protection;
        }

        public static PROTECTION GetProtectionLevel(this ConstructorInfo constructor)
        {
            PROTECTION protection = constructor.IsPublic ? PROTECTION.PUBLIC :
            (
                constructor.IsPrivate ? PROTECTION.PRIVATE : PROTECTION.PROTECTED
            );
            return protection;
        }
    }
}