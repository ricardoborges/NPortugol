using NUnit.Framework;

namespace NPortugol.Tests
{
    public class GrammarTestCase: BaseTest
    {
        [Test]
        public void Should_Parse_Function()
        {
            var tree = BuildAST("funcao main() fim");
            
            Assert.AreEqual("FUNC", tree.Token.Text);
            Assert.AreEqual("main", tree.Children[0].Text);
        }

        [Test]
        public void Should_Parse_Function_Params()
        {
            var tree = BuildAST("funcao main(x, y) fim");

            Assert.AreEqual("PARAM", tree.Children[1].ToString());
            Assert.AreEqual("x", tree.Children[1].GetChild(0).Text);
            Assert.AreEqual("y", tree.Children[1].GetChild(1).Text);
        }

        [Test]
        public void Should_Parse_Var_Declaration()
        {
            var tree = BuildAST("funcao main() variavel first, second fim");

            Assert.AreEqual("SLIST", tree.Children[1].ToString());
            Assert.AreEqual("VAR", tree.Children[1].GetChild(0).ToString());
            Assert.AreEqual("first", tree.Children[1].GetChild(0).GetChild(0).Text);
            Assert.AreEqual("second", tree.Children[1].GetChild(0).GetChild(1).Text);
        }

        [Test]
        public void Should_Parse_Return()
        {
            var tree = BuildAST("funcao main() retorne 10 fim");

            Assert.AreEqual("SLIST", tree.Children[1].ToString());
            Assert.AreEqual("RET", tree.Children[1].GetChild(0).ToString());
            Assert.AreEqual("10", tree.Children[1].GetChild(0).GetChild(0).Text);
        }

        [Test]
        public void Should_Parse_Function_Call()
        {
            var tree = BuildAST("funcao main() do(x) fim");

            Assert.AreEqual("SLIST", tree.Children[1].ToString());
            Assert.AreEqual("CALL", tree.Children[1].GetChild(0).ToString());
            Assert.AreEqual("do", tree.Children[1].GetChild(0).GetChild(0).Text);

            var argNode = tree.Children[1].GetChild(0).GetChild(1);

            Assert.AreEqual("ARG", argNode.ToString());
            Assert.AreEqual("x", argNode.GetChild(0).Text);
        }

        [Test]
        public void Should_Parse_If_Stat()
        {
            var tree = BuildAST("funcao main() se 1 == 1 entao fim fim");

            var jmpNode = tree.Children[1].GetChild(0);

            Assert.AreEqual("SLIST", tree.Children[1].ToString());
            Assert.AreEqual("JMP", jmpNode.ToString());
            Assert.AreEqual("LEXP", jmpNode.GetChild(0).ToString());
            Assert.AreEqual("==", jmpNode.GetChild(0).GetChild(0).Text);
        }

        [Test]
        public void Should_Parse_Else_Stat()
        {
            var tree = BuildAST("funcao main() se 1 == 1 entao variavel x senao variavel y fim fim");

            var sjmpNode = tree.Children[1].GetChild(0);

            var s1 = sjmpNode.GetChild(1).GetChild(0);
            var s2 = sjmpNode.GetChild(1).GetChild(1);

            Assert.AreEqual("SJMP", sjmpNode.ToString());
            Assert.AreEqual("LEXP", sjmpNode.GetChild(0).ToString());
            Assert.AreEqual("==", sjmpNode.GetChild(0).GetChild(0).Text);
            Assert.AreEqual("VAR", s1.ToString());
            Assert.AreEqual("SLIST", s2.ToString());
            Assert.AreEqual("VAR", s2.GetChild(0).ToString());
        }

        [Test]
        public void Should_Parse_For_Stat()
        {
            var tree = BuildAST("funcao main() para x=0 ate 10 fim fim");

            var loopNode = tree.Children[1].GetChild(0);

            var assignNode = loopNode.GetChild(0);
            var value = loopNode.GetChild(1);
            var stat = loopNode.GetChild(2);

            Assert.AreEqual("ASGN", assignNode.ToString());
            Assert.AreEqual("10", value.Text);
            Assert.AreEqual("SLIST", stat.ToString());
        }
    }
}