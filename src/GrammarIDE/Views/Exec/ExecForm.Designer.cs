namespace GrammarIDE.Views.Exec
{
    partial class ExecForm
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.RunStackGrid = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.StackGrid = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SymbolsGrid = new System.Windows.Forms.DataGridView();
            this.DebugBtn = new System.Windows.Forms.Button();
            this.BtRun = new System.Windows.Forms.Button();
            this.StepBtn = new System.Windows.Forms.Button();
            this.BuildButton = new System.Windows.Forms.Button();
            this.AsmScript = new System.Windows.Forms.RichTextBox();
            this.ScriptRun = new System.Windows.Forms.RichTextBox();
            this.StopBtn = new System.Windows.Forms.Button();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RunStackGrid)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StackGrid)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SymbolsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.RunStackGrid);
            this.groupBox6.Location = new System.Drawing.Point(622, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(380, 127);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Runtime Stack";
            // 
            // RunStackGrid
            // 
            this.RunStackGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RunStackGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RunStackGrid.Location = new System.Drawing.Point(3, 16);
            this.RunStackGrid.Name = "RunStackGrid";
            this.RunStackGrid.Size = new System.Drawing.Size(374, 108);
            this.RunStackGrid.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.StackGrid);
            this.groupBox5.Location = new System.Drawing.Point(624, 136);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(378, 127);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Eval Stack";
            // 
            // StackGrid
            // 
            this.StackGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StackGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StackGrid.Location = new System.Drawing.Point(3, 16);
            this.StackGrid.Name = "StackGrid";
            this.StackGrid.Size = new System.Drawing.Size(372, 108);
            this.StackGrid.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SymbolsGrid);
            this.groupBox4.Location = new System.Drawing.Point(624, 269);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(378, 173);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Symbol Table";
            // 
            // SymbolsGrid
            // 
            this.SymbolsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SymbolsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SymbolsGrid.Location = new System.Drawing.Point(3, 16);
            this.SymbolsGrid.Name = "SymbolsGrid";
            this.SymbolsGrid.Size = new System.Drawing.Size(372, 154);
            this.SymbolsGrid.TabIndex = 0;
            // 
            // DebugBtn
            // 
            this.DebugBtn.Location = new System.Drawing.Point(443, 0);
            this.DebugBtn.Name = "DebugBtn";
            this.DebugBtn.Size = new System.Drawing.Size(75, 23);
            this.DebugBtn.TabIndex = 10;
            this.DebugBtn.Text = "Debug";
            this.DebugBtn.UseVisualStyleBackColor = true;
            this.DebugBtn.Click += new System.EventHandler(this.DebugBtn_Click);
            // 
            // BtRun
            // 
            this.BtRun.Location = new System.Drawing.Point(368, 0);
            this.BtRun.Name = "BtRun";
            this.BtRun.Size = new System.Drawing.Size(75, 23);
            this.BtRun.TabIndex = 9;
            this.BtRun.Text = "Executar";
            this.BtRun.UseVisualStyleBackColor = true;
            this.BtRun.Click += new System.EventHandler(this.BtRun_Click);
            // 
            // StepBtn
            // 
            this.StepBtn.Enabled = false;
            this.StepBtn.Location = new System.Drawing.Point(525, 0);
            this.StepBtn.Name = "StepBtn";
            this.StepBtn.Size = new System.Drawing.Size(46, 23);
            this.StepBtn.TabIndex = 7;
            this.StepBtn.Text = "Passo";
            this.StepBtn.UseVisualStyleBackColor = true;
            this.StepBtn.Click += new System.EventHandler(this.StepBtn_Click);
            // 
            // BuildButton
            // 
            this.BuildButton.Location = new System.Drawing.Point(293, 0);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(75, 23);
            this.BuildButton.TabIndex = 8;
            this.BuildButton.Text = "Compilar";
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // AsmScript
            // 
            this.AsmScript.BackColor = System.Drawing.Color.Black;
            this.AsmScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AsmScript.ForeColor = System.Drawing.Color.Lime;
            this.AsmScript.Location = new System.Drawing.Point(293, 24);
            this.AsmScript.Name = "AsmScript";
            this.AsmScript.Size = new System.Drawing.Size(325, 418);
            this.AsmScript.TabIndex = 6;
            this.AsmScript.Text = "";
            // 
            // ScriptRun
            // 
            this.ScriptRun.BackColor = System.Drawing.Color.White;
            this.ScriptRun.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptRun.ForeColor = System.Drawing.Color.Black;
            this.ScriptRun.Location = new System.Drawing.Point(3, 3);
            this.ScriptRun.Name = "ScriptRun";
            this.ScriptRun.Size = new System.Drawing.Size(284, 439);
            this.ScriptRun.TabIndex = 5;
            this.ScriptRun.Text = "função principal()\n  variável x\n\n  x = 5\n\n  imprima(fat(x))\nfim\n\nfunção fat(x)\n  " +
    "se x == 0 retorne 1 fim\n  retorne x * fat(x-1)\nfim";
            // 
            // StopBtn
            // 
            this.StopBtn.Enabled = false;
            this.StopBtn.Location = new System.Drawing.Point(572, 0);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(45, 23);
            this.StopBtn.TabIndex = 7;
            this.StopBtn.Text = "Parar";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // ExecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.DebugBtn);
            this.Controls.Add(this.BtRun);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StepBtn);
            this.Controls.Add(this.BuildButton);
            this.Controls.Add(this.AsmScript);
            this.Controls.Add(this.ScriptRun);
            this.Name = "ExecForm";
            this.Size = new System.Drawing.Size(1004, 446);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RunStackGrid)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StackGrid)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SymbolsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView RunStackGrid;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView StackGrid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView SymbolsGrid;
        private System.Windows.Forms.Button DebugBtn;
        private System.Windows.Forms.Button BtRun;
        private System.Windows.Forms.Button StepBtn;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.RichTextBox AsmScript;
        private System.Windows.Forms.RichTextBox ScriptRun;
        private System.Windows.Forms.Button StopBtn;
    }
}
