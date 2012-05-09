using NUnit.Framework;

namespace NPortugol.Tests
{
    [TestFixture]
    public class CompilerTestCase
    {
        private Npc compiler;

        [SetUp]
        public void Init()
        {
            compiler = new Npc();
        }

        [Test]
        public void Build_Main_Function()
        {
            var script = compiler.Compile("funcao principal() fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("RET", script[1]);
        }

        [Test]
        public void Build_Multi_Functions()
        {
            var script = compiler.Compile("funcao principal() fim funcao do() fim funcao other() fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("do:", script[2]);
            Assert.AreEqual("other:", script[4]);
        }

        [Test]
        public void Build_Function_Params()
        {
            var script = compiler.Compile("funcao do(x) fim");

            Assert.AreEqual("do:", script[0]);
            Assert.AreEqual("DCL x", script[1]);
            Assert.AreEqual("POP x", script[2]);
        }

        [Test]
        public void Build_Function_Call()
        {
            var script = compiler.Compile("funcao main() do(10) fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("PUSH 10", script[1]);
            Assert.AreEqual("CALL do", script[2]);
        }

        [Test]
        public void Build_Branching()
        {
            var template = "funcao main() se 1 {0} 1 entao fim fim";

            var script = compiler.Compile(string.Format(template, "=="));
            Assert.AreEqual("JNE label_0", script[3]);

            script = compiler.Compile(string.Format(template, ">"));
            Assert.AreEqual("JLE label_0", script[3]);

            script = compiler.Compile(string.Format(template, "<"));
            Assert.AreEqual("JGE label_0", script[3]);

            script = compiler.Compile(string.Format(template, ">="));
            Assert.AreEqual("JL label_0", script[3]);

            script = compiler.Compile(string.Format(template, "<="));
            Assert.AreEqual("JG label_0", script[3]);

            script = compiler.Compile(string.Format(template, "!="));
            Assert.AreEqual("JE label_0", script[3]);
        }

        [Test]
        public void Build_Branching_Else()
        {
            var script = compiler.Compile("funcao main() se 1 == 1 entao variavel x senao variavel y fim fim");

            Assert.AreEqual("JNE label_0", script[3]);
            Assert.AreEqual("JMP label_1", script[5]);
            Assert.AreEqual(".label_0", script[6]);
            Assert.AreEqual(".label_1", script[8]);
        }

        [Test]
        public void Build_Loop_For()
        {
            var script = compiler.Compile("funcao main() para x = 1 ate 10 fim fim");
            Assert.AreEqual(".label_0", script[2]);
            Assert.AreEqual("JG label_1", script[5]);
            Assert.AreEqual("JMP label_0", script[7]);
            Assert.AreEqual(".label_1", script[8]);

            script = compiler.Compile("funcao main() para x = 1 ate 10 dec fim fim");
            Assert.AreEqual("JL label_1", script[5]);
        }
    }
}