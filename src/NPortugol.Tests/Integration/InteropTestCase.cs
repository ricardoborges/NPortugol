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
  função calcular(venda)
     variável total
     total = venda.valor + 10
  fim
";
            var engine = new Motor(new Npc());

            engine.Compilar(script);

            engine.Executar("calcular", new Venda {valor = 20});
            
            Assert.AreEqual(30, engine.RuntimeContext.GetSymbolValue("calcular_total_0"));
        }

        [Test]
        public void Should_Bind_CSharp_Object_Property2()
        {
            var script =
                @"
  função email(usuario)
    variável m
    m = usuario.Email
  fim
";
            var engine = new Motor(new Npc());
            engine.Compilar(script);

            engine.Executar("email", new Usuario {Email = "ricardoborgesAtGmailDotCom"});

            Assert.AreEqual("ricardoborgesAtGmailDotCom", engine.RuntimeContext.GetSymbolValue("email_m_0"));
        }
    }
}