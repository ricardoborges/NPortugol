using System.Collections.Generic;
using NPortugol.Runtime.Interop;

namespace NPortugol.Web
{
    public interface IContainer
    {
        void Install(IModulo instance);

        List<IModulo> Modules { get; }
    }

    public class DefaultContainer : IContainer
    {
        private readonly List<IModulo> _list;

        public DefaultContainer()
        {
            _list =  new List<IModulo>();
        }

        public void Install(IModulo instance)
        {
            _list.Add(instance);
        }

        public List<IModulo> Modules
        {
            get { return _list; }
        }
    }
}