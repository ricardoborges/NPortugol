using System;
using System.Collections.Generic;

namespace NPortugol.Runtime.Interop
{
    public class Hospedagem: IHospedagem
    {
        private readonly Dictionary<string, IFuncaoHospedada> hostTable;
        private readonly Dictionary<string, Func<object[], object>> handlerTable;
        private readonly Dictionary<string, bool> voidmap;

        public Hospedagem()
        {
            hostTable = new Dictionary<string, IFuncaoHospedada>();
            handlerTable = new Dictionary<string, Func<object[], object>>();
            voidmap = new Dictionary<string, bool>();
        }

        public void Registrar(string name, IFuncaoHospedada funcaoHospedada, bool isVoid)
        {
            if (hostTable.ContainsKey(name))
                throw new Exception("Função já registrada.");

            hostTable[name] = funcaoHospedada;
            voidmap[name] = isVoid;
        }

        public IFuncaoHospedada Resolve(string name)
        {
            return !hostTable.ContainsKey(name) ? null : hostTable[name];
        }

        public Func<object[], object> ResolveHandler(string name)
        {
            return !handlerTable.ContainsKey(name)? null : handlerTable[name];
        }

        public void Registrar(string name, Func<object[], object> handler, bool isVoid)
        {
            if (hostTable.ContainsKey(name) || handlerTable.ContainsKey(name))
                throw new Exception("Função já registrada.");

            handlerTable[name] = handler;
            voidmap[name] = isVoid;
        }

        public bool IsRegistered(string name)
        {
            return hostTable.ContainsKey(name) || handlerTable.ContainsKey(name);
        }

        public bool IsVoid(string name)
        {
            return voidmap[name];
        }
    }
}