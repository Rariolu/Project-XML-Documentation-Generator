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
            if (File.Exists(tbAssemblyPath.Text))
            {
                Assembly assembly = Assembly.LoadFile(tbAssemblyPath.Text);
                
                
                foreach(Type type in assembly.GetExportedTypes())
                {
                    rtbLog.Text += "\n" + type.ToString();
                    foreach(MemberInfo member in type.GetMembers())
                    {
                        rtbLog.Text += "\n\t" + member.Name;
                    }
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
            if (File.Exists(tbDocumentationPath.Text))
            {
                ParseNode node;
                if (XMLDocumentationParser.ParseDocumentationFile(tbDocumentationPath.Text, out node))
                {
                    ProcessNode(node);
                }
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
    }
}
