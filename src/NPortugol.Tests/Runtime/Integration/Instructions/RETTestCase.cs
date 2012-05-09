using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Instructions
{
    [TestFixture]
    public class RETTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_End_When_RET_Pop_Main_Function()
        {
            var stream = new InstrucStream(GetInstructions());

            var function = new Function(Function.MainName, 0);
            var dofunction = new Function("Do", 4);

            var ftable = new FunctionTable
                             {
                                 { Function.MainName, function },
                                 { "Do", dofunction}
                             };

            var runn = new Runnable(stream, ftable);

            var script = new RuntimeScript(runn);

            var context = new RuntimeContext(script);

            context.Execute();

            Assert.AreEqual("Ola", context.GetSymbolValue("main_x_0"));
            Assert.AreEqual("Ola", context.GetSymbolValue("do_y_1"));
        }

        public override Instruction[] GetInstructions()
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

            return list.ToArray();
        }
    }
}