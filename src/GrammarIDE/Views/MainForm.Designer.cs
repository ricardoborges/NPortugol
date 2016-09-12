using GrammarIDE.Views.Ast;

namespace GrammarIDE.Views
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.RunTab = new System.Windows.Forms.TabPage();
            this.execForm1 = new GrammarIDE.Views.Exec.ExecForm();
            this.ScriptTab = new System.Windows.Forms.TabPage();
            this.astForm1 = new GrammarIDE.Views.Ast.AstForm();
            this.ConfigTab = new System.Windows.Forms.TabPage();
            this.configForm1 = new GrammarIDE.Views.Configuration.ConfigForm();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Output = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ajudaGridView = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.RunTab.SuspendLayout();
            this.ScriptTab.SuspendLayout();
            this.ConfigTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ajudaGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.RunTab);
            this.tabControl1.Controls.Add(this.ScriptTab);
            this.tabControl1.Controls.Add(this.ConfigTab);
            this.tabControl1.Location = new System.Drawing.Point(9, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1025, 472);
            this.tabControl1.TabIndex = 1;
            // 
            // RunTab
            // 
            this.RunTab.Controls.Add(this.execForm1);
            this.RunTab.Location = new System.Drawing.Point(4, 22);
            this.RunTab.Name = "RunTab";
            this.RunTab.Padding = new System.Windows.Forms.Padding(3);
            this.RunTab.Size = new System.Drawing.Size(1017, 446);
            this.RunTab.TabIndex = 3;
            this.RunTab.Text = "Executar";
            this.RunTab.UseVisualStyleBackColor = true;
            // 
            // execForm1
            // 
            this.execForm1.ExecPresenter = null;
            this.execForm1.Location = new System.Drawing.Point(0, 0);
            this.execForm1.Name = "execForm1";
            this.execForm1.Size = new System.Drawing.Size(1004, 446);
            this.execForm1.TabIndex = 0;
            // 
            // ScriptTab
            // 
            this.ScriptTab.Controls.Add(this.astForm1);
            this.ScriptTab.Location = new System.Drawing.Point(4, 22);
            this.ScriptTab.Name = "ScriptTab";
            this.ScriptTab.Padding = new System.Windows.Forms.Padding(3);
            this.ScriptTab.Size = new System.Drawing.Size(1017, 446);
            this.ScriptTab.TabIndex = 0;
            this.ScriptTab.Text = "Árvore";
            this.ScriptTab.UseVisualStyleBackColor = true;
            // 
            // astForm1
            // 
            this.astForm1.AstPresenter = null;
            this.astForm1.FontSize = 12;
            this.astForm1.Location = new System.Drawing.Point(0, 0);
            this.astForm1.Name = "astForm1";
            this.astForm1.Size = new System.Drawing.Size(1014, 445);
            this.astForm1.TabIndex = 0;
            // 
            // ConfigTab
            // 
            this.ConfigTab.Controls.Add(this.configForm1);
            this.ConfigTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigTab.Name = "ConfigTab";
            this.ConfigTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConfigTab.Size = new System.Drawing.Size(1017, 446);
            this.ConfigTab.TabIndex = 1;
            this.ConfigTab.Text = "Configuração";
            this.ConfigTab.UseVisualStyleBackColor = true;
            // 
            // configForm1
            // 
            this.configForm1.ConfigPresenter = null;
            this.configForm1.Location = new System.Drawing.Point(6, 6);
            this.configForm1.Name = "configForm1";
            this.configForm1.Size = new System.Drawing.Size(411, 98);
            this.configForm1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Output);
            this.groupBox2.Location = new System.Drawing.Point(422, 490);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 129);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Saída";
            // 
            // Output
            // 
            this.Output.BackColor = System.Drawing.Color.LightGray;
            this.Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Output.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Output.ForeColor = System.Drawing.Color.Black;
            this.Output.Location = new System.Drawing.Point(3, 16);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(602, 110);
            this.Output.TabIndex = 0;
            this.Output.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ajudaGridView);
            this.groupBox1.Location = new System.Drawing.Point(13, 490);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 126);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ajuda - Biblioteca de funções";
            // 
            // ajudaGridView
            // 
            this.ajudaGridView.AllowUserToAddRows = false;
            this.ajudaGridView.AllowUserToDeleteRows = false;
            this.ajudaGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ajudaGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ajudaGridView.Location = new System.Drawing.Point(3, 16);
            this.ajudaGridView.Name = "ajudaGridView";
            this.ajudaGridView.ReadOnly = true;
            this.ajudaGridView.Size = new System.Drawing.Size(397, 107);
            this.ajudaGridView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 621);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "NPortugol IDE";
            this.tabControl1.ResumeLayout(false);
            this.RunTab.ResumeLayout(false);
            this.ScriptTab.ResumeLayout(false);
            this.ConfigTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ajudaGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage RunTab;
        private System.Windows.Forms.TabPage ConfigTab;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox Output;
        private System.Windows.Forms.TabPage ScriptTab;
        private AstForm astForm1;
        private Configuration.ConfigForm configForm1;
        private Exec.ExecForm execForm1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView ajudaGridView;
    }
}