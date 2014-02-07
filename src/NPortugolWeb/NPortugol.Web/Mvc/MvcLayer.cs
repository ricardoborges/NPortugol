using System;
using System.Collections.Generic;
using System.Web;
using NPortugol.Runtime;
using NPortugol.Web.Core;
using NPortugol.Web.Modules;
using NPortugol.Web.Script;

namespace NPortugol.Web.Mvc
{
    public class MvcLayer: BaseLayer, IAppLayer
    {
        public void Process(HttpContext context)
        {
            if (context.Request.Path == "/favicon.ico") return;

            var route = new Router().FigureOut(context);

            Execute(route);
        }

        private void Execute(Route route)
        {
            var model = ExecuteAction(route);

            ExecuteView(route, model);
        }

        private SymbolTable ExecuteAction(Route route)
        {
            var script = ExtractContent(route.Controller);

            var engine = CreateEngine(script);

            engine.Execute(route.Action, null);

            return engine.RuntimeContext.Runnable.ScriptSymbolTable;
        }

        private void ExecuteView(Route route, SymbolTable model)
        {
            var script = new DefaultScriptBuilder().Parse(ExtractContent(route.View), route.Action);

            var engine = CreateEngine(script, model);

            engine.Execute(route.Action, new Infra(HttpContext.Current));
        }
    }
}