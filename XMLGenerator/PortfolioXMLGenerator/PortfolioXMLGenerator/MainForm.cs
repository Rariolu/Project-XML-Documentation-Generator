using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using PortfolioGeneratorBackend;

namespace PortfolioXMLGenerator
{
    public partial class MainForm : Form
    {
        ParsedAssembly assembly = null;
        MemberDict parsedDocumentationMembers = null;

        Dictionary<PROTECTION, string> protectionSymbols = new Dictionary<PROTECTION, string>()
        {
            {PROTECTION.PRIVATE, "-"},
            {PROTECTION.PROTECTED, "#"},
            {PROTECTION.PUBLIC, "+"}
        };

        string GetProtectionLevel(PROTECTION protLevel)
        {
            if (protectionSymbols.ContainsKey(protLevel))
            {
                return protectionSymbols[protLevel];
            }
            return "?";
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdOpen = new OpenFileDialog();
            fdOpen.Multiselect = false;
            fdOpen.Filter = "DLL files|*.dll|Executables|*.exe";
            if (fdOpen.ShowDialog() == DialogResult.OK)
            {
                tbAssemblyPath.Text = fdOpen.FileName;
            }
        }

        private void BtnProcessAssembly_Click(object sender, EventArgs e)
        {
            Exception err;
            if (ReflectionParser.ParseAssembly(tbAssemblyPath.Text, out assembly, out err))
            {
                btnSaveAssembly.Enabled = true;
                if (parsedDocumentationMembers != null)
                {
                    btnIntegrate.Enabled = true;
                }
                LogAssembly(assembly, rtbLog);
            }
            else
            {
                if (err is ReflectionTypeLoadException)
                {
                    ReflectionTypeLoadException rtle = err as ReflectionTypeLoadException;

                    StringBuilder sb = new StringBuilder();
                    foreach(Exception loaderExc in rtle.LoaderExceptions)
                    {
                        sb.AppendLine(loaderExc.Message);
                    }

                    sb.AppendLine("\n" + err.StackTrace);

                    LogAlert.Show(sb.ToString());
                }
                else
                {
                    LogAlert.Show(err.Message+"\n\n"+err.StackTrace);
                }
            }
        }

        void LogAssembly(ParsedAssembly parsedAssembly, RichTextBox rtb)
        {
            foreach (ParsedType type in parsedAssembly.ParsedTypes)
            {
                rtb.Text += "\n" + type.Name;

                foreach (ParsedVariable variable in type.Variables)
                {
                    rtb.Text += "\n\t" + string.Format("{0} {1}", GetProtectionLevel(variable.ProtectionLevel), variable.Name);
                }

                foreach (ParsedProperty property in type.Properties)
                {
                    string getter = "no getter";
                    ParsedPropertyAccessor getAccessor;
                    if (property.HasAccessor(ACCESSOR_TYPE.GETTER, out getAccessor))
                    {
                        getter = getAccessor.ProtectionLevel.ToString().ToLower() + " getter";
                    }

                    string setter = "no setter";
                    ParsedPropertyAccessor setAccessor;
                    if (property.HasAccessor(ACCESSOR_TYPE.SETTER, out setAccessor))
                    {
                        setter = setAccessor.ProtectionLevel.ToString().ToLower() + " setter";
                    }
                    rtb.Text += "\n\t" + string.Format("{0} ({1}, {2})", property.Name, getter, setter);
                }

                foreach(ParsedMethod constructor in type.Constructors)
                {
                    rtb.Text += "\n\t" + string.Format("{0} {1}", GetProtectionLevel(constructor.ProtectionLevel), constructor.CompleteName);
                }

                foreach (ParsedMethod method in type.Methods)
                {
                    rtb.Text += "\n\t" + string.Format("{0} {1}", GetProtectionLevel(method.ProtectionLevel), method.CompleteName);
                }


            }
        }

        private void BtnBrowseDocumentation_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdOpen = new OpenFileDialog();
            fdOpen.Multiselect = false;
            fdOpen.Filter = "XML files|*.xml";
            if (fdOpen.ShowDialog() == DialogResult.OK)
            {
                tbDocumentationPath.Text = fdOpen.FileName;
            }
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            parsedDocumentationMembers = XMLDocumentationParser.ParseDocumentationFile(tbDocumentationPath.Text);
            if (assembly != null)
            {
                btnIntegrate.Enabled = true;
            }
            foreach(ParsedMember member in parsedDocumentationMembers.Values)
            {
                string message = string.Format("\nName: {0}; Description: {1}; Type: {2};", member.FullName, member.Description, member.MemberType);
                rtbParseLog.AppendText(message);
            }
        }

