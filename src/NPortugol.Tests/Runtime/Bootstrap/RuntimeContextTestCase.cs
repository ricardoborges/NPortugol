using NPortugol.Runtime;
using NPortugol.Runtime.Exceptions;
using NPortugol.Tests.Runtime.Integration;
using NUnit.Framework;

namespace NPortugol.Tests.Runtime.Bootstrap
{
    [TestFixture]
    public class RuntimeContextTestCase: BaseRuntimeTest
    {
        [Test]
        public void Runtime_Should_Throws_Main_Function_Not_Found()
        {
            var context = CreateContext(GetRunnableFor("DoSomething", GetDeclareXInstructions()));

            Assert.Throws(typeof (MainNotFoundException), context.Execute);
        }

        [Test]
        public void Runtime_Should_Throws_Defined_Function_Not_Found()
        {
            var context = CreateContext(GetRunnableFor("DoSomething", GetDeclareXInstructions()));

            Assert.Throws(typeof (FunctionNotFoundException), () => context.Execute("DoOther", null));
        }

        [Test]
        public void Runtime_Should_Load_Main_Function()
        {
            var context = CreateContext(GetRunnableWithMainFor(GetDeclareXInstructions()));

            context.InitWithMain();

            Assert.AreEqual(Function.MainName, context.Runnable.MainFunction.Name);

            Assert.IsTrue(context.Runnable.FunctionTable.ContainsKey(Function.MainName));
        }

        [Test]
        public void Runtime_Should_Load_Specific_Function()
        {
            const string fname = "DoSomething";

            var context = CreateContext(GetRunnableFor(fname, GetDeclareXInstructions()));

            context.Init("DoSomething");

            Assert.IsTrue(context.Runnable.FunctionTable.ContainsKey(fname));
        }

        public RuntimeContext CreateContext(Runnable runnable)
        {
            var script = runnable;

            return  new RuntimeContext(script);
        }
    }
}