using Antlr.Runtime;
using NPortugol.Runtime;
using NPortugol.Runtime.Asm;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Asm.Generated
{
    [TestFixture]
    public class ANTLRParserTestCase
    {
        [Test]
        public void Parser_Should_Build_Funcion_And_EXIT_Instruction()
        {
            var parser = GetParser("main: \r\n EXIT");
            parser.script();

            Assert.IsTrue(parser._functionTable.ContainsKey("main"));

            Assert.AreEqual(OpCode.EXIT, parser._instructions[0].OpCode);
        }

        [Test]
        public void Parser_Should_Build_One_OP_Instruction()
        {
            var parser = GetParser("main:\r\nDCL x\r\nEXIT");
           
            parser.script();

            Assert.IsTrue(parser._functionTable.ContainsKey("main"));
            Assert.AreEqual(OpCode.DCL, parser._instructions[0].OpCode);
            Assert.AreEqual(OperandType.Variable, parser._instructions[0].Operands[0].Type);
            Assert.AreEqual("x", parser._instructions[0].Operands[0].Value);
        }

        [Test]
        public void Parser_Should_Build_Two_OP_Instruction()
        {
            var parser = GetParser("main:\r\n MOV x, 1 \r\n \r\n EXIT");
           
            parser.script();

            Assert.IsTrue(parser._functionTable.ContainsKey("main"));
            Assert.AreEqual(OpCode.MOV, parser._instructions[0].OpCode);
            Assert.AreEqual(OperandType.Variable, parser._instructions[0].Operands[0].Type);
            Assert.AreEqual(OperandType.Literal, parser._instructions[0].Operands[1].Type);
            Assert.AreEqual(1, parser._instructions[0].Operands[1].Value);
        }

        [Test]
        public void Parser_Should_Build_Two_OP_STRING_Instruction()
        {
            var parser = GetParser("main:\r\n MOV x, \"1\" \r\n \r\n EXIT");
            
            parser.script();

            Assert.IsTrue(parser._functionTable.ContainsKey("main"));
            Assert.AreEqual(OpCode.MOV, parser._instructions[0].OpCode);
            Assert.AreEqual(OperandType.Variable, parser._instructions[0].Operands[0].Type);
            Assert.AreEqual(OperandType.Literal, parser._instructions[0].Operands[1].Type);
            Assert.AreEqual("1", parser._instructions[0].Operands[1].Value);
        }

        [Test]
        public void Parser_Should_Resolve_Label()
        {
            var script = "main:\r\n JMP end\r\n DCL x\r\n .end\r\n EXIT";

            var parser = GetParser(script);

            parser.script();

            Assert.AreEqual(1, parser._labels.Count);
            Assert.AreEqual(OperandType.InstructionRef, parser._instructions[0].Operands[0].Type);
        }

        [Test]
        public void Parser_Should_Resolve_Push_Index_Var()
        {
            var f = (float)1.0;

            var i = (int) f;

            var script = "main:\r\n DCL v\r\n DCL i\r\n i=0\r\n PUSH 10\r\n POP v:1 EXIT\r\n";

            var parser = GetParser(script);

            parser.script();
            
        }

        [Test]
        public void Bug01()
        {
            var script = "pow:\r\nDCL x\r\nDCL y\r\nPOP y\r\nPOP x\r\nJLE y, 0, endloop\r\nDCL tmp1\r\nMOV tmp1, y\r\nSUB tmp1, 1\r\nPUSH x\r\nPUSH tmp1\r\nCALL pow\r\nDCL tmp2\r\nPOP tmp2\r\nMUL tmp2, x\r\nPUSH tmp2\r\nRET\r\n.endloop\r\nPUSH 1\r\nRET";
            var parser = GetParser(script);

            parser.script();
        }

        public ASMParser GetParser(string script)
        {
            var input = new ANTLRStringStream(script);
            var lexer = new ASMLexer(input);
            var tokens = new CommonTokenStream(lexer);
            return new ASMParser(tokens);
        }
    }
}