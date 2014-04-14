using System;
using System.Collections.Generic;

namespace NPortugol.Runtime.Interop
{
    public interface IModulo
    {
        Dictionary<string, Func<object[], object>> Functions { get; }
    }
}