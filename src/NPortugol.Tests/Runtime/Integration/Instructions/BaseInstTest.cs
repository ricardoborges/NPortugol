using NPortugol.Runtime;

namespace NPortugol.Tests.Runtime.Integration.Instructions
{
    public abstract class BaseInstTest: BaseRuntimeTest
    {
        public abstract Instruction[] GetInstructions();

        public RuntimeContext GetContextWithMain(Instruction[] instructions)
        {
            var runnable = GetRunnableWithMainFor(instructions);

            var script = new RuntimeScript(runnable);

            return new RuntimeContext(script);            
        }
    }
}