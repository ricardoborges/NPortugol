using System;
using System.IO;
using System.Text;
using System.Web;
using NPortugol.Runtime;

namespace NPortugol.Web
{
    public interface IDefaultDispatcher
    {
        void Process(HttpContext context);
    }

    /// <summary>
    /// Prova de Conceito
    /// </summary>
    /// TODO: Criar uma camada de aplicação MVC que organize rotas de URL's e separação de responsabilidades
    public class DefaultDispatcher : IDefaultDispatcher
    {
        public void Process(HttpContext context)
        {
            var content = ExtractContent(context.Request.PhysicalPath);

            var script = new DefaultScriptBuilder().Parse(content);

            if (context.Request.Url.Query.Contains("script"))
                context.Response.Write("<pre>" +  HttpUtility.HtmlEncode(script) + "</pre>");
            else
                Execute(script);
        }

        private static void Execute(string script)
        {
            var engine = new Engine();

            engine.HostContainer.Register("escreva", new EscrevaFunc(), true);

            engine.Compile(script);

            engine.Execute();
        }

        private static string ExtractContent(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}