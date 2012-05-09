using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Instructions.Arithmetic
{
    [TestFixture]
    public class DIVTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Arithmetic_DIV()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions());

            var context = new RuntimeContext(new RuntimeScript(runnable));

            context.Execute();

            Assert.AreEqual(1, context.GetSymbolValue("main_x_0"));
            Assert.AreEqual(5, context.GetSymbolValue("main_y_0"));
            Assert.AreEqual(3.33333325f, context.GetSymbolValue("main_z_0"));
        }

        public override Instruction[] GetInstructions()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "x"),new Operand(OperandType.Literal, 1)), 
                               new Instruction(OpCode.DIV, 2, new Operand(OperandType.Variable, "x"),new Operand(OperandType.Literal, 1)), 

                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "y")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "y"),new Operand(OperandType.Literal, 10.0f)), 
                               new Instruction(OpCode.DIV, 2, new Operand(OperandType.Variable, "y"),new Operand(OperandType.Literal, 2.0f)), 

                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "z")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "z"),new Operand(OperandType.Literal, 10)), 
                               new Instruction(OpCode.DIV, 2, new Operand(OperandType.Variable, "z"),new Operand(OperandType.Literal, 3)), 

                               new Instruction(OpCode.EXIT, 3, new Operand(OperandType.Null, null))
                           };

            return list.ToArray();
        }
    }
}