using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol.Runtime;

namespace NPortugol
{
    public class Npc : ICompiler
    {
        public bool DebugInfo { get; set; }

        public Bytecode Compile(string script)
        {
            var input  = new ANTLRStringStream(script);
            var lexer  = new NPortugolLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new NPortugolParser(tokens);

            var ast = parser.script();

            var tree   = (CommonTree)ast.Tree;
            var nodes  = new CommonTreeNodeStream(tree) { TokenStream = tokens };
            var walker = new NPortugolWalker(nodes) { DebugInfo = DebugInfo };

            var asm = walker.script();

            return new Bytecode(asm, parser.Functions, walker.SourceMap);
        }

        public Bytecode CompileFile(string scriptfile)
        {
            using (var reader = new StreamReader(scriptfile))
            {
                var file = reader.ReadToEnd();

                return Compile(file);
            }
        }

        public void WriteToDisk(string filename)
        {
            var bytecode = CompileFile(filename);

            new BytecodeSerializer().Create(ExtractName(filename), bytecode);
        }

        public Bytecode ReadFromDisk(string filename)
        {
            return new BytecodeSerializer().Read(filename);
        }

        private static string ExtractName(string filename)
        {
            var parts = filename.Split('\\');

            var cname = parts[parts.Length - 1];

            var name = cname.Split('.')[0];

            return string.Format("{0}.{1}", name, "npx");
        }
    }
}