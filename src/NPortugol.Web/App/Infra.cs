using System;
using System.Web;

namespace NPortugol.Web.App
{
    public class Infra
    {
        private readonly HttpContext _context;

        public Infra(HttpContext context)
        {
            _context = context;
        }

        public string url
        {
            get { return _context.Request.Url.AbsoluteUri; }
        }

        public string data
        {
            get { return DateTime.Now.ToShortDateString(); }
        }
    }
}