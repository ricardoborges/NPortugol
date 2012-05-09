using System;
using System.Text;

namespace NPortugol.Runtime
{
    public class Symbol
    {
        public string Name { get; set; }

        public string Function { get; set; }

        public object Value { get; set; }

        public Type Type
        {
            get
            {
                return Value.GetType();
            }
        }

        public override string ToString()
        {
            if (Value == null) return string.Empty;

            var sb = new StringBuilder();

            if (Value.GetType() == typeof(object[]))
            {
                sb.Append("[");

                foreach (var obj in (object[])Value)
                {
                    if (sb.Length == 1)
                        sb.Append(obj);
                    else
                        sb.Append("," + obj);
                }

                sb.Append("]");
            }
            else
                return Value.ToString();

            return sb.ToString();
        }
    }
}