using System.IO;
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

            tabControl1.SelectedTab.Text = ((ExecForm) presenter.ExecView).ProgramName;
        }

        public void CloseTab(string name)
        {
            tabControl1.TabPages.RemoveByKey(name);
        }

        private void tutoriaisToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //new AboutForm().ShowDialog();
        }

        private void salvarToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var view = tabControl1.SelectedTab.Controls[0] as IExecView;

            if (view == null)
            {
                MessageBox.Show("Nenhum programa selecionado.");
                return;
            }

            var text = view.Script.Text;
            var path = view.FilePath;
            var name = view.ProgramName;

            if (string.IsNullOrEmpty(path))
            {
                var dialog = new FolderBrowserDialog();

                if (DialogResult.OK == dialog.ShowDialog())
                {
                    path = dialog.SelectedPath;
                }

                path = path + "\\" + name;
            }

            using (var writer = new StreamWriter(path))
            {
                writer.Write(text);
            }
        }

        private void novoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void fecharToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void novoToolStripMenuItem_Click_1(object sender, System.EventArgs e)
        {
            mainPresenter.CreateWindow(string.Empty);
        }
    }
}
