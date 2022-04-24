using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PortfolioGeneratorBackend;

namespace UnitTests
{
    public static class ReflectionParsing
    {
        const string demoAssemblyDir = "..\\..\\..\\DemoAssembly\\bin\\Debug";
        
        public static void ClassParsing()
        {
            ParsedAssembly parsedAssembly;
            Exception err;
            string dllPath = Path.GetFullPath( Path.Combine(Directory.GetCurrentDirectory(), demoAssemblyDir, "DemoAssembly.dll"));
            
            if (!File.Exists(dllPath))
            {
                throw new FileNotFoundException(string.Format("Couldn't find file \"{0}\". Try building the DemoAssembly project in Visual Studio.", dllPath));
            }

            if (ReflectionParser.ParseAssembly(dllPath, out parsedAssembly, out err))
            {
                ParsedType classA;
                if (parsedAssembly.HasType("DemoAssembly.ClassA", out classA))
                {
                    classA.CheckParsedVariable("priv", PROTECTION.PRIVATE);
                    classA.CheckParsedVariable("pub", PROTECTION.PUBLIC);
                    classA.CheckParsedVariable("privProt", PROTECTION.PRIVATE_PROTECTED);
                    classA.CheckParsedVariable("protIntern", PROTECTION.PROTECTED_INTERNAL);

                    classA.CheckParsedVariable("privStatic", PROTECTION.PRIVATE, true);
                    classA.CheckParsedVariable("pubStatic", PROTECTION.PUBLIC, true);
                    classA.CheckParsedVariable("privProtStatic", PROTECTION.PRIVATE_PROTECTED, true);
                    classA.CheckParsedVariable("protInternStatic", PROTECTION.PROTECTED_INTERNAL, true);


                }
                else
                {
                    throw new Exception("An expected class type is not present in the parsed assembly.");
                }

                ParsedEnum bloop;
                if (parsedAssembly.HasEnum("DemoAssembly.BLOOP", out bloop))
                {
                    EnumValue valTwo;
                    if (bloop.HasValue("TWO",out valTwo))
                    {
                        if (valTwo.Value != 2)
                        {
                            throw new Exception("A parsed enum had an unexpected int value.");
                        }
                    }
                    else
                    {
                        throw new Exception("Expected enum value not found.");
                    }

                    EnumValue valThree;
                    if (bloop.HasValue("THREE", out valThree))
                    {
                        if (valThree.Value != 3)
                        {
                            throw new Exception("A parsed enum had an unexpected int value.");
                        }
                    }
                    else
                    {
                        throw new Exception("Expected enum value not found.");
                    }
                }
            }
            else
            {
                throw new Exception("Assembly failed to be parsed.");
            }

        }

        static void CheckParsedVariable(this ParsedType parsedType, string varName, PROTECTION protectionLevel, bool isStatic = false)
        {
            ParsedVariable parsedVariable;
            if (parsedType.HasVariable(varName, out parsedVariable))
            {
                if (parsedVariable.ProtectionLevel != protectionLevel)
                {
                    throw new Exception(string.Format("Protection level of a {0} variable has been incorrectly parsed.", protectionLevel));
                }

                if (parsedVariable.IsStatic != isStatic)
                {
                    string strExpectedStatic = isStatic ? "static" : "non-static";
                    string strParsedStatic = parsedVariable.IsStatic ? "static" : "non-static";
                    string message = string.Format("A {0} variable has been incorrectly parsed as a {1} variable.", strExpectedStatic, strParsedStatic);
                    throw new Exception(message);
                }
            }
            else
            {
                string message = string.Format("An expected variable member called \"{0}\" in \"{1}\" is not present in the parsed type.", varName, parsedType.Name);
                throw new Exception(message);
            }
        }
    }
}