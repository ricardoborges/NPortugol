using System.Windows.Forms;

namespace GrammarIDE.Views.Exec
{
    public interface IExecView
    {
        RichTextBox Script { get; }

        RichTextBox Asm { get; }

        DataGridView Symbols { get; }

        DataGridView EvalStack { get; }

        DataGridView RunStack { get; }
    }
}