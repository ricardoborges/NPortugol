namespace GrammarIDE.Views
{
    partial class GraphForm
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
            this.ASTPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ASTPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // ASTPicture
            // 
            this.ASTPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ASTPicture.Location = new System.Drawing.Point(0, 0);
            this.ASTPicture.Name = "ASTPicture";
            this.ASTPicture.Size = new System.Drawing.Size(926, 696);
            this.ASTPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ASTPicture.TabIndex = 1;
            this.ASTPicture.TabStop = false;
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 696);
            this.Controls.Add(this.ASTPicture);
            this.Name = "GraphForm";
            this.Text = "AST";
            ((System.ComponentModel.ISupportInitialize)(this.ASTPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ASTPicture;
    }
}