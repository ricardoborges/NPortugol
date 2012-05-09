using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Instructions
{
    [TestFixture]
    public class PUSHPOPTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Push_Literal_Value_Into_Stack()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions());

            var script = new RuntimeScript(runnable);

            var context = new RuntimeContext(script);

            context.Execute();

            Assert.AreEqual(1, context.GetSymbolValue("main_x_0"));
        }

        public override Instruction[] GetInstructions()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),
                               new Instruction(OpCode.PUSH, 1, new Operand(OperandType.Literal, 1)),
                               new Instruction(OpCode.POP, 2, new Operand(OperandType.Variable, "x")),
                               new Instruction(OpCode.EXIT, 3)
                           };

            return list.ToArray();
        }
    }
}