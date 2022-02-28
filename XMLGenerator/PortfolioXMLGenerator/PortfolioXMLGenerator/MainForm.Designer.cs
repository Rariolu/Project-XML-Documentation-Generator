
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tbAssemblyName = new System.Windows.Forms.TextBox();
            this.lblAssemblyName = new System.Windows.Forms.Label();
            this.gbAssembly.SuspendLayout();
            this.gbDocumentation.SuspendLayout();
            this.gbPortfolio.SuspendLayout();
            this.gbSaveParsedAssembly.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbAssemblyPath
            // 
            this.tbAssemblyPath.Location = new System.Drawing.Point(6, 29);
            this.tbAssemblyPath.Name = "tbAssemblyPath";
            this.tbAssemblyPath.Size = new System.Drawing.Size(194, 20);
            this.tbAssemblyPath.TabIndex = 0;
            this.tbAssemblyPath.Text = "D:\\PortfolioProject\\XMLGenerator\\PortfolioXMLGenerator\\PortfolioXMLGenerator\\bin\\" +
    "Debug\\PortfolioXMLGenerator.exe";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(206, 29);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // btnProcessAssembly
            // 
            this.btnProcessAssembly.Location = new System.Drawing.Point(6, 64);
            this.btnProcessAssembly.Name = "btnProcessAssembly";
            this.btnProcessAssembly.Size = new System.Drawing.Size(116, 40);
            this.btnProcessAssembly.TabIndex = 2;
            this.btnProcessAssembly.Text = "Process Assembly";
            this.btnProcessAssembly.UseVisualStyleBackColor = true;
            this.btnProcessAssembly.Click += new System.EventHandler(this.BtnProcessAssembly_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(6, 124);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(325, 229);
            this.rtbLog.TabIndex = 3;
            this.rtbLog.Text = "";
            // 
            // gbAssembly
            // 
            this.gbAssembly.Controls.Add(this.tbAssemblyPath);
            this.gbAssembly.Controls.Add(this.rtbLog);
            this.gbAssembly.Controls.Add(this.btnBrowse);
            this.gbAssembly.Controls.Add(this.btnProcessAssembly);
            this.gbAssembly.Location = new System.Drawing.Point(12, 27);
            this.gbAssembly.Name = "gbAssembly";
            this.gbAssembly.Size = new System.Drawing.Size(342, 369);
            this.gbAssembly.TabIndex = 4;
            this.gbAssembly.TabStop = false;
            this.gbAssembly.Text = "Assembly";
            // 
            // btnBrowseAssemblyDir
            // 
            this.btnBrowseAssemblyDir.Location = new System.Drawing.Point(249, 27);
            this.btnBrowseAssemblyDir.Name = "btnBrowseAssemblyDir";
            this.btnBrowseAssemblyDir.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseAssemblyDir.TabIndex = 6;
            this.btnBrowseAssemblyDir.Text = "Browse";
            this.btnBrowseAssemblyDir.UseVisualStyleBackColor = true;
            this.btnBrowseAssemblyDir.Click += new System.EventHandler(this.btnBrowseAssemblyDir_Click);
            // 
            // tbAssemblyOutputPath
            // 
            this.tbAssemblyOutputPath.Location = new System.Drawing.Point(5, 29);
            this.tbAssemblyOutputPath.Name = "tbAssemblyOutputPath";
            this.tbAssemblyOutputPath.Size = new System.Drawing.Size(229, 20);
            this.tbAssemblyOutputPath.TabIndex = 5;
            this.tbAssemblyOutputPath.Text = "C:\\Users\\maxr1\\Documents\\Bloop";
            // 
            // btnSaveAssembly
            // 
            this.btnSaveAssembly.Enabled = false;
            this.btnSaveAssembly.Location = new System.Drawing.Point(5, 285);
            this.btnSaveAssembly.Name = "btnSaveAssembly";
            this.btnSaveAssembly.Size = new System.Drawing.Size(116, 46);
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
            this.gbDocumentation.Location = new System.Drawing.Point(360, 21);
            this.gbDocumentation.Name = "gbDocumentation";
            this.gbDocumentation.Size = new System.Drawing.Size(425, 375);
            this.gbDocumentation.TabIndex = 5;
            this.gbDocumentation.TabStop = false;
            this.gbDocumentation.Text = "Documentation";
            // 
            // rtbParseLog
            // 
            this.rtbParseLog.Location = new System.Drawing.Point(6, 124);
            this.rtbParseLog.Name = "rtbParseLog";
            this.rtbParseLog.Size = new System.Drawing.Size(365, 229);
            this.rtbParseLog.TabIndex = 3;
            this.rtbParseLog.Text = "";
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(6, 64);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(118, 48);
            this.btnParse.TabIndex = 2;
            this.btnParse.Text = "Parse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // btnBrowseDocumentation
            // 
            this.btnBrowseDocumentation.Location = new System.Drawing.Point(269, 29);
            this.btnBrowseDocumentation.Name = "btnBrowseDocumentation";
            this.btnBrowseDocumentation.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDocumentation.TabIndex = 1;
            this.btnBrowseDocumentation.Text = "Browse";
            this.btnBrowseDocumentation.UseVisualStyleBackColor = true;
            this.btnBrowseDocumentation.Click += new System.EventHandler(this.BtnBrowseDocumentation_Click);
            // 
            // tbDocumentationPath
            // 
            this.tbDocumentationPath.Location = new System.Drawing.Point(6, 31);
            this.tbDocumentationPath.Name = "tbDocumentationPath";
            this.tbDocumentationPath.Size = new System.Drawing.Size(246, 20);
            this.tbDocumentationPath.TabIndex = 0;
            this.tbDocumentationPath.Text = "D:\\PortfolioProject\\XMLGenerator\\PortfolioXMLGenerator\\PortfolioXMLGenerator\\bin\\" +
    "Debug\\PortfolioXMLGenerator.xml";
            // 
            // gbPortfolio
            // 
            this.gbPortfolio.Controls.Add(this.btnParsePortfolio);
            this.gbPortfolio.Controls.Add(this.rtbPortfolioParse);
            this.gbPortfolio.Controls.Add(this.btnBrowsePortfolioXML);
            this.gbPortfolio.Controls.Add(this.tbPortfolioDir);
            this.gbPortfolio.Location = new System.Drawing.Point(791, 27);
            this.gbPortfolio.Name = "gbPortfolio";
            this.gbPortfolio.Size = new System.Drawing.Size(516, 369);
            this.gbPortfolio.TabIndex = 6;
            this.gbPortfolio.TabStop = false;
            this.gbPortfolio.Text = "Portfolio";
            // 
            // btnParsePortfolio
            // 
            this.btnParsePortfolio.Location = new System.Drawing.Point(6, 64);
            this.btnParsePortfolio.Name = "btnParsePortfolio";
            this.btnParsePortfolio.Size = new System.Drawing.Size(118, 48);
            this.btnParsePortfolio.TabIndex = 10;
            this.btnParsePortfolio.Text = "Parse";
            this.btnParsePortfolio.UseVisualStyleBackColor = true;
            this.btnParsePortfolio.Click += new System.EventHandler(this.btnParsePortfolio_Click);
            // 
            // rtbPortfolioParse
            // 
            this.rtbPortfolioParse.Location = new System.Drawing.Point(6, 124);
            this.rtbPortfolioParse.Name = "rtbPortfolioParse";
            this.rtbPortfolioParse.Size = new System.Drawing.Size(365, 229);
            this.rtbPortfolioParse.TabIndex = 9;
            this.rtbPortfolioParse.Text = "";
            // 
            // btnBrowsePortfolioXML
            // 
            this.btnBrowsePortfolioXML.Location = new System.Drawing.Point(353, 29);
            this.btnBrowsePortfolioXML.Name = "btnBrowsePortfolioXML";
            this.btnBrowsePortfolioXML.Size = new System.Drawing.Size(75, 23);
            this.btnBrowsePortfolioXML.TabIndex = 8;
            this.btnBrowsePortfolioXML.Text = "Browse";
            this.btnBrowsePortfolioXML.UseVisualStyleBackColor = true;
            this.btnBrowsePortfolioXML.Click += new System.EventHandler(this.btnBrowsePortfolioXML_Click);
            // 
            // tbPortfolioDir
            // 
            this.tbPortfolioDir.Location = new System.Drawing.Point(6, 29);
            this.tbPortfolioDir.Name = "tbPortfolioDir";
            this.tbPortfolioDir.Size = new System.Drawing.Size(341, 20);
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
            this.gbSaveParsedAssembly.Location = new System.Drawing.Point(18, 401);
            this.gbSaveParsedAssembly.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbSaveParsedAssembly.Name = "gbSaveParsedAssembly";
            this.gbSaveParsedAssembly.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbSaveParsedAssembly.Size = new System.Drawing.Size(336, 336);
            this.gbSaveParsedAssembly.TabIndex = 7;
            this.gbSaveParsedAssembly.TabStop = false;
            this.gbSaveParsedAssembly.Text = "Save Parsed Assembly";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(5, 97);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(319, 183);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // tbAssemblyName
            // 
            this.tbAssemblyName.Location = new System.Drawing.Point(139, 69);
            this.tbAssemblyName.Name = "tbAssemblyName";
            this.tbAssemblyName.Size = new System.Drawing.Size(185, 20);
            this.tbAssemblyName.TabIndex = 8;
            // 
            // lblAssemblyName
            // 
            this.lblAssemblyName.AutoSize = true;
            this.lblAssemblyName.Location = new System.Drawing.Point(2, 72);
            this.lblAssemblyName.Name = "lblAssemblyName";
            this.lblAssemblyName.Size = new System.Drawing.Size(131, 13);
            this.lblAssemblyName.TabIndex = 9;
            this.lblAssemblyName.Text = "Preferred Assembly Name:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 748);
            this.Controls.Add(this.gbSaveParsedAssembly);
            this.Controls.Add(this.gbPortfolio);
            this.Controls.Add(this.gbDocumentation);
            this.Controls.Add(this.gbAssembly);
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
    }
}

