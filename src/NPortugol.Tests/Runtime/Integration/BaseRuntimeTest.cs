using System.Collections.Generic;
using NPortugol.Runtime;

namespace NPortugol.Tests.Runtime.Integration
{
    public class BaseRuntimeTest
    {
        public Runnable GetRunnableWithMainFor(params Instruction[] instructions)
        {
            var stream = new InstrucStream(instructions);

            var function = new Function(Function.MainName, 0);

            var ftable = new FunctionTable { { Function.MainName, function } };

            return new Runnable(stream, ftable);
        }

        public Runnable GetRunnableFor(string functionName, params Instruction[] instructions)
        {
            var stream = new InstrucStream(instructions);

            var function = new Function(functionName, 0);

            var ftable = new FunctionTable { { functionName, function } };

            return new Runnable(stream, ftable);
        }

        public Instruction[] GetDeclareXInstructions()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),
                               new Instruction(OpCode.EXIT, 1)
                           };

            return list.ToArray();
        }
    }
}