using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Specs
{
    [TestFixture]
    public class RunnableDesign
    {
        [Test]
        public void CreateScript()
        {
            var op0 = new Operand(OperandType.Literal, 1, 0);
            var op1 = new Operand(OperandType.Literal, 1, 0);

            var stream = new InstrucStream(
                new Instruction(OpCode.ADD, 1, op0, op1),
                new Instruction(OpCode.MOV, 2, op0, op1)
                );

            var runtimeScript = new Runnable(stream, new FunctionTable());
        }
    }
}