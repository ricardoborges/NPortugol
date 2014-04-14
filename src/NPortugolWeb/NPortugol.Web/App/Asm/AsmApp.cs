using System.Web;
using NPortugol.Runtime;
using NPortugol.Web.Modules;

namespace NPortugol.Web.App.Asm
{
    public class AsmApp : BaseApp
    {
        public override void ProcessRequest(HttpContext context)
        {
            if (context.Request.Path == "/favicon.ico") return;

            var content = ExtractContent(context.Request.PhysicalPath);

            Execute(content);
        }

        private static void Execute(string script)
        {
            var engine = new Motor();

            engine.Install(new WebModulo());
            engine.Install(new HtmlModulo());

            var modules = RetrieveModules();

            foreach (var module in modules)
                engine.Install(module);

            engine.Load(script);

            engine.Execute("page", new Infra(HttpContext.Current));
        }
    }
}