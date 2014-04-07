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
            var script = Compile("função principal() fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("RET", script[2]);
        }

        [Test]
        public void Build_Multi_Functions()
        {
            var script = Compile("função principal() fim função do() fim função other() fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("do:", script[3]);
            Assert.AreEqual("other:", script[6]);
        }

        [Test]
        public void Build_Function_Params()
        {
            var script = Compile("função do(x) fim");

            Assert.AreEqual("do:", script[0]);
            Assert.AreEqual("DCL x", script[1]);
            Assert.AreEqual("POP x", script[2]);
        }

        [Test]
        public void Build_Function_Call()
        {
            var script = Compile("função main() do(10) fim");

            Assert.AreEqual("main:", script[0]);
            Assert.AreEqual("PUSH 10", script[2]);
            Assert.AreEqual("CALL do", script[3]);
        }

        [Test]
        public void Build_Branching()
        {
            var template = "função main() se 1 {0} 1 então fim fim";

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
            var script = Compile("função main() se 1 == 1 então variável x senão variável y fim fim");

            Assert.AreEqual("JNE label_0", script[4]);
            Assert.AreEqual("JMP label_1", script[6]);
            Assert.AreEqual(".label_0", script[7]);
            Assert.AreEqual(".label_1", script[9]);
        }

        [Test]
        public void Build_Loop_For()
        {
            var script = Compile("função main() para x = 1 até 10 fim fim");
            Assert.AreEqual(".label_0", script[3]);
            Assert.AreEqual("JG label_1", script[6]);
            Assert.AreEqual("JMP label_0", script[8]);
            Assert.AreEqual(".label_1", script[9]);

            script = Compile("função main() para x = 1 até 10 dec fim fim");
            Assert.AreEqual("JL label_1", script[6]);
        }
    }
}