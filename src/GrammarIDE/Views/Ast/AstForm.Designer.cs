namespace GrammarIDE.Views.Ast
{
    partial class AstForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ASTPicture = new System.Windows.Forms.PictureBox();
            this.PopupBtn = new System.Windows.Forms.Button();
            this.ParseBtn = new System.Windows.Forms.Button();
            this.ScriptText = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ASTPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(532, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Fonte";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(572, 6);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(53, 20);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ASTPicture);
            this.groupBox3.Location = new System.Drawing.Point(360, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(651, 410);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AST";
            // 
            // ASTPicture
            // 
            this.ASTPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASTPicture.Location = new System.Drawing.Point(3, 16);
            this.ASTPicture.Name = "ASTPicture";
            this.ASTPicture.Size = new System.Drawing.Size(645, 391);
            this.ASTPicture.TabIndex = 0;
            this.ASTPicture.TabStop = false;
            // 
            // PopupBtn
            // 
            this.PopupBtn.Enabled = false;
            this.PopupBtn.Location = new System.Drawing.Point(441, 3);
            this.PopupBtn.Name = "PopupBtn";
            this.PopupBtn.Size = new System.Drawing.Size(75, 23);
            this.PopupBtn.TabIndex = 9;
            this.PopupBtn.Text = "Expandir";
            this.PopupBtn.UseVisualStyleBackColor = true;
            this.PopupBtn.Click += new System.EventHandler(this.PopupBtn_Click);
            // 
            // ParseBtn
            // 
            this.ParseBtn.Location = new System.Drawing.Point(360, 3);
            this.ParseBtn.Name = "ParseBtn";
            this.ParseBtn.Size = new System.Drawing.Size(75, 23);
            this.ParseBtn.TabIndex = 10;
            this.ParseBtn.Text = "PARSE";
            this.ParseBtn.UseVisualStyleBackColor = true;
            this.ParseBtn.Click += new System.EventHandler(this.ParseBtn_Click);
            // 
            // ScriptText
            // 
            this.ScriptText.BackColor = System.Drawing.Color.White;
            this.ScriptText.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptText.ForeColor = System.Drawing.Color.Black;
            this.ScriptText.Location = new System.Drawing.Point(3, 3);
            this.ScriptText.Name = "ScriptText";
            this.ScriptText.Size = new System.Drawing.Size(351, 439);
            this.ScriptText.TabIndex = 8;
            this.ScriptText.Text = "funcao principal()\n  imprima(fat(5))\nfim\n\nfuncao fat(x)\n  se x == 0 retorne 1 fim" +
    "\n  retorne x * fat(x-1)\nfim";
            // 
            // AstForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.PopupBtn);
            this.Controls.Add(this.ParseBtn);
            this.Controls.Add(this.ScriptText);
            this.Name = "AstForm";
            this.Size = new System.Drawing.Size(1014, 445);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ASTPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox ASTPicture;
        private System.Windows.Forms.Button PopupBtn;
        private System.Windows.Forms.Button ParseBtn;
        private System.Windows.Forms.RichTextBox ScriptText;
    }
}
