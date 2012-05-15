using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NPortugol;
using NPortugol.Runtime;
using TurboNPortugol.Core;
using TurboNPortugol.Views.Exec;

namespace TurboNPortugol.Presenters
{
    public interface IExecPresenter
    {
        IExecView ExecView { get; set; }
        IMainPresenter MainPresenter { get; set; }

        string ProgramName { get; set; }
        string FilePath { get; set; }

        bool Debugging { get; }

        IList<string> Build(bool msg);
        void Run();
        void Debug();
        void Step();
        void Stop();

        void Error(Exception ex);
        void Error(string text);
    }

    public class ExecPresenter : IExecPresenter
    {
        private Npc npc = new Npc();
        private Engine engine = new Engine {EnableGC = false};

        public ExecPresenter(IMainPresenter mainPresenter)
        {
            this.MainPresenter = mainPresenter;

            BindFunctions();
        }

        #region Properties

        public IMainPresenter MainPresenter { get; set; }

        public string ProgramName { get; set; }

        public string FilePath { get; set; }

        public IExecView ExecView { get; set; }

        public bool Debugging
        {
            get { return engine.Debugging; }
        }

        #endregion

        #region Host Functions

        private void BindFunctions()
        {
            engine.HostContainer.Register("imprima", x => ExecView.WriteOutput(((Operand)x[0]).Value.ToString()));
            engine.HostContainer.Register("imprimaVetor", x => ImprimaVetor(x) );
            engine.HostContainer.Register("leia", x => Leia(x));
            engine.HostContainer.Register("tamanho", x => Tamanho(x));
        }

        private object ImprimaVetor(object[] parameters)
        {
            var list = ((Operand) parameters[0]).Value as object[];

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

            ExecView.WriteOutput(sb.ToString());

            return sb.ToString();
        }

        private object Tamanho(object[] parameters)
        {
            var parameter = (Operand)parameters[0];

            return ((object[])parameter.Value).Length;

        }

        private object Leia(object[] objects)
        {
            return Prompt.ShowDialog("Entrada1:", "Entrada2:");
        }

        #endregion

        #region Actions

        public IList<string> Build(bool msg)
        {
            npc.DebugInfo = true;

            var script = npc.Compile(ExecView.Script.Text);

            if (!msg) return script;
            
            ExecView.WriteLine();
            ExecView.WriteOutput("Construído com sucesso");

            return script;
        }

        public void Run()
        {
            ExecView.ClearOutput();

            engine.LoadAsm(Build(false));

            ExecView.WriteOutput("Saída:");
            ExecView.WriteLine();

            engine.Execute();

            Fill();

            //ExecView.WriteOutput("Concluído");
        }

        public void Debug()
        {
            engine.Debug = true;

            engine.LoadAsm(Build(false));

            LoadDebugInfo();

            ExecView.ClearOutput();

            engine.EnableGC = false;
        }

        private void LoadDebugInfo()
        {
            var dict = new Dictionary<int, int>();

            foreach (var item in npc.SourceMap)
            {
                if (!dict.ContainsValue(item.Value) && item.Value > 0)
                    dict.Add(item.Key, item.Value);
            }

            engine.LoadDebugInfo(dict);
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
        
        private void Fill()
        {
            PopulateSymbols();
        }

        private void ClearStacks()
        {
            ExecView.Symbols.DataSource = null;
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



        private void ClearLines()
        {
            ExecView.Script.SelectAll();
            ExecView.Script.SelectionBackColor = Color.White;
            ExecView.Script.SelectionColor = Color.Black;
        }

        private void SelectLine()
        {
            var line = engine.RuntimeContext.CurrentInst.Index;

            if (!npc.SourceMap.ContainsKey(line)) return;

            SelectSourceLine(line);
        }

        private void SelectSourceLine(int line)
        {
            var sline = npc.SourceMap[line];

            if (sline < 0) return;

            ExecView.Script.SelectAll();
            ExecView.Script.SelectionBackColor = Color.White;
            ExecView.Script.SelectionColor = Color.Black;

            var sstart = ExecView.Script.GetFirstCharIndexFromLine(sline);

            var slength = ExecView.Script.Lines[sline].Length;

            ExecView.Script.Select(sstart, slength);
            ExecView.Script.SelectionBackColor = Color.Yellow;
        }

        public void Error(Exception ex)
        {
            ExecView.ClearOutput();
            ExecView.WriteOutput(ex.Message);
        }

        public void Error(string text)
        {
            ExecView.ClearOutput();
            ExecView.WriteOutput(text);
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