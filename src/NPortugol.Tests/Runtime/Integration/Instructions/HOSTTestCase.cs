using System;
using System.Collections.Generic;
using NPortugol.Runtime;
using NPortugol.Runtime.Interop;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.Instructions
{
    public class MensagemFunc : IHostFunction
    {
        public object Execute(params object[] parameters)
        {
            return "Esta é uma mensagem from C#";
        }

        public string Name { get; set; }
    }

    public class PlusFunc : IHostFunction
    {
        public object Execute(params object[] parameters)
        {
            return (int)parameters[0] + (int)parameters[1];
        }

        public string Name { get; set; }
    }

    [TestFixture]
    public class HOSTTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Invoke_Host_Function()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions());

            var context = new RuntimeContext(runnable);

            context.HostContainer.Register("Msg", new MensagemFunc());

            context.Execute();

            Assert.AreEqual("Esta é uma mensagem from C#", context.GetSymbolValue("main_x_0"));
        }

        [Test]
        public void Process_Should_Invoke_Host_Handler()
        {
            var runnable = GetRunnableWithMainFor(GetInstructions());

            var script = runnable;

            var context = new RuntimeContext(script);

            context.HostContainer.Register("Msg", (parameters) => "Hello from C#!");

            context.Execute();

            Assert.AreEqual("Hello from C#!", context.GetSymbolValue("main_x_0"));
        }

        public override Instruction[] GetInstructions()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),     
                               new Instruction(OpCode.CALL, 2, new Operand(OperandType.HostFunctionRef, "Msg")), 
                               new Instruction(OpCode.POP, 3, new Operand(OperandType.Variable, "x")), 
                               new Instruction(OpCode.EXIT, 4, new Operand(OperandType.Null, null)), 
                           };

            return list.ToArray();
        }

        public Instruction[] GetInstructionsParams()
        {
            var list = new List<Instruction>
                           {
                               new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x")),     
                               new Instruction(OpCode.PUSH, 1, new Operand(OperandType.Literal, 2)), 
                               new Instruction(OpCode.PUSH, 2, new Operand(OperandType.Literal, 2)), 
                               new Instruction(OpCode.CALL, 3, new Operand(OperandType.HostFunctionRef, "Plus")), 
                               new Instruction(OpCode.POP, 4, new Operand(OperandType.Variable, "x")), 
                               new Instruction(OpCode.EXIT, 5, new Operand(OperandType.Null, null)), 
                           };

            return list.ToArray();
        }
    }
}