using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Instructions.Arithmetic
{
    [TestFixture]
    public class POWTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Arithmetic_POW()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions());

            var context = new RuntimeContext(new RuntimeScript(runnable));

            context.Execute();

            Assert.AreEqual(4, context.GetSymbolValue("main_x_0"));
            Assert.AreEqual(8, context.GetSymbolValue("main_y_0"));
            Assert.AreEqual(9, context.GetSymbolValue("main_z_0"));
            Assert.AreEqual(16, context.GetSymbolValue("main_t_0"));
        }

        public override Instruction[] GetInstructions()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "x"),new Operand(OperandType.Literal, 2)), 
                               new Instruction(OpCode.POW, 2, new Operand(OperandType.Variable, "x"),new Operand(OperandType.Literal, 2)), 
				
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "y")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "y"),new Operand(OperandType.Literal, 2)), 
                               new Instruction(OpCode.POW, 2, new Operand(OperandType.Variable, "y"),new Operand(OperandType.Literal, 3)), 
				
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "z")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "z"),new Operand(OperandType.Literal, 3)), 
                               new Instruction(OpCode.POW, 2, new Operand(OperandType.Variable, "z"),new Operand(OperandType.Literal, 2)), 
				
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "t")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "t"),new Operand(OperandType.Literal, 2)), 
                               new Instruction(OpCode.POW, 2, new Operand(OperandType.Variable, "t"),new Operand(OperandType.Variable, "x")), 
				
                               new Instruction(OpCode.EXIT, 3, new Operand(OperandType.Null, null))
                           };

            return list.ToArray();
        }
    }
}