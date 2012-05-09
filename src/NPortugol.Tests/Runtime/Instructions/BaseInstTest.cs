using NPortugol.Runtime;

namespace NPortugol.Tests.Runtime.Instructions
{
    public abstract class BaseInstTest
    {
        public string SymbolId(string id)
        {
            return SymbolTable.BuildSymbolId(Function.MainName, id, 0);
        }

        public virtual Runnable GetRunnable()
        {
            return new Runnable(new InstrucStream(), new FunctionTable());
        }

        public FunctionCall GetMainFunction()
        {
            return new FunctionCall(new Function(Function.MainName,0), 0);
        }
    }
}