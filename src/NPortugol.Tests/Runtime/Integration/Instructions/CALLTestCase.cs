using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Instructions
{
    [TestFixture]
    public class CALLTestCase
    {
        [Test]
        public void Process_Should_Invoke_Defined_Function()
        {
            var runnable = BuildNoParamsScript();

            var context = new RuntimeContext(runnable);
            
            context.Execute();

            Assert.AreEqual("Ola", context.GetSymbolValue("do_y_1"));
            Assert.AreEqual("Ola", context.GetSymbolValue("main_x_0"));
        }

        [Test]
        public void Process_Should_Invoke_Defined_Function_With_Params()
        {
            var runnable = BuildWithParamsScript();

            var context = new RuntimeContext(runnable);
            
            context.Execute();

            Assert.AreEqual("Mundo", context.GetSymbolValue("do_y_1"));
            Assert.AreEqual("Mundo", context.GetSymbolValue("main_x_0"));
            Assert.AreEqual("temp", context.GetSymbolValue("do_x_1"));
            Assert.AreEqual("Ola", context.GetSymbolValue("main_msg_0"));
        }

        public Runnable BuildNoParamsScript()
        {
            var list = new List<Instruction>
                           {
                               // Function Main
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),     
                               new Instruction(OpCode.CALL, 1, new Operand(OperandType.FunctionRef, "Do")), 
                               new Instruction(OpCode.POP, 2, new Operand(OperandType.Variable, "x")), 
                               new Instruction(OpCode.EXIT, 3, new Operand(OperandType.Null, null)), 

                               // Function Do
                               new Instruction(OpCode.DCL, 4, new Operand(OperandType.Variable, "y")),     
                               new Instruction(OpCode.MOV, 5, new Operand(OperandType.Variable, "y"), 
                                               new Operand(OperandType.Literal, "Ola")),     
                               new Instruction(OpCode.PUSH, 6, new Operand(OperandType.Variable, "y")), 
                               new Instruction(OpCode.RET, 7)
                           };

            return GetScript(list.ToArray(), 4);
        }

        public Runnable BuildWithParamsScript()
        {
            var list = new List<Instruction>
                           {
                               // Function Main
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "msg")),     
                               new Instruction(OpCode.MOV, 1, new Operand(OperandType.Variable, "msg"), 
                                               new Operand(OperandType.Literal, "Ola")),     
                               new Instruction(OpCode.DCL, 2, new Operand(OperandType.Variable, "x")),     
                               new Instruction(OpCode.PUSH, 3, new Operand(OperandType.Variable, "msg")),    // first param
                               new Instruction(OpCode.PUSH, 4, new Operand(OperandType.Literal, "Mundo")),   // second param
                               new Instruction(OpCode.CALL, 5, new Operand(OperandType.FunctionRef, "Do")), 
                               new Instruction(OpCode.POP, 6, new Operand(OperandType.Variable, "x")), 
                               new Instruction(OpCode.EXIT, 7, new Operand(OperandType.Null, null)), 

                               // Function Do
                               new Instruction(OpCode.DCL, 8, new Operand(OperandType.Variable, "msg")),     
                               new Instruction(OpCode.DCL, 9, new Operand(OperandType.Variable, "y")),     
                               new Instruction(OpCode.POP, 10, new Operand(OperandType.Variable, "y")),   // second param
                               new Instruction(OpCode.POP, 11, new Operand(OperandType.Variable, "msg")),    // first param
                               new Instruction(OpCode.DCL, 12, new Operand(OperandType.Variable, "x")),     
                               new Instruction(OpCode.MOV, 13, new Operand(OperandType.Variable, "x"), 
                                               new Operand(OperandType.Literal, "temp")),     
                               new Instruction(OpCode.PUSH, 14, new Operand(OperandType.Variable, "y")), 
                               new Instruction(OpCode.RET, 15)
                           };

            return GetScript(list.ToArray(), 8);
        }

        public Runnable GetScript(Instruction[] instructions, int secondIndex)
        {
            var stream = new InstrucStream(instructions);

            var function = new Function(Function.MainName, 0);
            var dofunction = new Function("Do", secondIndex);

            var ftable = new FunctionTable
                             {
                                 { Function.MainName, function },
                                 { "Do", dofunction}
                             };

            return new Runnable(stream, ftable);
        }
    }
}