using System;
using System.Collections.Generic;
using System.Web;
using NPortugol.Runtime.Interop;

namespace NPortugol.Web.Modules
{
    public class HtmlModulo : IModulo
    {
        private readonly Dictionary<string, Func<object[], object>> _handlers;

        public Dictionary<string, Func<object[], object>> Functions
        {
            get { return _handlers; }
        }

        public HtmlModulo()
        {
            _handlers = new Dictionary<string, Func<object[], object>>
                           {
                               {"combo", Combo},
                               {"campo", Campo}
                           };
        }

        private static object Campo(params object[] parameters)
        {
            var tmpl = string.Format("<input type='text' id='{0}' name='{0}' value='{1}'>",
                parameters[0], parameters[1]);

            HttpContext.Current.Response.Write(tmpl);

            return null;
        }

        private static object Combo(params object[] parameters)
        {
            HttpContext.Current.Response.Write("<select>");

            foreach (var parameter in parameters)
                HttpContext.Current.Response.Write(string.Format("<option value='{0}'>{0}</option>", parameter));


            HttpContext.Current.Response.Write("</select>");
            return null;
        }
    }
}