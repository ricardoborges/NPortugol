using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Sample
{
    [TestFixture]
    public class FactorialTestCase: BaseRuntimeTest
    {
        [Test]
        public void FactorialCalc()
        {
            var runnable = GetRunnableFor("fat", GetInstructions());

            var context = new RuntimeContext(runnable);

            var total = context.Execute("fat", 3);

            Assert.AreEqual(6, total);
            Assert.AreEqual(1, context.Execute("fat", 0));
            Assert.AreEqual(1, context.Execute("fat", 1));
            Assert.AreEqual(24, context.Execute("fat", 4));
            Assert.AreEqual(120, context.Execute("fat", 5));
            Assert.AreEqual(720, context.Execute("fat", 6));
        }

        public Instruction[] GetInstructions()
        {
            return new List<Instruction>
                       {
                           new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "total")),  // DCL total
                           new Instruction(OpCode.DCL, 1, new Operand(OperandType.Variable, "x")),      // DCL x
                           new Instruction(OpCode.DCL, 2, new Operand(OperandType.Variable, "i")),      // DCL i
                           new Instruction(OpCode.POP, 3, new Operand(OperandType.Variable, "x")),      // POP x
                           new Instruction(OpCode.PUSH, 4, new Operand(OperandType.Variable, "x")),     // PUSH x
                           new Instruction(OpCode.PUSH, 5, new Operand(OperandType.Literal, 0)),        // PUSH 0
                           new Instruction(OpCode.JNE, 6, new Operand(OperandType.InstructionRef, 9)),  // JNE calc
                           new Instruction(OpCode.MOV, 7, new Operand(OperandType.Variable, "total"),   // MOV total, 1
                                           new Operand(OperandType.Literal, 1)),
                           new Instruction(OpCode.JMP, 8, new Operand(OperandType.InstructionRef, 20)), // JMP endloop
                           new Instruction(OpCode.MOV, 9, new Operand(OperandType.Variable, "total"),   // .calc | MOV total, x
                                           new Operand(OperandType.Variable, "x")),
                           new Instruction(OpCode.MOV, 10, new Operand(OperandType.Variable, "i"),      //  MOV i, 1
                                           new Operand(OperandType.Literal, 1)),
                           new Instruction(OpCode.DCL, 11, new Operand(OperandType.Variable, "tmp1")),  // DCL tmp1 
                           new Instruction(OpCode.PUSH, 12, new Operand(OperandType.Variable, "x")),    // .loop | PUSH x
                           new Instruction(OpCode.PUSH, 13, new Operand(OperandType.Variable, "i")),    // PUSH i
                           new Instruction(OpCode.JE, 14,  new Operand(OperandType.InstructionRef, 20)),// JE endloop
                           new Instruction(OpCode.MOV, 15, new Operand(OperandType.Variable, "tmp1"),   // MOV tmp1, x 
                                           new Operand(OperandType.Variable, "x")),
                           new Instruction(OpCode.SUB, 16, new Operand(OperandType.Variable, "tmp1"),   // SUB tmp1, i
                                           new Operand(OperandType.Variable, "i")),
                           new Instruction(OpCode.MUL, 17, new Operand(OperandType.Variable, "total"),  // MUL total, tmp1
                                           new Operand(OperandType.Variable, "tmp1")),
                           new Instruction(OpCode.INC, 18, new Operand(OperandType.Variable, "i")),     // INC i
                           new Instruction(OpCode.JMP, 19, new Operand(OperandType.InstructionRef, 12)),// JMP loop
                           new Instruction(OpCode.PUSH, 20, new Operand(OperandType.Variable, "total")),// .endloop | PUSH total
                           new Instruction(OpCode.RET, 21) //"RET" 
                       }
                .ToArray();
        }
    }
}