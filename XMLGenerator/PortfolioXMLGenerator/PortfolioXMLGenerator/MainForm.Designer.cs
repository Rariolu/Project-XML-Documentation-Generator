
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
            this.SuspendLayout();
            // 
            // tbAssemblyPath
            // 
            this.tbAssemblyPath.Location = new System.Drawing.Point(12, 39);
            this.tbAssemblyPath.Name = "tbAssemblyPath";
            this.tbAssemblyPath.Size = new System.Drawing.Size(194, 20);
            this.tbAssemblyPath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(224, 39);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // btnProcessAssembly
            // 
            this.btnProcessAssembly.Location = new System.Drawing.Point(25, 117);
            this.btnProcessAssembly.Name = "btnProcessAssembly";
            this.btnProcessAssembly.Size = new System.Drawing.Size(116, 40);
            this.btnProcessAssembly.TabIndex = 2;
            this.btnProcessAssembly.Text = "Process Assembly";
            this.btnProcessAssembly.UseVisualStyleBackColor = true;
            this.btnProcessAssembly.Click += new System.EventHandler(this.BtnProcessAssembly_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(25, 181);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(325, 229);
            this.rtbLog.TabIndex = 3;
            this.rtbLog.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 450);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.btnProcessAssembly);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbAssemblyPath);
            this.Name = "MainForm";
            this.Text = "PortfolioXMLGenerator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAssemblyPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnProcessAssembly;
        private System.Windows.Forms.RichTextBox rtbLog;
    }
}

