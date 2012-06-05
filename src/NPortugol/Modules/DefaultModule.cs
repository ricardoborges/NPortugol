using System;
using System.Collections.Generic;
using NPortugol.Runtime.Interop;

namespace NPortugol.Modules
{
    public class DefaultModule: IModule
    {
        private readonly Dictionary<string, Func<object[], object>> handlers;

        public DefaultModule()
        {
            handlers = new Dictionary<string, Func<object[], object>>
                           {
                               {"vetorizar", Vetorizar}
                           };
        }

        public Dictionary<string, Func<object[], object>> Functions
        {
            get { return handlers; }
        }

        private static object Vetorizar(object[] args)
        {
            if (args.Length == 0)
                throw new Exception("Nenhum parametro encontrado para vetorizar.");

            var value = args[0].ToString();
            var split = args[1].ToString()[0];

            return value.Split(split);
        }
    }
}