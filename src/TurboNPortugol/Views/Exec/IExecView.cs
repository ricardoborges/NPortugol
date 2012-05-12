using System.Windows.Forms;

namespace TurboNPortugol.Views.Exec
{
    public interface IExecView
    {
        string ProgramName { get; set; }

        RichTextBox Script { get; }

        DataGridView Symbols { get; }

        void ClearOutput();

        string WriteOutput(string text);

        string WriteLine();
    }
}