namespace NPortugol.Runtime
{
    public interface ICompiler
    {
        Bytecode Compile(string script);

        Bytecode CompileFile(string file);
    }
}