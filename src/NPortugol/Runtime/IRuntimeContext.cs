using System.Collections.Generic;
using NPortugol.Runtime.Interop;

namespace NPortugol.Runtime
{
    public interface IRuntimeContext
    {
        Instruction CurrentInst { get; }

        FunctionCall CurrentFunction { get; set; }

        Runnable Runnable { get; }

        IHostContainer HostContainer { get; set;  }

        bool Debug { get; set; }

        bool Debugging { get; set; }

        bool EnableGC { get; set; }

        bool Completed { get; set; }

        void Execute();
        
        object Execute(string function, params object[] parameters);
        
        object GetSymbolValue(string id);
        
        void InitWithMain();
        
        void Init(string function);
        
        void LoadDebugInfo(Dictionary<int, int> info);
    }
}