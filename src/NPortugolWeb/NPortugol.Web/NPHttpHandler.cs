using System;
using System.Web;
using NPortugol.Web.App.Asm;
using NPortugol.Web.App.Classic;
using NPortugol.Web.App.Mvc;

namespace NPortugol.Web
{
    public enum AppType
    {
        Asm,
        Classic,
        MVC
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
            }
        }

        private static AppType FigureOutAppType(string path)
        {
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