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

        public object SetSymbolValue(string name, string id, string function, object value, int? index)
        {
            if (!ContainsKey(id))
                this[id] = new Symbol {Name = name, Value = value, Function = function};

            if (index != null)
            {
                var array = GetAsArray(id);

                if (index.Value >= array.Length)
                    Array.Resize(ref array, index.Value + 1);

                array[index.Value] = value;

                this[id].Value = array;

                return value;
            }

            return this[id].Value = value;
        }

        public object[] GetAsArray(string id)
        {
            var variable = this[id];

            if (variable == null || variable.Value == null)
            {
                variable.Value = new object[1];
                return (object[]) variable.Value;
            }  

            if (variable.Value.GetType() != typeof(object[]))
                variable.Value = new[] { variable };

            return (object[]) variable.Value;
        }
    }
}