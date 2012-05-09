using NPortugol.Runtime;
using NPortugol.Runtime.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;

namespace NPortugol.Tests.Runtime.Integration.Instructions
{
    [TestFixture]
    public class MOVTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Assign_Local_Variable_With_Literal()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions());

            var script = new RuntimeScript(runnable);

            var context = new RuntimeContext(script);

            context.Execute();

            var value = context.GetSymbolValue("main_name_0");

            Assert.AreEqual("Ricardo", value);
        }

        [Test]
        public void Process_Should_Assign_Local_Variable_With_Other_Variable()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions2());

            var script = new RuntimeScript(runnable);

            var context = new RuntimeContext(script);

            context.Execute();

            var value = context.GetSymbolValue("main_name2_0");

            Assert.AreEqual("Ricardo", value);
        }

        [Test]
        public void Process_Should_Stop_At_Ilegal_Assign()
        {
            var runnable = GetRunnableWithMainFor(GetIlegalInstructions()); 

            var script = new RuntimeScript(runnable);

            var context = new RuntimeContext(script);

            Assert.Throws(typeof (RuntimeException), context.Execute);
        }

        [Test]
        public void Process_Should_Assign_Indexed_Variable_With_Value()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions3());

            var script = new RuntimeScript(runnable);

            var context = new RuntimeContext(script);

            context.Execute();

            var value = context.GetSymbolValue("main_list_0", 1);

            Assert.AreEqual("teste", value);
        }

        [Test]
        public void Process_Should_Assign_Indexed_Variable_With_Indexed_Variable()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions4());

            var script = new RuntimeScript(runnable);

            var context = new RuntimeContext(script);

            context.Execute();

            var value = context.GetSymbolValue("main_list_0", 1);

            Assert.AreEqual("teste", value);
        }

        public Instruction[] GetIlegalInstructions()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "name")),
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Literal, "Ricardo"),
                                               new Operand(OperandType.Variable, "name")),
                               new Instruction(OpCode.EXIT, 2)
                           };

            return list.ToArray();
        }

        public override Instruction[] GetInstructions()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "name")),
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "name"),
                                               new Operand(OperandType.Literal, "Ricardo")),
                               new Instruction(OpCode.EXIT, 2)
                           };

            return list.ToArray();
        }

        public Instruction[] GetInstructions4()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "array")),
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "array", 1), new Operand(OperandType.Literal, "teste")),
                               
                               new Instruction(OpCode.DCL, 2, new Operand(OperandType.Variable, "list")),
                               new Instruction(OpCode.MOV, 3, new Operand(OperandType.Variable, "list", 1), new Operand(OperandType.Variable, "array", 1)),

                               new Instruction(OpCode.EXIT, 4)
                           };

            return list.ToArray();
        }

        public Instruction[] GetInstructions3()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "list")),
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "list", 1),
                                               new Operand(OperandType.Literal, "teste")),
                               new Instruction(OpCode.EXIT, 2)
                           };

            return list.ToArray();
        }

        public Instruction[] GetInstructions2()
        {
            var list = new List<Instruction>();

            var op_name = new Operand(OperandType.Variable, "name");
            var op_name2 = new Operand(OperandType.Variable, "name2");
            var op_value = new Operand(OperandType.Literal, "Ricardo");

            list.Add(new Instruction(OpCode.DCL, 0, op_name));
            list.Add(new Instruction(OpCode.DCL, 1, op_name2));
            list.Add(new Instruction(OpCode.MOV, 2, op_name, op_value));
            list.Add(new Instruction(OpCode.MOV, 3, op_name2, op_name));
            list.Add(new Instruction(OpCode.EXIT, 4));

            return list.ToArray();
        }
    }
}