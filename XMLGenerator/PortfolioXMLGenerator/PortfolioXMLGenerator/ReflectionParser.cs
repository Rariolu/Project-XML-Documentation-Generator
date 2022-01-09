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
                        ParsedType parsedType = new ParsedType(type.Name, type.FullName);
                        

                        foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                        {
                            
                            ParsedVariable parsedVariable = new ParsedVariable(field.Name, field.FieldType.ToString(), field.GetProtectionLevel(), field.IsStatic);
                            
                            parsedType.AddVariable(parsedVariable);
                        }

                        foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            
                            ParsedMethod parsedMethod = new ParsedMethod(method.Name, method.ReturnType.ToString(), method.GetProtectionLevel(), method.IsStatic);

                            ParameterInfo[] parameters = method.GetParameters();



                            foreach (ParameterInfo parameter in parameters)
                            {
                                ParsedParameter parsedParameter = new ParsedParameter();
                                parsedParameter.Name = parameter.Name;
                                parsedParameter.Type = parameter.ParameterType.ToString();
                                parsedMethod.AddParameter(parsedParameter);
                            }

                            parsedType.AddMethod(parsedMethod);
                        }

                        foreach(PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            ParsedProperty parsedProperty = new ParsedProperty(property.Name, property.PropertyType.ToString());

                            MethodInfo getter = property.GetGetMethod(true);

                            if (getter != null)
                            {
                                //ParsedAccessor accessor = new ParsedAccessor(ACCESSOR_TYPE.GETTER, getter.GetProtectionLevel());
                                //parsedProperty.AddAccessor(ACCESSOR_TYPE.GETTER, getter.GetProtectionLevel());//(accessor);
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
                                ParsedParameter parsedParameter = new ParsedParameter();
                                parsedParameter.Name = parameter.Name;
                                parsedParameter.Type = parameter.ParameterType.ToString();
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

        public static int I
        {
            get
            {
                return 42;
            }
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