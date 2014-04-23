using System.Web;
using NPortugol.Runtime;
using NPortugol.Web.Modules;
using NPortugol.Web.Script;

namespace NPortugol.Web.App.Classic
{
    public class ClassicApp : BaseApp
    {
        public override void ProcessRequest(HttpContext context)
        {
            if (context.Request.Path == "/favicon.ico") return;

            var content = ExtractContent(context.Request.PhysicalPath);

            var script = new DefaultScriptBuilder().Parse(content);

            if (context.Request.Url.Query.Contains("script"))
            {
                context.Response.Write(
                    string.Format(
                        @"<table>
                            <tr><td>Script</td><td>VM code</td></tr>
                            <tr><td><pre>{0}</pre></td><td><pre>{1}</pre></td></tr>
                        </table>",
                        HttpUtility.HtmlEncode(script),
                        HttpUtility.HtmlEncode(GetAsm(script))));
            }
            else
                Execute(script);
        }

        private static string GetAsm(string script)
        {
            var engine = new Motor();
            engine.Compilar(script);

            return engine.RuntimeContext.Runnable.InstrucStream.ToString();
        }

        private static void Execute(string script)
        {
            var engine = new Motor();

            engine.Instalar(new WebModulo());
            engine.Instalar(new HtmlModulo());

            var modules = RetrieveModules();

            foreach (var module in modules)
                engine.Instalar(module);

            engine.Compilar(script);

            engine.Executar("page", new Infra(HttpContext.Current));
        }
    }
}