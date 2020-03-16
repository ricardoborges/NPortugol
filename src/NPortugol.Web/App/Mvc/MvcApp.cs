using System;
using System.Web;
using NPortugol.Runtime;
using NPortugol.Web.Script;

namespace NPortugol.Web.App.Mvc
{
    public class MvcApp: BaseApp
    {
        public override void ProcessRequest(HttpContext context)
        {
            if (context.Request.Path == "/favicon.ico") return;

            if (context.Request.Url.AbsolutePath.Length == 1) return;

            var route = new Route(context);

            Execute(route);
        }

        private void Execute(Route route)
        {
            var model = ExecuteAction(route);

            ExecuteView(route, model);
        }

        private SymbolTable ExecuteAction(Route route)
        {
            var script = ExtractContent(route.ControllerPhysicalPath);

            var engine = CreateEngine(script);

            engine.DataBind(route);

            engine.Executar(route.Action, null);

            return engine.RuntimeContext.Runnable.ScriptSymbolTable;
        }

        private void ExecuteView(Route route, SymbolTable model)
        {
            var fviewname = "page";

            var controllerScript = ExtractContent(route.ControllerPhysicalPath);

            route.View = ResolveView(route, model);

            var script = new DefaultScriptBuilder().Parse(ExtractContent(route.ViewPhysicalPath), fviewname)
                         + " " + controllerScript;

            var engine = CreateEngine(script, model.Refactor(fviewname));

            engine.Executar("page", new Infra(HttpContext.Current));
        }

        public string ResolveView(Route route, SymbolTable model)
        {
            var viewKey = string.Format("{0}_visao_0", route.Action);

            return model.ContainsKey(viewKey) ? model[viewKey].ToString(): route.Action;
        }
    }
}