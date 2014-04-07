using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.VMEngine
{
    [TestFixture]
    public class RecursionTestCase
    {
        [Test]
        public void Recursion_Call()
        {
            var engine = new Motor();

            engine.Load(new Bytecode(GetLines()));

            Assert.AreEqual(9, engine.Execute("pow", new object[] {3, 2} ));
            Assert.AreEqual(27, engine.Execute("pow", new object[] { 3, 3 } ));
            Assert.AreEqual(16, engine.Execute("pow", new object[] { 2, 4 } ));
            Assert.AreEqual(125, engine.Execute("pow", new object[] { 5, 3 } ));
        }

        public static IList<string> GetLines()
        {
            return new[]
                       {
                           "pow:",
                           "DCL x",
                           "DCL y",
                           "POP y",
                           "POP x",
                           "PUSH y",
                           "PUSH 0",
                           "JLE endloop",
                           "DCL tmp1",
                           "MOV tmp1, y",
                           "SUB tmp1, 1",
                           "PUSH x",
                           "PUSH tmp1",
                           "CALL pow",
                           "DCL tmp2",
                           "POP tmp2",
                           "MUL tmp2, x",
                           "PUSH tmp2",
                           "RET",
                           ".endloop",
                           "PUSH 1",
                           "RET"                           
                       };
        }
    }
}