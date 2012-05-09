using System;
using System.Collections.Generic;
using NPortugol.Runtime.Exceptions;

namespace NPortugol.Runtime
{
    public class SymbolTable: Dictionary<string, Symbol>
    {
        public SymbolTable(SymbolTable parent)
        {
            if (parent == null) return;

            foreach (var item in parent)
            {
                Add(item.Key, item.Value);
            }
        }

        public void EnsureExists(string id, string name)
        {
            if (!ContainsKey(id))
            {
                new Ops().ThrowNonDeclared(name);
            }
        }

        public static SymbolTable CreateFor(SymbolTable parent)
        {
            return new SymbolTable(parent);
        }

        public static string BuildSymbolId(string function, string name, int index)
        {
            return string.Format("{0}_{1}_{2}", function, name, index).ToLower();
        }

        public object SetSymbolValue(string name, string id, string function, object value, object index)
        {
            if (!ContainsKey(id))
                this[id] = new Symbol {Name = name, Value = value, Function = function};

            if (index != null)
            {
                int i = ResolveIndex(id, index);

                var array = GetAsArray(id);

                if (i >= array.Length)
                    Array.Resize(ref array, i + 1);

                array[i] = value;

                this[id].Value = array;

                return value;
            }

            return this[id].Value = value;
        }

        private int ResolveIndex(string id, object value)
        {
            int r;

            if (int.TryParse(value.ToString(), out r)) return r;

            var index = this[id];

            return int.Parse(index.Value.ToString());
        }

        public object[] GetAsArray(string id)
        {
            var variable = this[id];

            if (variable == null || variable.Value == null)
                variable.Value = new[] { variable };

            if (variable.Value.GetType() != typeof(object[]))
                variable.Value = new[] { variable };

            return (object[]) variable.Value;
        }
    }
}