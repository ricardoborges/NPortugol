using System;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NUnit.Framework;

namespace NPortugol.Tests
{
    [TestFixture]
    public class BaseTest
    {
        public CommonTree BuildAST(string script)
        {
            var input = new ANTLRStringStream(script);
            var lexer = new NPortugolLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new NPortugolParser(tokens);
            var ast = parser.script();

            return (CommonTree) ast.Tree;
        }
    }
}