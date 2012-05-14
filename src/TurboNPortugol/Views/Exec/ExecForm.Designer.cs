namespace TurboNPortugol.Views.Exec
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SymbolsGrid = new System.Windows.Forms.DataGridView();
            this.DebugBtn = new System.Windows.Forms.Button();
            this.BtRun = new System.Windows.Forms.Button();
            this.StepBtn = new System.Windows.Forms.Button();
            this.BuildButton = new System.Windows.Forms.Button();
            this.ScriptRun = new System.Windows.Forms.RichTextBox();
            this.StopBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Output = new System.Windows.Forms.RichTextBox();
            this.FecharBtn = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SymbolsGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SymbolsGrid);
            this.groupBox4.Location = new System.Drawing.Point(623, 497);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(378, 126);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tabela de Variáveis";
            // 
            // SymbolsGrid
            // 
            this.SymbolsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SymbolsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SymbolsGrid.Location = new System.Drawing.Point(3, 16);
            this.SymbolsGrid.Name = "SymbolsGrid";
            this.SymbolsGrid.Size = new System.Drawing.Size(372, 107);
            this.SymbolsGrid.TabIndex = 0;
            // 
            // DebugBtn
            // 
            this.DebugBtn.Location = new System.Drawing.Point(156, 5);
            this.DebugBtn.Name = "DebugBtn";
            this.DebugBtn.Size = new System.Drawing.Size(75, 23);
            this.DebugBtn.TabIndex = 10;
            this.DebugBtn.Text = "DEBUG";
            this.DebugBtn.UseVisualStyleBackColor = true;
            this.DebugBtn.Click += new System.EventHandler(this.DebugBtn_Click);
            // 
            // BtRun
            // 
            this.BtRun.Location = new System.Drawing.Point(81, 5);
            this.BtRun.Name = "BtRun";
            this.BtRun.Size = new System.Drawing.Size(75, 23);
            this.BtRun.TabIndex = 9;
            this.BtRun.Text = "RUN";
            this.BtRun.UseVisualStyleBackColor = true;
            this.BtRun.Click += new System.EventHandler(this.BtRun_Click);
            // 
            // StepBtn
            // 
            this.StepBtn.Enabled = false;
            this.StepBtn.Location = new System.Drawing.Point(238, 5);
            this.StepBtn.Name = "StepBtn";
            this.StepBtn.Size = new System.Drawing.Size(46, 23);
            this.StepBtn.TabIndex = 7;
            this.StepBtn.Text = "STEP";
            this.StepBtn.UseVisualStyleBackColor = true;
            this.StepBtn.Click += new System.EventHandler(this.StepBtn_Click);
            // 
            // BuildButton
            // 
            this.BuildButton.Location = new System.Drawing.Point(6, 5);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(75, 23);
            this.BuildButton.TabIndex = 8;
            this.BuildButton.Text = "BUILD";
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // ScriptRun
            // 
            this.ScriptRun.BackColor = System.Drawing.Color.White;
            this.ScriptRun.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptRun.ForeColor = System.Drawing.Color.Black;
            this.ScriptRun.Location = new System.Drawing.Point(3, 34);
            this.ScriptRun.Name = "ScriptRun";
            this.ScriptRun.Size = new System.Drawing.Size(995, 457);
            this.ScriptRun.TabIndex = 5;
            this.ScriptRun.Text = "";
            // 
            // StopBtn
            // 
            this.StopBtn.Enabled = false;
            this.StopBtn.Location = new System.Drawing.Point(285, 5);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(45, 23);
            this.StopBtn.TabIndex = 7;
            this.StopBtn.Text = "STOP";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Output);
            this.groupBox2.Location = new System.Drawing.Point(3, 497);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(614, 129);
            this.groupBox2.TabIndex = 14;
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
            this.Output.Size = new System.Drawing.Size(608, 110);
            this.Output.TabIndex = 0;
            this.Output.Text = "";
            // 
            // FecharBtn
            // 
            this.FecharBtn.Location = new System.Drawing.Point(923, 5);
            this.FecharBtn.Name = "FecharBtn";
            this.FecharBtn.Size = new System.Drawing.Size(75, 23);
            this.FecharBtn.TabIndex = 15;
            this.FecharBtn.Text = "FECHAR";
            this.FecharBtn.UseVisualStyleBackColor = true;
            this.FecharBtn.Click += new System.EventHandler(this.FecharBtn_Click);
            // 
            // ExecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FecharBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.DebugBtn);
            this.Controls.Add(this.BtRun);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StepBtn);
            this.Controls.Add(this.BuildButton);
            this.Controls.Add(this.ScriptRun);
            this.Name = "ExecForm";
            this.Size = new System.Drawing.Size(1004, 629);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SymbolsGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView SymbolsGrid;
        private System.Windows.Forms.Button DebugBtn;
        private System.Windows.Forms.Button BtRun;
        private System.Windows.Forms.Button StepBtn;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.RichTextBox ScriptRun;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox Output;
        private System.Windows.Forms.Button FecharBtn;
    }
}
