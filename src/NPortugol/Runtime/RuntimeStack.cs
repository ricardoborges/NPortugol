using System.Collections.Generic;

namespace NPortugol.Runtime
{
    public class RuntimeStack : Stack<IStackItem>
    {
        private int id;

        public void Push(Function function)
        {
            var @new = new FunctionCall(function, id++);

            Push(@new);
        }
    }
}