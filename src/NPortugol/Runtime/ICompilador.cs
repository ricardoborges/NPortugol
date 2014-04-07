namespace NPortugol.Runtime
{
    public interface ICompilador
    {
        Bytecode Compilar(string codigo);

        Bytecode CompilarArquivo(string arquivo);
    }
}