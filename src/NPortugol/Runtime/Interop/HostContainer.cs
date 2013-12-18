using System;
using System.Collections.Generic;

namespace NPortugol.Runtime.Interop
{
    public class HostContainer: IHostContainer
    {
        private readonly Dictionary<string, IHostFunction> hostTable;
        private readonly Dictionary<string, Func<object[], object>> handlerTable;
        private readonly Dictionary<string, bool> voidmap;

        public HostContainer()
        {
            hostTable = new Dictionary<string, IHostFunction>();
            handlerTable = new Dictionary<string, Func<object[], object>>();
            voidmap = new Dictionary<string, bool>();
        }

        public void Register(string name, IHostFunction hostFunction, bool isVoid)
        {
            if (hostTable.ContainsKey(name))
                throw new Exception("Função já registrada.");

            hostTable[name] = hostFunction;
            voidmap[name] = isVoid;
        }

        public IHostFunction Resolve(string name)
        {
            return !hostTable.ContainsKey(name) ? null : hostTable[name];
        }

        public Func<object[], object> ResolveHandler(string name)
        {
            return !handlerTable.ContainsKey(name)? null : handlerTable[name];
        }

        public void Register(string name, Func<object[], object> handler, bool isVoid)
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