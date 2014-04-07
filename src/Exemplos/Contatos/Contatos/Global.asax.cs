using NPortugol.Web.App;

namespace Contatos
{
    public class Global : NPWebApp
    {
        public override void InstalarModulos()
        {
            Container.Install(new Dados());
        }
    }
}