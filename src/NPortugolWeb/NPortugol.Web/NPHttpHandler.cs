using System.Web;
using NPortugol.Web.Core;
using NPortugol.Web.Mvc;

namespace NPortugol.Web
{
    public class NPHttpHandler: IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Path.Contains(".np"))
                new DefaultAppLayer().Process(context);
            else 
                new MvcLayer().Process(context);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}