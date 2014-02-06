using System.Web;
using NPortugol.Web.Core;

namespace NPortugol.Web
{
    public class NPHttpHandler: IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (!context.Request.Path.Contains(".np")) return;

            new DefaultAppLayer().Process(context);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}