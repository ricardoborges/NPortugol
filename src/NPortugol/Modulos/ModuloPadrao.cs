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
            if (parameters.Length == 0)
                throw new Exception("A função data requer o parametro com valor a ser convertido. Ex: data('01/01/2016')");

            return EnsureValidDate(parameters);
        }

        private static object Hoje(object[] parameters)
        {
            return DateTime.Today.ToShortDateString();
        }

        private static object NomeDoDia(object[] parameters)
        {
            if (parameters.Length == 0)
                throw new Exception("A função nomedia requer um valor do tipo data. Ex: nomedia(data('01/01/2016'))");

            var date = EnsureValidDate(parameters);

            var name = string.Empty;

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday: name = "Domingo"; break;
                case DayOfWeek.Monday: name = "Segunda"; break;
                case DayOfWeek.Tuesday: name = "Terça"; break;
                case DayOfWeek.Wednesday: name = "Quarta"; break;
                case DayOfWeek.Thursday: name = "Quinta"; break;
                case DayOfWeek.Friday: name = "Sexta"; break;
                case DayOfWeek.Saturday: name = "Sábado"; break;
            }

            return name;
        }

        private static DateTime EnsureValidDate(object[] parameters)
        {
            DateTime data;

            DateTime.TryParse(parameters[0].ToString(), out data);

            if (data < new DateTime(2, 1, 1))
                throw new Exception($"Não foi possível converter '{parameters[0]}' em uma data válida.");

            return data;
        }
    }
}