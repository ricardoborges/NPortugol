using System.Collections.Generic;

namespace NPortugol.Runtime
{
    public interface ICompiler
    {
        IList<string> Compile(string script);

        IList<string> CompileFile(string file);
    }
}