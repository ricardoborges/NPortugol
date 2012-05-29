using System.Collections.Generic;
using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol.Runtime;

namespace NPortugol
{
    public class Npc : ICompiler
    {
        public bool DebugInfo { get; set; }

        public Dictionary<int, int> SourceMap { get; set; }

        public IList<string> FunctionNames { get; set; }

        public IList<string> Compile(string script)
        {
            var input = new ANTLRStringStream(script);
            var lexer = new NPortugolLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new NPortugolParser(tokens);

            var ast = parser.script();

            FunctionNames = parser.Functions;

            var tree = (CommonTree)ast.Tree;

            var nodes = new CommonTreeNodeStream(tree) { TokenStream = tokens };

            var walker = new NPortugolWalker(nodes) { DebugInfo = DebugInfo };

            var asm = walker.script();

            SourceMap = walker.SourceMap;

            EnsureFunction(asm, parser.Functions.Count == 0);

            return asm;
        }

        private void EnsureFunction(IList<string> asm, bool create)
        {
            if (!create) return;

            asm.Insert(0, "main:");
            asm.Add("EXIT");
        }

        public IList<string> CompileFile(string scriptfile)
        {
            using (var reader = new StreamReader(scriptfile))
            {
                var file = reader.ReadToEnd();

                return Compile(file);
            }
        }
    }
}