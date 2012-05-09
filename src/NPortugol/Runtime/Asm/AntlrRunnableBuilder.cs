using System.Collections.Generic;
using System.Text;
using Antlr.Runtime;

namespace NPortugol.Runtime.Asm
{
    public class AntlrRunnableBuilder: IRunnableBuilder
    {
        private ASMParser parser;

        public bool DebugInfo { get; set; }

        public void Load(string script)
        {
            var input = new ANTLRStringStream(script);
            var lexer = new ASMLexer(input);
            var tokens = new CommonTokenStream(lexer);
            parser = new ASMParser(tokens) {DebugInfo = DebugInfo};
        }

        public void Load(IEnumerable<string> lines)
        {
            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                sb.Append(line+"\r\n");
            }

            Load(sb.ToString());
        }

        public Runnable Build()
        {
            var list = new List<Instruction>();

            var instructions = parser.script();

            if (instructions != null)
                list.AddRange(instructions);
            
            return new Runnable(new InstrucStream(list.ToArray()), parser._functionTable, new SymbolTable(null));
        }
    }
}