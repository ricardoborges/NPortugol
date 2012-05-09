namespace GrammarIDE.Views.Configuration
{
    partial class ConfigForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DirBtn = new System.Windows.Forms.Button();
            this.DotPath = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(3, 71);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 7;
            this.SaveBtn.Text = "Salvar";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DirBtn);
            this.groupBox1.Controls.Add(this.DotPath);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 62);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DOT Path";
            // 
            // DirBtn
            // 
            this.DirBtn.Location = new System.Drawing.Point(358, 24);
            this.DirBtn.Name = "DirBtn";
            this.DirBtn.Size = new System.Drawing.Size(39, 23);
            this.DirBtn.TabIndex = 1;
            this.DirBtn.Text = "...";
            this.DirBtn.UseVisualStyleBackColor = true;
            this.DirBtn.Click += new System.EventHandler(this.DirBtn_Click);
            // 
            // DotPath
            // 
            this.DotPath.Location = new System.Drawing.Point(7, 26);
            this.DotPath.Name = "DotPath";
            this.DotPath.Size = new System.Drawing.Size(345, 20);
            this.DotPath.TabIndex = 0;
            this.DotPath.Text = "C:\\Program Files\\Graphviz 2.28\\bin";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConfigForm";
            this.Size = new System.Drawing.Size(411, 98);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DirBtn;
        private System.Windows.Forms.TextBox DotPath;
    }
}
