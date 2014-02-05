using System.Web;
using NPortugol.Runtime.Interop;

namespace NPortugol.Web
{
    public class EscrevaFunc : IHostFunction
    {
        public object Execute(params object[] parameters)
        {
            foreach (var parameter in parameters)
                HttpContext.Current.Response.Write(parameter);    

            return null;
        }

        public string Name
        {
            get { return "escreva"; }
        }
    }
}