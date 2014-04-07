using System.Collections.Generic;
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

        public IList<string> Compile(string code)
        {
            return compiler.Compilar(code).Script;
        }

        [Test]
        public void Build_Main_Function()
        {
            var script = Compile("fun��o principal() fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("RET", script[2]);
        }

        [Test]
        public void Build_Multi_Functions()
        {
            var script = Compile("fun��o principal() fim fun��o do() fim fun��o other() fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("do:", script[3]);
            Assert.AreEqual("other:", script[6]);
        }

        [Test]
        public void Build_Function_Params()
        {
            var script = Compile("fun��o do(x) fim");

            Assert.AreEqual("do:", script[0]);
            Assert.AreEqual("DCL x", script[1]);
            Assert.AreEqual("POP x", script[2]);
        }

        [Test]
        public void Build_Function_Call()
        {
            var script = Compile("fun��o main() do(10) fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("PUSH 10", script[2]);
            Assert.AreEqual("CALL do", script[3]);
        }

        [Test]
        public void Build_Branching()
        {
            var template = "fun��o main() se 1 {0} 1 ent�o fim fim";

            var script = Compile(string.Format(template, "=="));
            Assert.AreEqual("JNE label_0", script[4]);

            script = Compile(string.Format(template, ">"));
            Assert.AreEqual("JLE label_0", script[4]);

            script = Compile(string.Format(template, "<"));
            Assert.AreEqual("JGE label_0", script[4]);

            script = Compile(string.Format(template, ">="));
            Assert.AreEqual("JL label_0", script[4]);

            script = Compile(string.Format(template, "<="));
            Assert.AreEqual("JG label_0", script[4]);

            script = Compile(string.Format(template, "!="));
            Assert.AreEqual("JE label_0", script[4]);
        }

        [Test]
        public void Build_Branching_Else()
        {
            var script = Compile("fun��o main() se 1 == 1 ent�o vari�vel x sen�o vari�vel y fim fim");

            Assert.AreEqual("JNE label_0", script[4]);
            Assert.AreEqual("JMP label_1", script[6]);
            Assert.AreEqual(".label_0", script[7]);
            Assert.AreEqual(".label_1", script[9]);
        }

        [Test]
        public void Build_Loop_For()
        {
            var script = Compile("fun��o main() para x = 1 at� 10 fim fim");
            Assert.AreEqual(".label_0", script[3]);
            Assert.AreEqual("JG label_1", script[6]);
            Assert.AreEqual("JMP label_0", script[8]);
            Assert.AreEqual(".label_1", script[9]);

            script = Compile("fun��o main() para x = 1 at� 10 dec fim fim");
            Assert.AreEqual("JL label_1", script[6]);
        }
    }
}