using System;
using System.Windows.Forms;
using GrammarIDE.Core;
using GrammarIDE.Presenters;

namespace GrammarIDE.Views
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();

            var mainPresenter = new MainPresenter {MainView = this};

            var configRepo = new ConfigRepo();

            astForm1.AstPresenter = new AstPresenter(configRepo, mainPresenter) { AstView = astForm1 };
            astForm1.Init();

            configForm1.ConfigPresenter = new ConfigPresenter(configRepo, mainPresenter);
            configForm1.Init();

            execForm1.ExecPresenter = new ExecPresenter(configRepo, mainPresenter) {ExecView = execForm1};
            execForm1.Init();
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
            return Output.Text += "-------------------------------------------------------------------------------------------------------------\r\n";
        }
    }
}
