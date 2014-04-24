using System;
using System.Collections.Generic;
using System.Linq;
using NPortugol.Runtime.Interop;

namespace NPortugol.Modulos
{
    public class ModuloPadrao: IModulo
    {
        private readonly Dictionary<string, Func<object[], object>> handlers;

        public ModuloPadrao()
        {
            handlers = new Dictionary<string, Func<object[], object>>
                           {
                               {"vetorizar", Vetorizar},
                               {"data", Data},
                               {"hoje", Hoje},
                               {"nomedia", NomeDoDia}
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

            if (args.Length == 2)
            {
                var split = args[1].ToString()[0];

                return value.Split(split);
            }

            return value.ToCharArray().Cast<object>().ToArray();
        }

        private static object Data(object[] parameters)
        {
            var parameter = parameters[0].ToString();

            return DateTime.Parse(parameter);
        }

        private static object Hoje(object[] parameters)
        {
            return DateTime.Today.ToShortDateString();
        }

        private static object NomeDoDia(object[] parameters)
        {
            var parameter = Convert.ToDateTime(parameters[0]);

            var name = string.Empty;

            switch (parameter.DayOfWeek)
            {
                case DayOfWeek.Sunday: name = "Domingo"; break;
                case DayOfWeek.Monday: name = "Segunda"; break;
                case DayOfWeek.Tuesday: name = "Ter�a"; break;
                case DayOfWeek.Wednesday: name = "Quarta"; break;
                case DayOfWeek.Thursday: name = "Quinta"; break;
                case DayOfWeek.Friday: name = "Sexta"; break;
                case DayOfWeek.Saturday: name = "S�bado"; break;
            }

            return name;
        }
    }
}