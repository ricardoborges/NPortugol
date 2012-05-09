using System.Windows.Forms;

namespace GrammarIDE.Views
{
    public partial class GraphForm : Form
    {
        public GraphForm()
        {
            InitializeComponent();

            ASTPicture.Load("graph.gif");
        }
    }
}
