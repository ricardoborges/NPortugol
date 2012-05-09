using System;
using System.Windows.Forms;
using GrammarIDE.Presenters;

namespace GrammarIDE.Views.Ast
{
    public partial class AstForm : UserControl, IAstView
    {
        public AstForm()
        {
            InitializeComponent();
        }

        public IAstPresenter AstPresenter { get; set; }

        private void ParseBtn_Click(object sender, EventArgs e)
        {
            try
            {
                AstPresenter.Parse(ScriptText.Text);

                if (string.IsNullOrEmpty(AstPresenter.Config.DotPath))
                    throw new Exception("Caminho para dot.exe não encontrado. Esqueceu de configurar?");

                PopupBtn.Enabled = true;
            }
            catch (Exception ex)
            {
               AstPresenter.MainPresenter.Error(ex);
            }
        }

        private void PopupBtn_Click(object sender, EventArgs e)
        {
            new GraphForm().Show();
        }

        public int FontSize
        {
            get { return (int) numericUpDown1.Value; }
            set { numericUpDown1.Value = value; }
        }

        public void LoadAstImage(string image)
        {
            ASTPicture.Load(image);
        }

        public void Init()
        {
            if (!string.IsNullOrEmpty(AstPresenter.Config.Script))
                ScriptText.Text = AstPresenter.Config.Script;
        }
    }
}
