using Moq;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Instructions
{
    [TestFixture]
    public class DCLTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Declare_Variable()
        {
            var contextMock = new Mock<IRuntimeContext>();

            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.DCL, 0, new Operand(OperandType.Variable, "x"))
                );

            contextMock.Setup(x => x.CurrentFunction).Returns(GetMainFunction());

            contextMock.Setup(x => x.Runnable).Returns(GetRunnable());

            var executor = new InstrucExecutor(contextMock.Object);

            executor.ExecuteInstruction();

            Assert.IsTrue(executor.SymbolTable.ContainsKey(SymbolName("x")));
        }
    }
}