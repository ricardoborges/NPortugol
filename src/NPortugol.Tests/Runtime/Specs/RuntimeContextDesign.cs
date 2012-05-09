using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Specs
{
    [TestFixture]
    public class RuntimeContextDesign
    {
        [Test]
        public void CreateContext()
        {
            var context = new RuntimeContext(CreateRuntimeScript());
        }

        public RuntimeScript CreateRuntimeScript()
        {
            return new RuntimeScript(CreateRunnable());
        }

        public Runnable CreateRunnable()
        {
            var op0 = new Operand(OperandType.Literal, 1, 0);
            var op1 = new Operand(OperandType.Literal, 1, 0);

            var inst1 = new Instruction(OpCode.ADD, 1, op0, op1);
            var inst2 = new Instruction(OpCode.ADD, 2, op0, op1);

            var stream = new InstrucStream(inst1, inst2);

            var func = new Function(Function.MainName, 0);
            
            var ftable = new FunctionTable();

            ftable[Function.MainName] = func;

            return new Runnable(stream, ftable);
        }
    }
}