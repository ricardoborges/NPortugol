using System;
using System.Diagnostics;
using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using GrammarIDE.Core;
using GrammarIDE.Views.Ast;
using NPortugol;

namespace GrammarIDE.Presenters
{
    public interface IAstPresenter
    {
        IAstView AstView { get; set; }

        Config Config { get; }

        IMainPresenter MainPresenter { get; set; }

        void Parse(string text);
    }

    public class AstPresenter : IAstPresenter
    {
        private IConfigRepo configRepo;

        public AstPresenter(IConfigRepo configRepo, IMainPresenter mainPresenter)
        {
            this.configRepo = configRepo;
            this.MainPresenter = mainPresenter;
        }

        public IAstView AstView { get; set; }

        public Config Config
        {
            get { return configRepo.GetConfig(); }
        }

        public IMainPresenter MainPresenter { get; set; }

        public void Parse(string script)
        {
            NPortugolParser parser = null;

            try
            {
                configRepo.SaveScript(script);

                MainPresenter.MainView.ClearOutput();

                var input = new ANTLRStringStream(script);
                var lexer = new NPortugolLexer(input);
                var tokens = new CommonTokenStream(lexer);
                parser = new NPortugolParser(tokens);

                var r = parser.script();

                var t = (CommonTree)r.Tree;

                CreateASTGraph(t);
            }
            catch (RecognitionException ex)
            {
                var message = parser.GetErrorMessage(ex);

                MainPresenter.Error(message);
            }
            catch (Exception ex)
            {
                throw new Exception((string.Format("Putz... Erro não identificado :( [{0}]", ex.Message)));
            }
        }

        private void CreateASTGraph(CommonTree commonTree)
        {
            if (string.IsNullOrEmpty(Config.DotPath)) return;

            var dotpath = string.Format("{0}\\dot.exe", Config.DotPath);

            var gen = new DotTreeGenerator();
            var dotgraph = gen.ToDot(commonTree);

            dotgraph = new DotGraphStyle { FontSize = AstView.FontSize }.Apply(dotgraph);

            var writer = new StreamWriter("graph.dot");
            writer.Write(dotgraph);
            writer.Close();

            if (File.Exists("graph.gif")) File.Delete("graph.gif");

            var psi = new ProcessStartInfo(dotpath, "-Tgif graph.dot -o graph.gif");
            var p = new Process { StartInfo = psi };
            p.Start();
            p.WaitForExit();

            AstView.LoadAstImage("graph.gif");
        }
    }
}