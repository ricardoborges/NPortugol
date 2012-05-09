using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Specs
{
    [TestFixture]
    public class InstructionDesign
    {
        [Test]
        public void SimpleInstruction()
        {
            var op0 = new Operand(OperandType.Variable, "x", 0);
            var op1 = new Operand(OperandType.Literal, 1, 0);

            var inst = new Instruction(OpCode.MOV, 1, op0, op1);
            var inst2 = new Instruction(OpCode.MOV, 2, op0, op1);

            Assert.AreEqual("00001:", inst.IndexName);
            Assert.AreEqual("00002:", inst2.IndexName);
        }
    }
}