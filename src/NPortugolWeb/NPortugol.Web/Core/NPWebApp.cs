using System.Web;

namespace NPortugol.Web.Core
{
    public interface INPortugolWebApp
    {
        IContainer Container { get; set; }
    }

    public abstract class NPWebApp : HttpApplication, INPortugolWebApp
    {
        protected NPWebApp()
        {
            _container = new DefaultContainer();

            InstalarModulos();
        }

        public abstract void InstalarModulos();

        private static IContainer _container;

        public IContainer Container
        {
            get { return _container; }
            set { _container = value; }
        }
    }
}