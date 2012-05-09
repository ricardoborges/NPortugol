using System.Collections.Generic;
using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Integration.VMEngine
{
    [TestFixture]
    public class LoopTestCase
    {
        [Test]
        public void Loop_Process()
        {
            var engine = new Engine();

            engine.LoadAsm(GetLines());

            Assert.AreEqual(1, engine.Execute("fat", 0));
            Assert.AreEqual(6, engine.Execute("fat", 3));
            Assert.AreEqual(24, engine.Execute("fat", 4));
            Assert.AreEqual(120, engine.Execute("fat", 5));
            Assert.AreEqual(720, engine.Execute("fat", 6));
        }

        private static IList<string> GetLines()
        {
            return new[]
                       {
                           "fat:", 
                           "DCL total", 
                           "DCL x", 
                           "DCL i", 
                           "POP x", 
                           "PUSH x",
                           "PUSH 0", 
                           "JNE calc",
                           "MOV total, 1",
                           "JMP endloop",
                           ".calc",
                           "MOV total, x", 
                           "MOV i, 1", 
                           "DCL tmp1", 
                           ".loop", 
                           "PUSH x",
                           "PUSH i", 
                           "JE endloop", 
                           "MOV tmp1, x", 
                           "SUB tmp1, i", 
                           "MUL total, tmp1", 
                           "INC i", 
                           "JMP loop", 
                           ".endloop", 
                           "PUSH total", 
                           "RET" 
                       };            
        }
    }
}