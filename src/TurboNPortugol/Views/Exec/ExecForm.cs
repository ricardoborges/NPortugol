﻿using System;
using System.IO;
using System.Windows.Forms;
using TurboNPortugol.Presenters;

namespace TurboNPortugol.Views.Exec
{
    public partial class ExecForm : UserControl, IExecView
    {
        private string filePath;
        private string name;

        public ExecForm()
        {
            InitializeComponent();
        }

        public ExecForm(string filePath, string name)
        {
            this.filePath = filePath;
            this.name = name;

            InitializeComponent();
            Init(filePath);
        }

        private void Init(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                CreateTemplate();
                return;
            }

            using(var reader = new StreamReader(file))
            {
                var text = reader.ReadToEnd();

                Script.Text = text;
            }
        }

        private void CreateTemplate()
        {
            Script.Text = "funcao principal()\r\n \r\nfim";
        }

        public IExecPresenter ExecPresenter { get; set; }

        public DataGridView Symbols { get { return SymbolsGrid; } }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            try
            {
                ExecPresenter.Build(true);

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

        public string ProgramName
        {
            get { return name; }
            set { name = value ; }
        }

        public RichTextBox Script
        {
            get { return ScriptRun; }
        }
        
        private void StopBtn_Click(object sender, EventArgs e)
        {
            ExecPresenter.Stop();

            StepBtn.Enabled = false;
            StopBtn.Enabled = false;

            BuildButton.Enabled = true;
            BtRun.Enabled = true;
        }

        public void ClearOutput()
        {
            Output.Clear();
        }

        public string WriteOutput(string text)
        {
             return Output.Text += text + "\r\n";
        }

        public string WriteLine()
        {
            return Output.Text += "-------------------------------------------------------------------------\r\n";
        }

        private void FecharBtn_Click(object sender, EventArgs e)
        {
            ExecPresenter.MainPresenter.CloseWindow(ProgramName);
        }
    }
}
