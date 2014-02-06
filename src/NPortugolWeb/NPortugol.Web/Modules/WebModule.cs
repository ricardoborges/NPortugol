using System;
using System.Collections.Generic;
using System.Web;
using NPortugol.Runtime.Interop;

namespace NPortugol.Web.Modules
{
    public class WebModule : IModule
    {
        private readonly Dictionary<string, Func<object[], object>> _handlers;

        public Dictionary<string, Func<object[], object>> Functions
        {
            get { return _handlers; }
        }

        public WebModule()
        {
            _handlers = new Dictionary<string, Func<object[], object>>
                           {
                               {"escreva", Escreva},
                               {"param", Param}
                           };
        }

        private static object Escreva(params object[] parameters)
        {
            foreach (var parameter in parameters)
                HttpContext.Current.Response.Write(parameter);

            return null;
        }

        private static object Param(params object[] parameters)
        {
            foreach (var parameter in parameters)
                HttpContext.Current.Response.Write(HttpContext.Current.Request[parameter.ToString()]);

            return null;
        }
    }
}