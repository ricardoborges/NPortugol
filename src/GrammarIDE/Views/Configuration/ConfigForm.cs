using System;
using System.Windows.Forms;
using GrammarIDE.Presenters;

namespace GrammarIDE.Views.Configuration
{
    public partial class ConfigForm : UserControl, IConfigView
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        public void Init()
        {
            DotPath.Text = ConfigPresenter.GetConfig().DotPath;
        }

        public IConfigPresenter ConfigPresenter { get; set; }

        private void DirBtn_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DotPath.Text = dialog.SelectedPath;
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ConfigPresenter.Save(DotPath.Text);
        }
    }
}
