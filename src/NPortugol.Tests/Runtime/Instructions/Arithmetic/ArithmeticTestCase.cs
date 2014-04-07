using Moq;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Instructions.Arithmetic
{
    [TestFixture]
    public class ArithmeticTestCase: BaseInstTest
    {
        private Mock<IRuntimeContext> contextMock;
        private Executor executor;

        [SetUp]
        public void Init()
        {
            contextMock = new Mock<IRuntimeContext>();

            contextMock.Setup(x => x.CurrentFunction).Returns(GetMainFunction());

            contextMock.Setup(x => x.Runnable).Returns(GetRunnable());

            executor = new Executor(contextMock.Object);
        }

        [Test]
        public void Process_Should_Arithmetic_ADD()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.ADD, 0, new Operand(OperandType.Variable, "x"), new Operand(OperandType.Literal, 1))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(11, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_ADD_VAR()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.ADD, 0, new Operand(OperandType.Variable, "x"), 
                                new Operand(OperandType.Variable, "y"))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(15, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_DEC()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.DEC, 0, new Operand(OperandType.Variable, "x"))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(9, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_DIV()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.DIV, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Literal, 2))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(5, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_DIV_VAR()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.DIV, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Variable, "y"))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(2, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_INC()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.INC, 0, new Operand(OperandType.Variable, "x"))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(11, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_MOD()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.MOD, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Literal, 3))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(1, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_MOD_VAR()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.MOD, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Variable, "y"))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(0, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_MUL()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.MUL, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Literal, 2))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(20, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_MUL_VAR()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.MUL, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Variable, "y"))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(50, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_NEG()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.NEG, 0, new Operand(OperandType.Variable, "x")));

            executor.ExecuteInstruction();

            Assert.AreEqual(-10, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_POW()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.POW, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Literal, 2))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(100, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_POW_VAR()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.POW, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Variable, "y"))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(100000, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_SUB()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.SUB, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Literal, 2))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(8, executor.SymbolTable[SymbolId("x")].Value);
        }

        [Test]
        public void Process_Should_Arithmetic_SUB_VAR()
        {
            contextMock.Setup(x => x.CurrentInst).Returns(
                new Instruction(OpCode.SUB, 0, new Operand(OperandType.Variable, "x"),
                                new Operand(OperandType.Variable, "y"))
                );

            executor.ExecuteInstruction();

            Assert.AreEqual(5, executor.SymbolTable[SymbolId("x")].Value);
        }

        public override Runnable GetRunnable()
        {
            var x = new Symbol {Value = 10};

            var s1 = new Symbol {Value = 10};
            var s2 = new Symbol {Value = 5};

            var stable = new SymbolTable(null)
                             {
                                 {"main_x_0", s1},
                                 {"main_y_0",  s2}
                             };

            return new Runnable(new InstrucStream(), new FunctionTable(), stable);
        }
    }
}