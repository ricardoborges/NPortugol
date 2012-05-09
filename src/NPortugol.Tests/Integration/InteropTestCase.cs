using NPortugol.Runtime;
using NUnit.Framework;

namespace NPortugol.Tests.Integration
{
    [TestFixture]
    public class InteropTestCase
    {
        public class Usuario
        {
            public string Email { get; set; }
        }

        public class Venda
        {
            public int valor { get; set; }
        }

        [Test]
        public void Should_Bind_CSharp_Object_Property()
        {
            var script =
                @"
  funcao calcular(venda)
     variavel total
     total = venda.valor + 10
  fim
";
            var engine = new Engine(new Npc());

            engine.Compile(script);

            engine.Execute("calcular", new Venda {valor = 20});
            
            Assert.AreEqual(30, engine.RuntimeContext.GetSymbolValue("calcular_total_0"));
        }

        [Test]
        public void Should_Bind_CSharp_Object_Property2()
        {
            var script =
                @"
  funcao email(usuario)
    variavel m
    m = usuario.Email
  fim
";
            var engine = new Engine(new Npc());
            engine.Compile(script);

            engine.Execute("email", new Usuario {Email = "ricardoborgesAtGmailDotCom"});

            Assert.AreEqual("ricardoborgesAtGmailDotCom", engine.RuntimeContext.GetSymbolValue("email_m_0"));
        }
    }
}