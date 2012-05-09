using System;
using System.Windows.Forms;
using GrammarIDE.Presenters;

namespace GrammarIDE.Views.Exec
{
    public partial class ExecForm : UserControl, IExecView
    {
        public ExecForm()
        {
            InitializeComponent();
        }

        public void Init()
        {
            if (!string.IsNullOrEmpty(ExecPresenter.Config.Script))
                ScriptRun.Text = ExecPresenter.Config.Script;
        }

        public IExecPresenter ExecPresenter { get; set; }

        public DataGridView Symbols { get { return SymbolsGrid; } }

        public DataGridView EvalStack { get { return StackGrid; } }

        public DataGridView RunStack { get { return RunStackGrid; } }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            try
            {
                ExecPresenter.Build();

                BtRun.Enabled = true;
            }
            catch (Exception ex)
            {
               ExecPresenter.MainPresenter.Error(ex);
            }
        }

        private void BtRun_Click(object sender, EventArgs e)
        {
            try
            {
                ExecPresenter.Run();
            }
            catch (Exception ex)
            {
                ExecPresenter.MainPresenter.Error(ex);
            }
        }

        private void DebugBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ExecPresenter.Debug();

                StepBtn.Enabled = true;
                StopBtn.Enabled = true;
                
                BuildButton.Enabled = false;
                BtRun.Enabled = false;
            }
            catch (Exception ex)
            {
                StepBtn.Enabled = false;
                StopBtn.Enabled = false;
                
                BuildButton.Enabled = true;
                BtRun.Enabled = true;
                ExecPresenter.MainPresenter.Error(ex);
            }
        }

        private void StepBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ExecPresenter.Step();

                if (!ExecPresenter.Debugging)
                    StepBtn.Enabled = false;
            }
            catch (Exception ex)
            {
                StepBtn.Enabled = false;
                ExecPresenter.MainPresenter.Error(ex);
            }
        }

        public RichTextBox Script
        {
            get { return ScriptRun; }
        }

        public RichTextBox Asm
        {
            get { return AsmScript; }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            ExecPresenter.Stop();

            StepBtn.Enabled = false;
            StopBtn.Enabled = false;

            BuildButton.Enabled = true;
            BtRun.Enabled = true;
        }
    }
}
