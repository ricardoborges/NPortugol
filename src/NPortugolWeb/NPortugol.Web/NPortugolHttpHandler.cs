using System;
using System.Web;

namespace NPortugol.Web
{
    public class NPortugolHttpHandler: IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (!context.Request.Path.Contains(".np")) return;

            new DefaultDispatcher().Process(context);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}