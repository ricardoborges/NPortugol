using Moq;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Instructions
{
    [TestFixture]
    public class JMPTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Declare_Variable()
        {
            var contextMock = new Mock<IRuntimeContext>();

            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.JMP, 0, new Operand(OperandType.Literal, 5))
                );

            contextMock.Setup(x => x.CurrentFunction).Returns(GetMainFunction());

            contextMock.Setup(x => x.Runnable).Returns(GetRunnable());

            var executor = new Executor(contextMock.Object);

            executor.ExecuteInstruction();

            Assert.AreEqual(5, contextMock.Object.Runnable.IP);
        }
    }
}