using System;
using System.Collections.Generic;

namespace NPortugol.Runtime.Interop
{
    public interface IModule
    {
        Dictionary<string, Func<object[], object>> Functions { get; }
    }
}