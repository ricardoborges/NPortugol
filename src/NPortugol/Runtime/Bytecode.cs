using System;
using System.Collections.Generic;

namespace NPortugol.Runtime
{
    [Serializable]
    public class Bytecode
    {
        private readonly IList<string> script;
        private readonly IList<string> functions;
        private readonly Dictionary<int, int> sourceMap;

        public Bytecode(IList<string> script)
        {
            this.script = script;
        }

        public Bytecode(IList<string> script, IList<string> functions)
        {
            this.script = script;
            this.functions = functions;
        }

        public Bytecode(IList<string> script, IList<string> functions, Dictionary<int, int> sourceMap)
        {
            this.script = script;
            this.functions = functions;
            this.sourceMap = sourceMap;
        }

        public IList<string> Script
        {
            get{ return script;}
        }

        public IList<string> FunctionNames
        {
            get { return functions; }
        }

        public Dictionary<int, int> SourceMap
        {
            get{ return sourceMap;}
        }
    }
}