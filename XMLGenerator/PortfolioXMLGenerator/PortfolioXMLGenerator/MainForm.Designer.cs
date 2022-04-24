
namespace PortfolioXMLGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbAssemblyPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnProcessAssembly = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.gbAssembly = new System.Windows.Forms.GroupBox();
            this.btnBrowseAssemblyDir = new System.Windows.Forms.Button();
            this.tbAssemblyOutputPath = new System.Windows.Forms.TextBox();
            this.btnSaveAssembly = new System.Windows.Forms.Button();
            this.gbDocumentation = new System.Windows.Forms.GroupBox();
            this.rtbParseLog = new System.Windows.Forms.RichTextBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.btnBrowseDocumentation = new System.Windows.Forms.Button();
            this.tbDocumentationPath = new System.Windows.Forms.TextBox();
            this.gbPortfolio = new System.Windows.Forms.GroupBox();
            this.btnParsePortfolio = new System.Windows.Forms.Button();
            this.rtbPortfolioParse = new System.Windows.Forms.RichTextBox();
            this.btnBrowsePortfolioXML = new System.Windows.Forms.Button();
            this.tbPortfolioDir = new System.Windows.Forms.TextBox();
            this.gbSaveParsedAssembly = new System.Windows.Forms.GroupBox();
            this.lblAssemblyName = new System.Windows.Forms.Label();
            this.tbAssemblyName = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnIntegrate = new System.Windows.Forms.Button();
            this.gbIntegrationTest = new System.Windows.Forms.GroupBox();
            this.rtbIntegrationTest = new System.Windows.Forms.RichTextBox();
            this.gbAssembly.SuspendLayout();
            this.gbDocumentation.SuspendLayout();
            this.gbPortfolio.SuspendLayout();
            this.gbSaveParsedAssembly.SuspendLayout();
            this.gbIntegrationTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbAssemblyPath
            // 
            this.tbAssemblyPath.Location = new System.Drawing.Point(8, 36);
            this.tbAssemblyPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbAssemblyPath.Name = "tbAssemblyPath";
            this.tbAssemblyPath.Size = new System.Drawing.Size(257, 22);
            this.tbAssemblyPath.TabIndex = 0;
            this.tbAssemblyPath.Text = "C:\\Users\\maxr1\\source\\repos\\Project-XML-Documentation-Generator\\XMLGenerator\\Port" +
    "folioXMLGenerator\\PortfolioGeneratorBackend\\bin\\Debug\\PortfolioGeneratorBackend." +
    "dll";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(275, 36);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 28);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // btnProcessAssembly
            // 
            this.btnProcessAssembly.Location = new System.Drawing.Point(8, 79);
            this.btnProcessAssembly.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcessAssembly.Name = "btnProcessAssembly";
            this.btnProcessAssembly.Size = new System.Drawing.Size(155, 49);
            this.btnProcessAssembly.TabIndex = 2;
            this.btnProcessAssembly.Text = "Process Assembly";
            this.btnProcessAssembly.UseVisualStyleBackColor = true;
            this.btnProcessAssembly.Click += new System.EventHandler(this.BtnProcessAssembly_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(8, 153);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(4);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(432, 281);
            this.rtbLog.TabIndex = 3;
            this.rtbLog.Text = "";
            // 
            // gbAssembly
            // 
            this.gbAssembly.Controls.Add(this.tbAssemblyPath);
            this.gbAssembly.Controls.Add(this.rtbLog);
            this.gbAssembly.Controls.Add(this.btnBrowse);
            this.gbAssembly.Controls.Add(this.btnProcessAssembly);
            this.gbAssembly.Location = new System.Drawing.Point(16, 33);
            this.gbAssembly.Margin = new System.Windows.Forms.Padding(4);
            this.gbAssembly.Name = "gbAssembly";
            this.gbAssembly.Padding = new System.Windows.Forms.Padding(4);
            this.gbAssembly.Size = new System.Drawing.Size(456, 454);
            this.gbAssembly.TabIndex = 4;
            this.gbAssembly.TabStop = false;
            this.gbAssembly.Text = "Assembly";
            // 
            // btnBrowseAssemblyDir
            // 
            this.btnBrowseAssemblyDir.Location = new System.Drawing.Point(332, 33);
            this.btnBrowseAssemblyDir.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseAssemblyDir.Name = "btnBrowseAssemblyDir";
            this.btnBrowseAssemblyDir.Size = new System.Drawing.Size(100, 28);
            this.btnBrowseAssemblyDir.TabIndex = 6;
            this.btnBrowseAssemblyDir.Text = "Browse";
            this.btnBrowseAssemblyDir.UseVisualStyleBackColor = true;
            this.btnBrowseAssemblyDir.Click += new System.EventHandler(this.btnBrowseAssemblyDir_Click);
            // 
            // tbAssemblyOutputPath
            // 
            this.tbAssemblyOutputPath.Location = new System.Drawing.Point(7, 36);
            this.tbAssemblyOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbAssemblyOutputPath.Name = "tbAssemblyOutputPath";
            this.tbAssemblyOutputPath.Size = new System.Drawing.Size(304, 22);
            this.tbAssemblyOutputPath.TabIndex = 5;
            this.tbAssemblyOutputPath.Text = "C:\\Users\\maxr1\\Documents\\Bloop";
            // 
            // btnSaveAssembly
            // 
            this.btnSaveAssembly.Enabled = false;
            this.btnSaveAssembly.Location = new System.Drawing.Point(7, 351);
            this.btnSaveAssembly.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveAssembly.Name = "btnSaveAssembly";
            this.btnSaveAssembly.Size = new System.Drawing.Size(155, 57);
            this.btnSaveAssembly.TabIndex = 4;
            this.btnSaveAssembly.Text = "Save";
            this.btnSaveAssembly.UseVisualStyleBackColor = true;
            this.btnSaveAssembly.Click += new System.EventHandler(this.btnSaveAssembly_Click);
            // 
            // gbDocumentation
            // 
            this.gbDocumentation.Controls.Add(this.rtbParseLog);
            this.gbDocumentation.Controls.Add(this.btnParse);
            this.gbDocumentation.Controls.Add(this.btnBrowseDocumentation);
            this.gbDocumentation.Controls.Add(this.tbDocumentationPath);
            this.gbDocumentation.Location = new System.Drawing.Point(480, 26);
            this.gbDocumentation.Margin = new System.Windows.Forms.Padding(4);
            this.gbDocumentation.Name = "gbDocumentation";
            this.gbDocumentation.Padding = new System.Windows.Forms.Padding(4);
            this.gbDocumentation.Size = new System.Drawing.Size(567, 462);
            this.gbDocumentation.TabIndex = 5;
            this.gbDocumentation.TabStop = false;
            this.gbDocumentation.Text = "Documentation";
            // 
            // rtbParseLog
            // 
            this.rtbParseLog.Location = new System.Drawing.Point(8, 153);
            this.rtbParseLog.Margin = new System.Windows.Forms.Padding(4);
            this.rtbParseLog.Name = "rtbParseLog";
            this.rtbParseLog.Size = new System.Drawing.Size(485, 281);
            this.rtbParseLog.TabIndex = 3;
            this.rtbParseLog.Text = "";
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(8, 79);
            this.btnParse.Margin = new System.Windows.Forms.Padding(4);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(157, 59);
            this.btnParse.TabIndex = 2;
            this.btnParse.Text = "Parse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // btnBrowseDocumentation
            // 
            this.btnBrowseDocumentation.Location = new System.Drawing.Point(359, 36);
            this.btnBrowseDocumentation.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseDocumentation.Name = "btnBrowseDocumentation";
            this.btnBrowseDocumentation.Size = new System.Drawing.Size(100, 28);
            this.btnBrowseDocumentation.TabIndex = 1;
            this.btnBrowseDocumentation.Text = "Browse";
            this.btnBrowseDocumentation.UseVisualStyleBackColor = true;
            this.btnBrowseDocumentation.Click += new System.EventHandler(this.BtnBrowseDocumentation_Click);
            // 
            // tbDocumentationPath
            // 
            this.tbDocumentationPath.Location = new System.Drawing.Point(8, 38);
            this.tbDocumentationPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbDocumentationPath.Name = "tbDocumentationPath";
            this.tbDocumentationPath.Size = new System.Drawing.Size(327, 22);
            this.tbDocumentationPath.TabIndex = 0;
            this.tbDocumentationPath.Text = "C:\\Users\\maxr1\\source\\repos\\Project-XML-Documentation-Generator\\XMLGenerator\\Port" +
    "folioXMLGenerator\\PortfolioGeneratorBackend\\bin\\Debug\\PortfolioGeneratorBackend." +
    "xml";
            // 
            // gbPortfolio
            // 
            this.gbPortfolio.Controls.Add(this.btnParsePortfolio);
            this.gbPortfolio.Controls.Add(this.rtbPortfolioParse);
            this.gbPortfolio.Controls.Add(this.btnBrowsePortfolioXML);
            this.gbPortfolio.Controls.Add(this.tbPortfolioDir);
            this.gbPortfolio.Location = new System.Drawing.Point(1055, 33);
            this.gbPortfolio.Margin = new System.Windows.Forms.Padding(4);
            this.gbPortfolio.Name = "gbPortfolio";
            this.gbPortfolio.Padding = new System.Windows.Forms.Padding(4);
            this.gbPortfolio.Size = new System.Drawing.Size(688, 454);
            this.gbPortfolio.TabIndex = 6;
            this.gbPortfolio.TabStop = false;
            this.gbPortfolio.Text = "Portfolio";
            // 
            // btnParsePortfolio
            // 
            this.btnParsePortfolio.Location = new System.Drawing.Point(8, 79);
            this.btnParsePortfolio.Margin = new System.Windows.Forms.Padding(4);
            this.btnParsePortfolio.Name = "btnParsePortfolio";
            this.btnParsePortfolio.Size = new System.Drawing.Size(157, 59);
            this.btnParsePortfolio.TabIndex = 10;
            this.btnParsePortfolio.Text = "Parse";
            this.btnParsePortfolio.UseVisualStyleBackColor = true;
            this.btnParsePortfolio.Click += new System.EventHandler(this.btnParsePortfolio_Click);
            // 
            // rtbPortfolioParse
            // 
            this.rtbPortfolioParse.Location = new System.Drawing.Point(8, 153);
            this.rtbPortfolioParse.Margin = new System.Windows.Forms.Padding(4);
            this.rtbPortfolioParse.Name = "rtbPortfolioParse";
            this.rtbPortfolioParse.Size = new System.Drawing.Size(485, 281);
            this.rtbPortfolioParse.TabIndex = 9;
            this.rtbPortfolioParse.Text = "";
            // 
            // btnBrowsePortfolioXML
            // 
            this.btnBrowsePortfolioXML.Location = new System.Drawing.Point(471, 36);
            this.btnBrowsePortfolioXML.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowsePortfolioXML.Name = "btnBrowsePortfolioXML";
            this.btnBrowsePortfolioXML.Size = new System.Drawing.Size(100, 28);
            this.btnBrowsePortfolioXML.TabIndex = 8;
            this.btnBrowsePortfolioXML.Text = "Browse";
            this.btnBrowsePortfolioXML.UseVisualStyleBackColor = true;
            this.btnBrowsePortfolioXML.Click += new System.EventHandler(this.btnBrowsePortfolioXML_Click);
            // 
            // tbPortfolioDir
            // 
            this.tbPortfolioDir.Location = new System.Drawing.Point(8, 36);
            this.tbPortfolioDir.Margin = new System.Windows.Forms.Padding(4);
            this.tbPortfolioDir.Name = "tbPortfolioDir";
            this.tbPortfolioDir.Size = new System.Drawing.Size(453, 22);
            this.tbPortfolioDir.TabIndex = 7;
            this.tbPortfolioDir.Text = "C:\\Users\\maxr1\\Documents\\Bloop";
            // 
            // gbSaveParsedAssembly
            // 
            this.gbSaveParsedAssembly.Controls.Add(this.lblAssemblyName);
            this.gbSaveParsedAssembly.Controls.Add(this.tbAssemblyName);
            this.gbSaveParsedAssembly.Controls.Add(this.richTextBox1);
            this.gbSaveParsedAssembly.Controls.Add(this.tbAssemblyOutputPath);
            this.gbSaveParsedAssembly.Controls.Add(this.btnBrowseAssemblyDir);
            this.gbSaveParsedAssembly.Controls.Add(this.btnSaveAssembly);
            this.gbSaveParsedAssembly.Location = new System.Drawing.Point(24, 494);
            this.gbSaveParsedAssembly.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbSaveParsedAssembly.Name = "gbSaveParsedAssembly";
            this.gbSaveParsedAssembly.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbSaveParsedAssembly.Size = new System.Drawing.Size(448, 414);
            this.gbSaveParsedAssembly.TabIndex = 7;
            this.gbSaveParsedAssembly.TabStop = false;
            this.gbSaveParsedAssembly.Text = "Save Parsed Assembly";
            // 
            // lblAssemblyName
            // 
            this.lblAssemblyName.AutoSize = true;
            this.lblAssemblyName.Location = new System.Drawing.Point(3, 89);
            this.lblAssemblyName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAssemblyName.Name = "lblAssemblyName";
            this.lblAssemblyName.Size = new System.Drawing.Size(177, 17);
            this.lblAssemblyName.TabIndex = 9;
            this.lblAssemblyName.Text = "Preferred Assembly Name:";
            // 
            // tbAssemblyName
            // 
            this.tbAssemblyName.Location = new System.Drawing.Point(185, 85);
            this.tbAssemblyName.Margin = new System.Windows.Forms.Padding(4);
            this.tbAssemblyName.Name = "tbAssemblyName";
            this.tbAssemblyName.Size = new System.Drawing.Size(245, 22);
            this.tbAssemblyName.TabIndex = 8;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(7, 119);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(424, 224);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // btnIntegrate
            // 
            this.btnIntegrate.Enabled = false;
            this.btnIntegrate.Location = new System.Drawing.Point(672, 21);
            this.btnIntegrate.Name = "btnIntegrate";
            this.btnIntegrate.Size = new System.Drawing.Size(122, 43);
            this.btnIntegrate.TabIndex = 8;
            this.btnIntegrate.Text = "Integrate Description";
            this.btnIntegrate.UseVisualStyleBackColor = true;
            this.btnIntegrate.Click += new System.EventHandler(this.btnIntegrate_Click);
            // 
            // gbIntegrationTest
            // 
            this.gbIntegrationTest.Controls.Add(this.rtbIntegrationTest);
            this.gbIntegrationTest.Controls.Add(this.btnIntegrate);
            this.gbIntegrationTest.Location = new System.Drawing.Point(488, 501);
            this.gbIntegrationTest.Name = "gbIntegrationTest";
            this.gbIntegrationTest.Size = new System.Drawing.Size(800, 407);
            this.gbIntegrationTest.TabIndex = 9;
            this.gbIntegrationTest.TabStop = false;
            this.gbIntegrationTest.Text = "Integration Test";
            // 
            // rtbIntegrationTest
            // 
            this.rtbIntegrationTest.Location = new System.Drawing.Point(6, 29);
            this.rtbIntegrationTest.Name = "rtbIntegrationTest";
            this.rtbIntegrationTest.Size = new System.Drawing.Size(660, 372);
            this.rtbIntegrationTest.TabIndex = 9;
            this.rtbIntegrationTest.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1759, 921);
            this.Controls.Add(this.gbIntegrationTest);
            this.Controls.Add(this.gbSaveParsedAssembly);
            this.Controls.Add(this.gbPortfolio);
            this.Controls.Add(this.gbDocumentation);
            this.Controls.Add(this.gbAssembly);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "PortfolioXMLGenerator";
            this.gbAssembly.ResumeLayout(false);
            this.gbAssembly.PerformLayout();
            this.gbDocumentation.ResumeLayout(false);
            this.gbDocumentation.PerformLayout();
            this.gbPortfolio.ResumeLayout(false);
            this.gbPortfolio.PerformLayout();
            this.gbSaveParsedAssembly.ResumeLayout(false);
            this.gbSaveParsedAssembly.PerformLayout();
            this.gbIntegrationTest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbAssemblyPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnProcessAssembly;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.GroupBox gbAssembly;
        private System.Windows.Forms.GroupBox gbDocumentation;
        private System.Windows.Forms.Button btnBrowseDocumentation;
        private System.Windows.Forms.TextBox tbDocumentationPath;
        private System.Windows.Forms.RichTextBox rtbParseLog;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Button btnSaveAssembly;
        private System.Windows.Forms.Button btnBrowseAssemblyDir;
        private System.Windows.Forms.TextBox tbAssemblyOutputPath;
        private System.Windows.Forms.GroupBox gbPortfolio;
        private System.Windows.Forms.TextBox tbPortfolioDir;
        private System.Windows.Forms.Button btnBrowsePortfolioXML;
        private System.Windows.Forms.Button btnParsePortfolio;
        private System.Windows.Forms.RichTextBox rtbPortfolioParse;
        private System.Windows.Forms.GroupBox gbSaveParsedAssembly;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblAssemblyName;
        private System.Windows.Forms.TextBox tbAssemblyName;
        private System.Windows.Forms.Button btnIntegrate;
        private System.Windows.Forms.GroupBox gbIntegrationTest;
        private System.Windows.Forms.RichTextBox rtbIntegrationTest;
    }
}

