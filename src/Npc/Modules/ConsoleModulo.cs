using System;
using System.Collections.Generic;
using NPortugol.Runtime.Interop;

namespace Npc.Modules
{
    public class ConsoleModulo: IModulo
    {
        private readonly Dictionary<string, Func<object[], object>> handlers;

        public ConsoleModulo()
        {
            handlers = new Dictionary<string, Func<object[], object>>
                           {
                               {"imprima", Imprima},
                               {"imprimaLinha", ImprimaLinha},
                               {"leia", Leia}
                           };
        }

        public Dictionary<string, Func<object[], object>> Functions
        {
            get { return handlers; }
        }

        private static object Imprima(object[] args)
        {
            if (args.Length == 0)
                throw new Exception("Nenhum parametro encontrado para vetorizar.");

            foreach (var obj in args)
            {
                Console.Write(obj);
            }

            return null;
        }

        private static object ImprimaLinha(object[] args)
        {
            if (args.Length == 0)
                throw new Exception("Nenhum parametro encontrado para vetorizar.");

            foreach (var obj in args)
            {
                Console.WriteLine(obj);
            }

            return null;
        }

        private static object Leia(object[] args)
        {
            return Console.ReadLine();
        }
    }
}