        /// <summary>
        /// Eventhandler called when "btnSaveAssembly" is clicked.
        /// </summary>
        /// <param name="sender">The control that sent the quested.</param>
        /// <param name="e"></param>
        private void btnSaveAssembly_Click(object sender, EventArgs e)
        {
            assembly.IntegrateParsedDocumentation(parsedDocumentationMembers);
            //foreach(ParsedType type in assembly.ParsedTypes)
            //{
            //    string fullname = type.FullName;
            //    if (parsedDocumentationMembers.ContainsKey(fullname))
            //    {
            //        type.Description = parsedDocumentationMembers[fullname].Description;
            //    }

            //    foreach(ParsedMethod method in type.Methods)
            //    {
            //        string methodFullName = fullname + "." + method.CompleteName;

            //        if (parsedDocumentationMembers.ContainsKey(methodFullName))
            //        {
            //            ParsedMemberMethod parsedMethod = parsedDocumentationMembers[methodFullName] as ParsedMemberMethod;
            //            method.Description = parsedMethod.Description;

            //            if (method.Parameters.Length > 0)
            //            {
            //                foreach (ParsedParameter param in method.Parameters)
            //                {
            //                    //TODO: Write better version later
            //                    foreach (ParsedMemberMethodParam docParam in parsedMethod.Parameters)
            //                    {
            //                        if (param.Name == docParam.Name)
            //                        {
            //                            param.Description = docParam.Description;
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    foreach(ParsedVariable variable in type.Variables)
            //    {
            //        string varFullName = fullname + "." + variable.Name;
            //        if (parsedDocumentationMembers.ContainsKey(varFullName))
            //        {
            //            variable.Description = parsedDocumentationMembers[varFullName].Description;
            //        }
            //    }

            //    foreach(ParsedProperty property in type.Properties)
            //    {
            //        string propFullName = fullname + "." + property.Name;
            //        if (parsedDocumentationMembers.ContainsKey(propFullName))
            //        {
            //            property.Description = parsedDocumentationMembers[propFullName].Description;
            //        }
            //    }

            //    foreach(ParsedConstructor constructor in type.Constructors)
            //    {
            //        string constructorFullName = fullname + "." + constructor.CompleteName;
            //        if (parsedDocumentationMembers.ContainsKey(constructorFullName))
            //        {
            //            ParsedMemberMethod parsedConstructor = parsedDocumentationMembers[constructorFullName] as ParsedMemberMethod;

            //            constructor.Description = parsedConstructor.Description;

            //            foreach (ParsedParameter param in constructor.Parameters)
            //            {
            //                //TODO: Write better version later
            //                foreach (ParsedMemberMethodParam docParam in parsedConstructor.Parameters)
            //                {
            //                    if (param.Name == docParam.Name)
            //                    {
            //                        param.Description = docParam.Description;
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            Exception errors;
            if (XMLPortfolioSerialiser.SerialiseParsedElements(assembly, tbAssemblyOutputPath.Text, out errors, tbAssemblyName.Text))
            {
                MessageBox.Show("Saved.");
            }
            else
            {
                LogAlert.Show(errors.Message);
            }
        }

        private void btnBrowseAssemblyDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbAssemblyOutputPath.Text = fbd.SelectedPath;
            }
        }

        private void btnBrowsePortfolioXML_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbPortfolioDir.Text = fbd.SelectedPath;
            }
        }

        private void btnParsePortfolio_Click(object sender, EventArgs e)
        {
            ParsedAssembly parseAssembly;
            if (XMLPortfolioParser.ParsePortfolio(tbPortfolioDir.Text, out parseAssembly))
            {
                LogAssembly(parseAssembly, rtbPortfolioParse);
            }
        }

        private void btnIntegrate_Click(object sender, EventArgs e)
        {
            assembly.IntegrateParsedDocumentation(parsedDocumentationMembers);

            rtbIntegrationTest.AppendText("Types:\n\n");

            foreach(ParsedType parsedType in assembly.ParsedTypes)
            {
                if (parsedType.Name == "ParsedAssembly")
                {
                    int j = 45;
                }

                string type = string.Format("{0}: {1}\n", parsedType.Name, parsedType.Description.RemoveIndentsAndNewLines());
                rtbIntegrationTest.AppendText(type);

                rtbIntegrationTest.AppendText("\tVariables:\n");

                foreach(ParsedVariable parsedVariable in parsedType.Variables)
                {
                    string var = string.Format("\t\t{0}: {1};\n", parsedVariable.Name, parsedVariable.Description.RemoveIndentsAndNewLines());
                    rtbIntegrationTest.AppendText(var);
                }

                rtbIntegrationTest.AppendText("\tConstructors:\n");

                foreach(ParsedConstructor parsedConstructor in parsedType.Constructors)
                {
                    string constructor = string.Format("\t\t{0}: {1};\n", parsedConstructor.CompleteName, parsedConstructor.Description.RemoveIndentsAndNewLines());
                    rtbIntegrationTest.AppendText(constructor);
                }

                rtbIntegrationTest.AppendText("\tMethods:\n");

            
                foreach(ParsedMethod parsedMethod in parsedType.Methods)
                {
                    string method = string.Format("\t\t{0}: {1};\n", parsedMethod.Name, parsedMethod.Description.RemoveIndentsAndNewLines());
                    rtbIntegrationTest.AppendText(method);
                }

                rtbIntegrationTest.AppendText("\tProperties:\n");

                foreach(ParsedProperty parsedProperty in parsedType.Properties)
                {
                    string property = string.Format("\t\t{0}: {1};\n", parsedProperty.Name, parsedProperty.Description.RemoveIndentsAndNewLines());
                    rtbIntegrationTest.AppendText(property);
                }
            }

            rtbIntegrationTest.AppendText("Enums:\n\n");

            foreach(ParsedEnum parsedEnum in assembly.ParsedEnums)
            {
                string _enum = string.Format("{0}: {1}\n", parsedEnum.Name, parsedEnum.Description.RemoveIndentsAndNewLines());
                rtbIntegrationTest.AppendText(_enum);


                EnumValue[] enumValues = parsedEnum.Values.Select(kp => kp.Value).ToArray();
                //foreach(EnumValue enumValue in parsedEnum.Values)
                foreach(EnumValue enumValue in enumValues)
                {
                    string valueStr = string.Format("\t{0}:{1};\n", enumValue.Name, enumValue.Description.RemoveIndentsAndNewLines());
                    rtbIntegrationTest.AppendText(valueStr);
                }
            }
        }


    }
}