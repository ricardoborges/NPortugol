using System.Collections.Generic;
using System.IO;
using System.Web;
using NPortugol.Runtime;
using NPortugol.Runtime.Interop;
using NPortugol.Web.Modules;
using NPortugol.Web.Script;

namespace NPortugol.Web.Core
{
    /// <summary>
    /// Prova de Conceito
    /// </summary>
    /// TODO: Criar uma camada de aplica��o MVC
    public class DefaultAppLayer : IAppLayer
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

            engine.Install(new WebModule());

            var modules = RetrieveModules();

            foreach (var module in modules)
                engine.Install(module);

            engine.Compile(script);

            engine.Execute("page", new Infra(HttpContext.Current));
        }

        private static List<IModule> RetrieveModules()
        {
            return ((INPortugolWebApp) HttpContext.Current.ApplicationInstance)
                .Container.Modules;
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