using System;
using System.Collections.Generic;

namespace NPortugol.Runtime
{
    public class Function
    {
        public const string MainName = "main";

        public Function(string name, int entryPoint)
        {
            Name = name;
            EntryPoint = entryPoint;
        }

        public string Name { get; private set; }

        //[Obsolete]
        //public IList<string> Parameters { get; set; }

        public int EntryPoint { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}