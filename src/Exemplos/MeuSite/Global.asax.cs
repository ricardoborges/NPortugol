using MeuSite.Modulos;
using NPortugol.Web.App;

namespace MeuSite
{
    public class Global : NPWebApp
    {
        public override void InstalarModulos()
        {
            Container.Install(new Utilidades());
        }
    }
}