using System.Collections.Generic;
using System.Linq;
using NPortugol.Runtime;

namespace NPortugol.Web.App.Mvc
{
    public static class SymbolTableExtensions
    {
        public static SymbolTable Refactor(this SymbolTable table, string function)
        {
            var st = new SymbolTable(null);

            foreach (var item in table)
            {
                var parts = item.Key.Split('_');

                if (int.Parse(parts[2]) == 0)
                    st.Add(SymbolTable.BuildSymbolId(function, parts[1],
                                                     int.Parse(parts[2])), 
                           new Symbol
                               {
                                   Function = function,
                                   Name = parts[1],
                                   Value = item.Value
                               });
            }

            return st;
        }
    }

    public static class EngineExtensions
    {
        public static void DataBind(this Engine engine, Route route)
        {
            var list = new Dictionary<string, object>();

            var function = engine.RuntimeContext.Runnable.FunctionTable[route.Action];

            var instructions = engine.RuntimeContext.Runnable.Instructions;

            for (int i = function.EntryPoint; i < instructions.Length; i++)
            {
                var inst = instructions[i];

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
    }
}