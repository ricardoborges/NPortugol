using System.Collections.Generic;
using NPortugol.Runtime.Interop;

namespace NPortugol.Web.Core
{
    public interface IContainer
    {
        void Install(IModule instance);

        List<IModule> Modules { get; }
    }

    public class DefaultContainer : IContainer
    {
        private readonly List<IModule> _list;

        public DefaultContainer()
        {
            _list =  new List<IModule>();
        }

        public void Install(IModule instance)
        {
            _list.Add(instance);
        }

        public List<IModule> Modules
        {
            get { return _list; }
        }
    }
}