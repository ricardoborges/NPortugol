using System;
using System.Web;

namespace NPortugol.Web.Mvc
{
    public class Route
    {
        public Route(string area, string action, HttpContext context)
        {
            Area = area;
            Action = action;
            Context = context;
        }

        public string Area { get; set; }

        public string Action { get; set; }

        public string Controller
        {
            get
            {
                var appPath = Context.Request.PhysicalApplicationPath;

                return string.Format(@"{0}{1}\controle\{2}.np", appPath, Area, Action);
            }
        }

        public HttpContext Context { get; set; }

        public string View 
        {
            get
            {
                var appPath = Context.Request.PhysicalApplicationPath;

                return string.Format(@"{0}{1}\visao\{2}.html", appPath, Area, Action);
            }
        }
    }

    public class Router
    {
        public Route FigureOut(HttpContext context)
        {
            var parts = context.Request.Url.LocalPath.Split('/');

            return new Route(parts[1], parts[2], context);
        }
    }
}