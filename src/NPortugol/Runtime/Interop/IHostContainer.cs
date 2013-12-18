using System;

namespace NPortugol.Runtime.Interop
{
    public interface IHostContainer
    {
        void Register(string name, IHostFunction hostFunction, bool isVoid);

        IHostFunction Resolve(string name);

        Func<object[], object> ResolveHandler(string name);

        void Register(string name, Func<object[], object> handler, bool isVoid);
        
        bool IsRegistered(string name);

        bool IsVoid(string name);
    }
}