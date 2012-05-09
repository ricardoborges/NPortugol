using NUnit.Framework;
using PSharp.Runtime.Interop;
using PSharp.Runtime.Tests.Integration.Instructions;

namespace PSharp.Runtime.Tests.Integration
{
    public class Mensagem : IHostFunction
    {
        public object Execute(params object[] parameters)
        {
            return "Esta é uma mensagem from C#";
        }
    }

    [TestFixture]
    public class HostContainerTestCase: BaseInstTest
    {
        [Test]
        public void Process_Should_Invoke_Host_Function()
        {
            var context = GetContext();

            context.HostContainer.Register("Mensagem", new Mensagem());

            var function = context.HostContainer.Resolve("Mensagem");

            var result = function.Execute(null);

            Assert.AreEqual("Esta é uma mensagem from C#", result);
        }

        [Test]
        public void Process_Should_Invoke_Host_Handler()
        {
            var context = GetContext();
                
            context.HostContainer.Register("Mensagem", p => Mensagem(p));

            var function = context.HostContainer.ResolveHandler("Mensagem");

            var result = function.Invoke(null);

            Assert.AreEqual("Esta é uma mensagem from C#", result);
        }

        public object Mensagem(params object[] parameters)
        {
            return "Esta é uma mensagem from C#";
        }

        public override Instruction[] GetInstructions()
        {
            return null;
        }

        public RuntimeContext GetContext()
        {
            var runnable = GetRunnableWithMainFor(GetDeclareXInstructions());

            var script = new RuntimeScript(runnable);

           return new RuntimeContext(script);
        }
    }
}