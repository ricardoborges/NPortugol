namespace GrammarIDE.Views.Ast
{
    public interface IAstView
    {
        int FontSize { get; set; }
        
        void LoadAstImage(string image);
    }
}