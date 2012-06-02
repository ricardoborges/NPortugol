using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Instructions.Arithmetic
{
    [TestFixture]
    public class INCTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Arithmetic_INC()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions());

            var context = new RuntimeContext(runnable);

            context.Execute();

            Assert.AreEqual(5, context.GetSymbolValue("main_x_0"));
            Assert.AreEqual(9, context.GetSymbolValue("main_y_0"));
            Assert.AreEqual(10, context.GetSymbolValue("main_z_0"));
            Assert.AreEqual(17, context.GetSymbolValue("main_t_0"));
        }

        public override Instruction[] GetInstructions()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "x"), new Operand(OperandType.Literal, 4)), 
                               new Instruction(OpCode.INC, 2, new Operand(OperandType.Variable, "x")), 
				
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "y")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "y"), new Operand(OperandType.Literal, 8)), 
                               new Instruction(OpCode.INC, 2, new Operand(OperandType.Variable, "y")), 
				
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "z")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "z"), new Operand(OperandType.Literal, 9)), 
                               new Instruction(OpCode.INC, 2, new Operand(OperandType.Variable, "z")), 
				
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "t")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "t"), new Operand(OperandType.Literal, 16)), 
                               new Instruction(OpCode.INC, 2, new Operand(OperandType.Variable, "t")), 
				
                               new Instruction(OpCode.EXIT, 3, new Operand(OperandType.Null, null))
                           };

            return list.ToArray();
        }
    }
}