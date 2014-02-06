using System.Web;

namespace NPortugol.Web.Core
{
    public interface IAppLayer
    {
        void Process(HttpContext context);
    }
}