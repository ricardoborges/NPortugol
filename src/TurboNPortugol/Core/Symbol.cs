namespace GrammarIDE.Core
{
    public class Symbol
    {
        public Symbol(string nome, object value)
        {
            Nome = nome;
            Value = value;
        }

        public string Nome { get; set; }

        public object Value { get; set; }
    }
}