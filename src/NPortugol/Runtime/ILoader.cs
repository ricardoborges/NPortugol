using System.Collections.Generic;

namespace NPortugol.Runtime
{
    public interface ILoader
    {
        Runnable Load(IList<string> lines);

        Runnable Load(string script);
    }
}