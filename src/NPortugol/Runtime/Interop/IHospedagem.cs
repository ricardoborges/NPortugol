using System;

namespace NPortugol.Runtime.Interop
{
    public interface IHospedagem
    {
        void Registrar(string name, IFuncaoHospedada funcaoHospedada, bool isVoid);

        IFuncaoHospedada Resolve(string name);

        Func<object[], object> ResolveHandler(string name);

        void Registrar(string name, Func<object[], object> handler, bool isVoid);
        
        bool IsRegistered(string name);

        bool IsVoid(string name);
    }
}