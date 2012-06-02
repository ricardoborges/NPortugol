using NPortugol.Runtime;
using NPortugol.Tests.Runtime.Integration.Instructions;
using NUnit.Framework;
using System.Collections.Generic;

namespace NPortugol.Tests.Runtime.Integration.Instructions
{
    [TestFixture]
    public class DCLTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Declare_Local_Variables()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions());

            var context = new RuntimeContext(runnable);

            context.Execute();

            var valuex = context.GetSymbolValue("main_x_0");
            var valuey = context.GetSymbolValue("main_y_0");
            var valuez = context.GetSymbolValue("main_z_0");
            var valuet = context.GetSymbolValue("main_t_0");

            Assert.AreEqual(null, valuex);
            Assert.AreEqual(null, valuey);
            Assert.AreEqual(null, valuez);
            Assert.AreEqual(null, valuet);

        }

        public override Instruction[] GetInstructions()
        {
            var list =  new List<Instruction>
                            {
                                new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),
                                new Instruction(OpCode.DCL, 1, new Operand(OperandType.Variable, "y")),
                                new Instruction(OpCode.DCL, 2, new Operand(OperandType.Variable, "z")),
                                new Instruction(OpCode.DCL, 3, new Operand(OperandType.Variable, "t")),
                                new Instruction(OpCode.EXIT, 4)
                            };


            return list.ToArray();
        }
    }
}