using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Instructions
{
    [TestFixture]
    public class BranchingTestCase : BaseInstTest
    {
        public Instruction[] GetInstructionsFor(Instruction jmp)
        {
            var list = new List<Instruction>
                           {
                               // Function Main
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),
                               new Instruction(OpCode.PUSH, 1, new Operand(OperandType.Literal, 5)),
                               new Instruction(OpCode.PUSH, 2, new Operand(OperandType.Literal, 5)),
                               jmp,
                               new Instruction(OpCode.MOV, 4, new Operand(OperandType.Variable, "x"), new Operand(OperandType.Literal, 10)),
                               new Instruction(OpCode.EXIT, 5, new Operand(OperandType.Null, null)), 
                           };

            return list.ToArray();
        }

        public Instruction[] GetInstructionsVar(Instruction jmp)
        {
            var list = new List<Instruction>
                           {
                               // Function Main
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),
                               new Instruction(OpCode.PUSH, 1, new Operand(OperandType.Literal, 5)),
                               new Instruction(OpCode.PUSH, 2, new Operand(OperandType.Literal, 6)),
                               jmp,
                               new Instruction(OpCode.MOV, 4, new Operand(OperandType.Variable, "x"), new Operand(OperandType.Literal, 10)),
                               new Instruction(OpCode.EXIT, 5, new Operand(OperandType.Null, null)), 
                           };

            return list.ToArray();
        }

        [Test]
        public void Process_Should_Unconditional_Jump()
        {
            var inst = new Instruction(OpCode.JMP, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsFor(inst));

            context.Execute();

            Assert.IsNull(context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Equals()
        {
            var inst = new Instruction(OpCode.JE, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsFor(inst));

            context.Execute();

            Assert.IsNull(context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Equals_Vars()
        {
            var inst = new Instruction(OpCode.JE, 3, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsVar(inst));

            context.Execute();

            Assert.AreEqual(10, context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Not_Equals()
        {
            var inst = new Instruction(OpCode.JNE, 1, new Operand(OperandType.InstructionRef, 4));

            var context = GetContextWithMain(GetInstructionsFor(inst));

            context.Execute();

            Assert.AreEqual(10, context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Not_Equals_Vars()
        {
            var inst = new Instruction(OpCode.JNE, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsVar(inst));

            context.Execute();

            Assert.IsNull(context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Less()
        {
            var inst = new Instruction(OpCode.JL, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsFor(inst));

            context.Execute();

            Assert.AreEqual(10, context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Less_Vars()
        {
            var inst = new Instruction(OpCode.JL, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsVar(inst));

            context.Execute();

            Assert.IsNull(context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Less_Equals()
        {
            var inst = new Instruction(OpCode.JLE, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsFor(inst));

            context.Execute();

            Assert.IsNull(context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Less_Equals_Vars()
        {
            var inst = new Instruction(OpCode.JLE, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsVar(inst));

            context.Execute();

            Assert.IsNull(context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Greater()
        {
            var inst = new Instruction(OpCode.JG, 1, new Operand(OperandType.InstructionRef, 5));
            
            var context = GetContextWithMain(GetInstructionsFor(inst));

            context.Execute();

            Assert.AreEqual(10, context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Greater_Vars()
        {
            var inst = new Instruction(OpCode.JG, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsVar(inst));

            context.Execute();

            Assert.AreEqual(10, context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Greater_Equals()
        {
            var inst = new Instruction(OpCode.JGE, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsFor(inst));

            context.Execute();

            Assert.IsNull(context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        [Test]
        public void Process_Should_Jump_Greater_Equals_Vars()
        {
            var inst = new Instruction(OpCode.JGE, 1, new Operand(OperandType.InstructionRef, 5));

            var context = GetContextWithMain(GetInstructionsVar(inst));

            context.Execute();

            Assert.AreEqual(10, context.Runnable.ScriptSymbolTable["main_x_0"]);
        }

        public override Instruction[] GetInstructions()
        {
            return null;
        }
    }
}