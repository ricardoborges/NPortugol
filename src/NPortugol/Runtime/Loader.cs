using System.Collections.Generic;
using NPortugol.Runtime.Asm;

namespace NPortugol.Runtime
{
    public class Loader: ILoader
    {
        private readonly IRunnableBuilder rBuilder;

        public Loader(IRunnableBuilder rBuilder)
        {
            this.rBuilder = rBuilder;
        }

        public Runnable Load(IList<string> lines)
        {
            rBuilder.Load(lines);

            return rBuilder.Build();
        }

        public Runnable Load(string script)
        {
            rBuilder.Load(script);

            return rBuilder.Build();
        }
    }
}