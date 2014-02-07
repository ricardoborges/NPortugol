using System;
using System.Collections.Generic;
using System.Linq;
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
            
           DataBind(engine, route);

            engine.Execute(route.Action, null);

            return engine.RuntimeContext.Runnable.ScriptSymbolTable;
        }

        private static void DataBind(Engine engine, Route route)
        {
            var list = new Dictionary<string, object>();

            var instructions = engine.RuntimeContext.Runnable.Instructions;

            foreach (var inst in instructions)
            {
                if (inst.OpCode == OpCode.DCL)
                {
                    var value = route.Context.Request[inst.Operands[0].Value.ToString()] ?? string.Empty;

                    list.Add(inst.Operands[0].Value.ToString(), value);
                }
                
                if (inst.OpCode == OpCode.EMP) break;
            }

            foreach (var keypar in list.Reverse())
            {
                var op = new Operand(OperandType.Literal, keypar.Value);

                engine.RuntimeContext.Runnable.ParamStack.Push(op);        
            }
        }

        private void ExecuteView(Route route, SymbolTable model)
        {
            var script = new DefaultScriptBuilder().Parse(ExtractContent(route.View), route.Action);

            var engine = CreateEngine(script, model);

            engine.Execute(route.Action, new Infra(HttpContext.Current));
        }
    }
}