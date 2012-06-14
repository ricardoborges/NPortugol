using NUnit.Framework;

namespace NPortugol.Tests
{
    [TestFixture]
    public class TranslationTestCase
    {
        [Test]
        public void Should_Translate_Add_Expression()
        {
            var script = "funcao soma() retorne 2 + 2 fim";

            var bytecode = new Npc().Compile(script);

            var dm = new ILTranslator(bytecode).Translate();

            var result = dm.Invoke(null, null);

            Assert.AreEqual(4, result);
        }
    }
}