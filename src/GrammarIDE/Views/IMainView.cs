namespace GrammarIDE.Views
{
    public interface IMainView
    {
        void ClearOutput();

        string WriteOutput(string text);

        string WriteLine();
    }
}