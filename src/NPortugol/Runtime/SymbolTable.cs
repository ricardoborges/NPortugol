using System.Collections.Generic;
using NPortugol.Runtime.Exceptions;

namespace NPortugol.Runtime
{
    public class SymbolTable: Dictionary<string, object>
    {
        public SymbolTable(SymbolTable parent)
        {
            if (parent == null) return;

            foreach (var item in parent)
            {
                Add(item.Key, item.Value);
            }
        }

        public void EnsureExists(string name)
        {
            if (!ContainsKey(name))
            {
                new Ops().ThrowNonDeclared(name);
            }
        }

        public static SymbolTable CreateFor(SymbolTable parent)
        {
            return new SymbolTable(parent);
        }

        public static string BuildSymbolName(string function, string name, int index)
        {
            return string.Format("{0}_{1}_{2}", function, name, index).ToLower();
        }

        public object[] GetAsArray(string name)
        {
            var variable = this[name];

            if (variable == null || variable.GetType() != typeof(object[]))
                variable = new[] { variable };

            return (object[]) variable;
        }
    }
}