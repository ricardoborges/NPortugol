using System;
using System.Web;
using NPortugol.Web.App;
using NPortugol.Web.App.Asm;
using NPortugol.Web.App.Classic;
using NPortugol.Web.App.Mvc;

namespace NPortugol.Web
{
    public enum AppType
    {
        Asm,
        Classic,
        MVC,
        CSS,
        IMG
    }

    public class NPHttpHandler: IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            switch (FigureOutAppType(context.Request.Path))
            {
                case AppType.Classic:
                    new ClassicApp().ProcessRequest(context);
                    break;
                case AppType.MVC:
                    new MvcApp().ProcessRequest(context);
                    break;
                case AppType.Asm:
                    new AsmApp().ProcessRequest(context);
                    break;
                case AppType.CSS:
                    HttpContext.Current.Response.WriteFile(context.Request.Path);
                    break;
            }
        }

        private static AppType FigureOutAppType(string path)
        {
            if (path.EndsWith(".css"))
                return AppType.CSS;

            if (path.EndsWith(".png") || path.EndsWith(".jpg") || path.EndsWith(".gif"))
                return AppType.IMG;

            return path.Contains(".np")
                       ? AppType.Classic
                       : (path.Contains(".asm")
                              ? AppType.Asm
                              : AppType.MVC);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}