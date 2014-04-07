using System;
using System.Collections.Generic;
using NPortugol.Runtime.Interop;

namespace Contatos
{
    public static class DataBase
    {
        private static readonly List<string> _list = new List<string>();

        public static void Inserir(object[] obj)
        {
            var item = string.Format("{0} {1}", obj[0], obj[1]);

            _list.Add(item);
        }

        public static object[] Listar()
        {
            return _list.ToArray();
        }

        public static int Total
        {
            get { return _list.Count; }
        }
    }

    public class Dados: IModule
    {
        private readonly Dictionary<string, Func<object[], object>> _functions 
            = new Dictionary<string, Func<object[], object>>();

        public Dados()
        {
            _functions.Add("salvar", Salvar);
            _functions.Add("listar", Listar);
            _functions.Add("total", Total);
        }

        private object Salvar(object[] arg)
        {
            DataBase.Inserir(arg);

            return null;
        }
        
        private object[] Listar(object[] arg)
        {
            return DataBase.Listar();
        }

        private object Total(object[] arg)
        {
            return DataBase.Total - 1;
        }

        public Dictionary<string, Func<object[], object>> Functions
        {
            get { return _functions; }
        }
    }
}