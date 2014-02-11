using System.Web;

namespace NPortugol.Web.App.Mvc
{
    public class Route
    {
        public static string Default = "indice";

        public Route(HttpContext context)
        {
            var parts = context.Request.Url.AbsolutePath.Split('/');

            Area = parts[1];
            Controller = parts[2];
            Action = parts.Length < 4? Default: parts[3];
            Context = context;
            View = Action;
        }

        public Route(string area, string controller, string action, HttpContext context)
        {
            Area = area;
            Controller = controller;
            Action = action;
            Context = context;
            View = Action;
        }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string View { get; set; }

        public string ControllerPhysicalPath
        {
            get
            {
                var appPath = Context.Request.PhysicalApplicationPath;

                return string.Format(@"{0}{1}\controle\{2}.np", appPath, Area, Controller);
            }
        }

        public HttpContext Context { get; set; }

        public string ViewPhysicalPath 
        {
            get
            {
                var appPath = Context.Request.PhysicalApplicationPath;

                return string.Format(@"{0}{1}\visao\{2}.html", appPath, Area, View);
            }
        }

        public string Url
        {
            get
            {
                return string.Format("http://{0}/{1}/{2}/{3}", Context.Request.Url.Authority, Area, Controller, Action);
            }
        }
    }
}