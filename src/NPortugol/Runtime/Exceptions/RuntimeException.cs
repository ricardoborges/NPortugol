using System;

namespace NPortugol.Runtime.Exceptions
{
    public class RuntimeException: Exception
    {
        public RuntimeException(string msg): base(msg)
        {
            
        }
    }
}