using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Specs
{
    [TestFixture]
    public class InstrucStreamDesign
    {
        [Test]
        public void CreateStream()
        {
            var op0 = new Operand(OperandType.Literal, 1, 0);
            var op1 = new Operand(OperandType.Literal, 1, 0);

            var runtimeStream = new InstrucStream(
                new Instruction(OpCode.ADD, 1, op0, op1),
                new Instruction(OpCode.MOV, 2, op0, op1)
                );

            Assert.AreEqual(2, runtimeStream.Size);
        }
    }
}