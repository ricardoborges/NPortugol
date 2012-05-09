using NPortugol.Runtime;

namespace NPortugol.Runtime
{
    public class RuntimeScript
    {
        public RuntimeScript(Runnable runnable)
        {
            this.Runnable = runnable;
        }

        public Runnable Runnable { get; private set; }
        
        public string Name { get; private set; }
    }
}