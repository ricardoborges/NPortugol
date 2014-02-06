using System;
using System.Collections.Generic;
using System.Text;
using NPortugol.Runtime;
using NPortugol.Runtime.Interop;
using NPortugol.Web.Script;
using NUnit.Framework;

namespace NPortugol.Tests.Web
{
    [TestFixture]
    public class ScriptBuilderTestCase
    {
        private string _page = @"
<html>
<body>

<% variavel i %>

<ul>
    <% para i = 0 ate 10 %>
    <li> Página <% escreva(i) %></li>
    <% fim %>
</ul>

</body>
</html>
";
        [Test]
        public void Should_Parse_Main_Function()
        {
            var sbuiler = new DefaultScriptBuilder();

            var function = sbuiler.Parse(_page);

            var engine = new Engine();
            
            //engine.HostContainer.Register("escreva", new EscrevaFunc(), false);
            
            engine.Compile(function);

            engine.Execute();
        }


    }
}