using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol.Runtime;

namespace NPortugol
{
    public class Npc : ICompilador
    {
        public bool DebugInfo { get; set; }

        public Bytecode Compilar(string script)
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

        public Bytecode CompilarArquivo(string scriptfile)
        {
            using (var reader = new StreamReader(scriptfile))
            {
                var file = reader.ReadToEnd();

                return Compilar(file);
            }
        }

        public void SalvarEmDisco(string filename)
        {
            var bytecode = CompilarArquivo(filename);

            new BytecodeSerializer().Create(ExtrairNome(filename), bytecode);
        }

        public Bytecode LerNoDisco(string filename)
        {
            return new BytecodeSerializer().Read(filename);
        }

        private static string ExtrairNome(string filename)
        {
            var parts = filename.Split('\\');

            var cname = parts[parts.Length - 1];

            var name = cname.Split('.')[0];

            return string.Format("{0}.{1}", name, "npx");
        }
    }
}