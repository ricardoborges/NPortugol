using System.Collections.Generic;

namespace NPortugol.Runtime.Asm
{
    public interface IRunnableBuilder
    {
        bool DebugInfo { get; set; }

        void Load(string script);

        void Load(IEnumerable<string> lines);

        Runnable Build();
    }
}