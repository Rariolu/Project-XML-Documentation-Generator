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

namespace PortfolioXMLGenerator
{
    public partial class MainForm : Form
    {
        public int blep;
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

        ParsedAssembly assembly;
        MemberDict parsedDocumentationMembers;
        private void BtnProcessAssembly_Click(object sender, EventArgs e)
        {
            //if (File.Exists(tbAssemblyPath.Text))
            //{
            //    Assembly assembly = Assembly.LoadFile(tbAssemblyPath.Text);


            //    foreach(Type type in assembly.GetTypes())
            //    {
            //        rtbLog.Text += "\n" + type.ToString();
            //        foreach(MemberInfo member in type.GetMembers())
            //        {
            //            rtbLog.Text += "\n\t" + member.Name;
            //        }
            //    }
            //}

            if (ReflectionParser.ParseAssembly(tbAssemblyPath.Text, out assembly))
            {
                btnSaveAssembly.Enabled = true;
                foreach(ParsedType type in assembly.ParsedTypes)
                {
                    rtbLog.Text += "\n" + type.Name;

                    foreach(ParsedVariable variable in type.Variables)
                    {
                        rtbLog.Text += "\n\t" + string.Format("{0} {1}",protectionSymbols[variable.ProtectionLevel], variable.Name);
                    }

                    foreach (ParsedProperty property in type.Properties)
                    {
                        string getter = "no getter";
                        PROTECTION getterProt;
                        if (property.HasAccessor(ACCESSOR_TYPE.GETTER, out getterProt))
                        {
                            getter = getterProt.ToString().ToLower() + " getter";
                        }

                        string setter = "no setter";
                        PROTECTION setterProt;
                        if (property.HasAccessor(ACCESSOR_TYPE.SETTER, out setterProt))
                        {
                            setter = setterProt.ToString().ToLower() + " setter";
                        }
                        rtbLog.Text += "\n\t" + string.Format("{0} ({1}, {2})", property.Name, getter, setter);
                    }

                    foreach (ParsedMethod method in type.Methods)
                    {
                        rtbLog.Text += "\n\t" + string.Format("{0} {1}",protectionSymbols[method.ProtectionLevel], method.Name);
                    }


                }
            }
        }

        Dictionary<PROTECTION, string> protectionSymbols = new Dictionary<PROTECTION, string>()
        {
            {PROTECTION.PRIVATE, "-"},
            {PROTECTION.PROTECTED, "#"},
            {PROTECTION.PUBLIC, "+"}
        };

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
            //if (File.Exists(tbDocumentationPath.Text))
            //{
            //    ParseNode node;
            //    if (XMLDocumentationParser.ParseDocumentationFile(tbDocumentationPath.Text, out node))
            //    {
            //        ProcessNode(node);
            //    }
            //}
            parsedDocumentationMembers = XMLDocumentationParser.ParseDocumentationFile(tbDocumentationPath.Text);
            foreach(ParsedMember member in parsedDocumentationMembers.Values)
            {
                string message = string.Format("\nName: {0}; Description: {1}; Type: {2};", member.FullName, member.Description, member.MemberType);
                rtbParseLog.AppendText(message);
            }
        }

        void ProcessNode(ParseNode node)
        {
            if (!string.IsNullOrEmpty(node.Description))
            {
                string message = string.Format("\nName: {0}; Description: {1}; Type: {2};", node.Name, node.Description, node.Type);
                rtbParseLog.AppendText(message);
            }
            foreach(ParseNode child in node.Children)
            {
                ProcessNode(child);
            }
        }

        private void btnSaveAssembly_Click(object sender, EventArgs e)
        {
            foreach(ParsedType type in assembly.ParsedTypes)
            {
                string fullname = type.FullName;
                if (parsedDocumentationMembers.ContainsKey(fullname))
                {
                    type.Description = parsedDocumentationMembers[fullname].Description;
                }

                foreach(ParsedMethod method in type.Methods)
                {
                    string methodFullName = fullname + "." + method.CompleteName;
                    //string methodFullName = fullname + "." + method.Name;
                    //if (method.Parameters.Length > 0)
                    //{
                    //    string paramStr = "";
                    //    for(int i = 0; i < method.Parameters.Length; i++)
                    //    {
                    //        paramStr += method.Parameters[i].Type;
                    //        if (i < method.Parameters.Length-1)
                    //        {
                    //            paramStr += ",";
                    //        }
                    //    }
                    //    methodFullName += string.Format("({0})", paramStr);
                    //}

                    if (parsedDocumentationMembers.ContainsKey(methodFullName))
                    {
                        ParsedMemberMethod parsedMethod = parsedDocumentationMembers[methodFullName] as ParsedMemberMethod;
                        method.Description = parsedMethod.Description;

                        foreach(ParsedParameter param in method.Parameters)
                        {
                            //TODO: Write better version later
                            foreach(ParsedMemberMethodParam docParam in parsedMethod.Parameters)
                            {
                                if (param.Name == docParam.Name)
                                {
                                    param.Description = docParam.Description;
                                    break;
                                }
                            }
                            //method.Parameters
                        }
                    }
                }

                foreach(ParsedVariable variable in type.Variables)
                {
                    string varFullName = fullname + "." + variable.Name;
                    if (parsedDocumentationMembers.ContainsKey(varFullName))
                    {
                        variable.Description = parsedDocumentationMembers[varFullName].Description;
                    }
                }

                foreach(ParsedProperty property in type.Properties)
                {
                    string propFullName = fullname + "." + property.Name;
                    if (parsedDocumentationMembers.ContainsKey(propFullName))
                    {
                        property.Description = parsedDocumentationMembers[propFullName].Description;
                    }
                }

                foreach(ParsedConstructor constructor in type.Constructors)
                {
                    string constructorFullName = fullname + "." + constructor.CompleteName;
                    if (parsedDocumentationMembers.ContainsKey(constructorFullName))
                    {
                        ParsedMemberMethod parsedConstructor = parsedDocumentationMembers[constructorFullName] as ParsedMemberMethod;

                        constructor.Description = parsedConstructor.Description;

                        foreach (ParsedParameter param in constructor.Parameters)
                        {
                            //TODO: Write better version later
                            foreach (ParsedMemberMethodParam docParam in parsedConstructor.Parameters)
                            {
                                if (param.Name == docParam.Name)
                                {
                                    param.Description = docParam.Description;
                                    break;
                                }
                            }
                            //method.Parameters
                        }
                    }
                }
            }
            XMLPortfolioSerialiser.SerialiseParsedElements(assembly, tbAssemblyOutputPath.Text);
        }

        private void btnBrowseAssemblyDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbAssemblyOutputPath.Text = fbd.SelectedPath;
            }
        }
    }
}
