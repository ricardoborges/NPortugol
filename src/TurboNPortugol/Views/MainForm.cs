using System.Windows.Forms;
using TurboNPortugol.Presenters;
using TurboNPortugol.Views.Exec;

namespace TurboNPortugol.Views
{
    public partial class MainForm : Form, IMainView
    {
        private IMainPresenter mainPresenter;

        public MainForm()
        {
            InitializeComponent();

            mainPresenter = new MainPresenter {MainView = this};

            mainPresenter.CreateWindow(string.Empty);

            //execForm1.ExecPresenter = new ExecPresenter(mainPresenter) {ExecView = execForm1};
        }

        private void abrirToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (DialogResult.OK == dialog.ShowDialog())
            {
                var filepath = dialog.FileName;

                mainPresenter.CreateWindow(filepath);
            }
        }

        public void AddTab(ExecPresenter presenter)
        {
            tabControl1.TabPages.Add(presenter.ExecView.ProgramName, presenter.ExecView.ProgramName);
            
            tabControl1.TabPages[tabControl1.TabCount-1].Controls.Add((ExecForm)presenter.ExecView);

            tabControl1.SelectTab(tabControl1.TabCount - 1);
        }

        public void CloseTab(string name)
        {
            tabControl1.TabPages.RemoveByKey(name);
        }
    }
}
