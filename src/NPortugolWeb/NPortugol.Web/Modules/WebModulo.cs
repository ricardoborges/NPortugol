using System;
using System.Collections.Generic;
using System.Web;
using NPortugol.Runtime.Interop;
using NPortugol.Web.App.Mvc;

namespace NPortugol.Web.Modules
{
    public class WebModulo : IModulo
    {
        private readonly Dictionary<string, Func<object[], object>> _handlers;

        public Dictionary<string, Func<object[], object>> Functions
        {
            get { return _handlers; }
        }

        public WebModulo()
        {
            _handlers = new Dictionary<string, Func<object[], object>>
                           {
                               {"escreva", Escreva},
                               {"param", Param},
                               {"url", Url},
                               {"postado", Postado}
                           };
        }

        public static object Postado(params object[] parameters)
        {
            return HttpContext.Current.Request.HttpMethod.ToLower() == "post";
        }

        public static object Url(params object[] parameters)
        {
            var area = parameters[0].ToString();
            var controller = parameters[1].ToString();
            var action = parameters.Length < 3? Route.Default: parameters[2].ToString();

            var url = new Route(area, controller, action, HttpContext.Current).Url;

            return url;
        }

        private static object Escreva(params object[] parameters)
        {
            foreach (var parameter in parameters)
                HttpContext.Current.Response.Write(parameter);

            return null;
        }

        private static object Param(params object[] parameters)
        {
            //foreach (var parameter in parameters)
            //    HttpContext.Current.Response.Write(HttpContext.Current.Request[parameter.ToString()]);

            return HttpContext.Current.Request[parameters[0].ToString()];
        }
    }
}