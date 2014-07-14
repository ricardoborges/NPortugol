using System;
using System.Collections.Generic;
using System.Text;
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
                               {"imprimaVetor", ImprimaVetor},
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

        private object ImprimaVetor(object[] parameters)
        {
            var list = parameters[0] as object[];

            if (list == null) return string.Empty;

            var sb = new StringBuilder();

            sb.Append("{");

            foreach (var item in list)
            {
                if (sb.Length == 1)
                    sb.Append(item);
                else
                    sb.Append(", " + item);
            }

            sb.Append("}");

            Console.WriteLine(sb.ToString());

            return null;
        }

        private static object Leia(object[] args)
        {
            Console.WriteLine("Informe a entrada: ");
            return Console.ReadLine();
        }
    }
}