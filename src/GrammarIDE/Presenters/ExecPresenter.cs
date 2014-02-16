using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using GrammarIDE.Core;
using GrammarIDE.Views.Exec;
using NPortugol;
using NPortugol.Runtime;

namespace GrammarIDE.Presenters
{
    public interface IExecPresenter
    {
        IExecView ExecView { get; set; }
        IMainPresenter MainPresenter { get; set; }

        Config Config { get; }
        
        bool Debugging { get; }

        void Build();
        void Run();
        void Debug();
        void Step();
        void Stop();
    }

    public class ExecPresenter : IExecPresenter
    {
        private Npc psc = new Npc();
        private Engine engine = new Engine {EnableGC = false};
        private readonly IConfigRepo configRepo;

        private Dictionary<int, int> sourceMap;

        public ExecPresenter(IConfigRepo configRepo, IMainPresenter mainPresenter)
        {
            this.configRepo = configRepo;
            this.MainPresenter = mainPresenter;

            BindFunctions();
        }

        #region Properties

        public IMainPresenter MainPresenter { get; set; }
        public IExecView ExecView { get; set; }

        public Config Config
        {
            get { return configRepo.GetConfig(); }
        }

        public bool Debugging
        {
            get { return engine.Debugging; }
        }

        #endregion

        #region Host Functions

        private void BindFunctions()
        {
            engine.HostContainer.Register("imprima", Imprima, true);
            engine.HostContainer.Register("imprimaVetor", ImprimaVetor, true );
            engine.HostContainer.Register("leia", Leia, false);
            engine.HostContainer.Register("tamanho", Tamanho, true);
        }

        private object Imprima(object[] parameters)
        {
            var value = parameters[0].ToString();

            MainPresenter.MainView.WriteOutput(value);

            return null;
        }

        private object ImprimaVetor(object[] parameters)
        {
            var list = parameters[0] as object[];

            if (list == null) return string.Empty;

            var sb = new StringBuilder();
            
            sb.Append("{");

            foreach (var item in list)
            {
                if (sb.Length == 1)
                    sb.Append(item);
                else
                    sb.Append(", " + item);
            }

            sb.Append("}");

            MainPresenter.MainView.WriteOutput(sb.ToString());

            return sb.ToString();
        }

        private object Tamanho(object[] parameters)
        {
            var parameter = parameters[0];

            return ((object[])parameter).Length;

        }

        private object Leia(object[] objects)
        {
            return Prompt.ShowDialog("Digite a entrada:", "Leia");
        }

        #endregion

        #region Actions

        public void Build()
        {
            configRepo.SaveScript(ExecView.Script.Text);

            ExecView.Asm.Text = string.Empty;

            psc.DebugInfo = true;

            var bytecode = psc.Compile(ExecView.Script.Text);

            ShowAsm(bytecode.Script);

            MainPresenter.MainView.WriteLine();
            MainPresenter.MainView.WriteOutput("Construído com sucesso");

            sourceMap = bytecode.SourceMap;
        }

        public void Run()
        {
            //Build();

            MainPresenter.MainView.ClearOutput();

            engine.Load(ExecView.Asm.Text);

            //MainPresenter.MainView.WriteOutput("Saída:");
            //MainPresenter.MainView.WriteLine();

            engine.Execute();

            Fill();

            //MainPresenter.MainView.WriteLine();
            //MainPresenter.MainView.WriteOutput("Executado com sucesso");
        }

        public void Debug()
        {
            Build();

            engine.Debug = true;

            engine.Load(ExecView.Asm.Text);

            MainPresenter.MainView.ClearOutput();

            engine.EnableGC = false;
        }

        public void Step()
        {
            engine.Execute();

            if (engine.RuntimeContext.Completed)
            {
                ClearLines();
                return;
            }

            SelectLine();

            Fill();
        }

        public void Stop()
        {
            engine.Debugging = false;
            ClearLines();
            ClearStacks();
        }

        #endregion

        #region IDE

        private void ShowAsm(IList<string> script)
        {
            if (script == null) return;

            foreach (var line in script)
            {
                ExecView.Asm.Text += line + "\r\n";
            }
        }

        private void Fill()
        {
            PopulateSymbols();
            PopulateStack();
            PopulateRunStack();
        }

        private void ClearStacks()
        {
            ExecView.Symbols.DataSource = null;
            ExecView.EvalStack.DataSource = null;
            ExecView.RunStack.DataSource = null;
        }

        private void PopulateSymbols()
        {
            var stable = engine.RuntimeContext.Runnable.ScriptSymbolTable;

            var list = new List<object>();

            foreach (var item in stable)
            {
                list.Add(new { item.Value.Name, Value = GetValue(item.Value.Value), item.Value.Function });
            }

            ExecView.Symbols.DataSource = list;            
        }

        private void PopulateStack()
        {
            var list = new List<object>();

            foreach (IStackItem item in engine.RuntimeContext.Runnable.ParamStack)
            {
                list.Add(new { StackItem = item.ToString() });
            }

            ExecView.EvalStack.DataSource = list;
        }

        private void PopulateRunStack()
        {
            var list = new List<object>();

            foreach (IStackItem item in engine.RuntimeContext.Runnable.RuntimeStack)
            {
                list.Add(new { StackCall = item.ToString() });
            }

            ExecView.RunStack.DataSource = list;
        }

        private void ClearLines()
        {
            ExecView.Asm.SelectAll();
            ExecView.Asm.SelectionBackColor = Color.Black;
            ExecView.Asm.SelectionColor = Color.Lime;

            ExecView.Script.SelectAll();
            ExecView.Script.SelectionBackColor = Color.White;
            ExecView.Script.SelectionColor = Color.Black;
        }

        private void SelectLine()
        {
            ExecView.Asm.SelectAll();
            ExecView.Asm.SelectionBackColor = Color.Black;
            ExecView.Asm.SelectionColor = Color.Lime;

            var line = engine.RuntimeContext.CurrentInst.Index;

            int start = ExecView.Asm.GetFirstCharIndexFromLine(line);

            int length = ExecView.Asm.Lines[line].Length;

            ExecView.Asm.Select(start, length);
            ExecView.Asm.SelectionBackColor = Color.Yellow;
            ExecView.Asm.SelectionColor = Color.Black;

            if (!sourceMap.ContainsKey(line)) return;

            SelectSourceLine(line);
        }

        private void SelectSourceLine(int line)
        {
            var sline = sourceMap[line];

            if (sline < 0) return;

            ExecView.Script.SelectAll();
            ExecView.Script.SelectionBackColor = Color.White;
            ExecView.Script.SelectionColor = Color.Black;

            var sstart = ExecView.Script.GetFirstCharIndexFromLine(sline);

            var slength = ExecView.Script.Lines[sline].Length;

            ExecView.Script.Select(sstart, slength);
            ExecView.Script.SelectionBackColor = Color.Yellow;
        }

        private object GetValue(object value)
        {
            if (value == null) return null;

            var sb = new StringBuilder();

            if (value.GetType() == typeof(object[]))
            {
                sb.Append("[");

                foreach (var obj in (object[])value)
                {
                    if (sb.Length == 1)
                        sb.Append(obj);
                    else
                        sb.Append("," + obj);
                }

                sb.Append("]");
            }
            else
                return value;

            return sb.ToString();
        }

        #endregion
    }
}