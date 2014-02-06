using System;
using System.Collections.Generic;
using System.Web;
using NPortugol.Runtime.Interop;

namespace MeuSite.Modulos
{
    public class Utilidades: IModule
    {
        private readonly Dictionary<string, Func<object[], object>> handlers;

        public Dictionary<string, Func<object[], object>> Functions
        {
            get { return handlers; }
        }

        public Utilidades()
        {
            handlers = new Dictionary<string, Func<object[], object>>
                           {
                               {"lista", Lista}
                           };
        }

        private static object Lista(params object[] parameters)
        {
            HttpContext.Current.Response.Write("<ul>");

            foreach (var parameter in parameters)
                HttpContext.Current.Response.Write(string.Format("<li>{0}</li>", parameter));

            HttpContext.Current.Response.Write("</ul>");

            return null;
        }
    }
}