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

        Bytecode Build(bool msg);
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
        private Motor _motor = new Motor {EnableGC = false};
        private Dictionary<int, int> sourceMap;

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
            get { return _motor.Debugging; }
        }

        #endregion

        #region Host Functions

        private void BindFunctions()
        {
            _motor.Hospedagem.Registrar("imprima", Imprima, true);
            _motor.Hospedagem.Registrar("imprimaVetor", ImprimaVetor, true);
            _motor.Hospedagem.Registrar("leia", Leia, false);
            _motor.Hospedagem.Registrar("tamanho", Tamanho, false);
            _motor.Hospedagem.Registrar("resto", Resto, false);
        }
        
        private object Imprima(object[] parameters)
        {
            var value = parameters[0].ToString();

            value = value.Replace("True", "Verdadeiro");
            value = value.Replace("False", "Falso");

            ExecView.WriteOutput(value);

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

            ExecView.WriteOutput(sb.ToString());

            return sb.ToString();
        }

        private object Tamanho(object[] parameters)
        {
            var parameter = parameters[0];

            return ((object[])parameter).Length;

        }

        private object Resto(object[] parameters)
        {
            var a = Convert.ToInt32(parameters[0]);
            var b = Convert.ToInt32(parameters[0]);

            return a%b;
        }

        private object Leia(object[] objects)
        {
            return Prompt.ShowDialog("Digite a entrada:", "Leia");
        }

        #endregion

        #region Actions

        public Bytecode Build(bool msg)
        {
            npc.DebugInfo = true;

            var bytecode = npc.Compilar(ExecView.Script.Text);
            sourceMap = bytecode.SourceMap;

            if (!msg) return bytecode;
            
            ExecView.WriteLine();
            ExecView.WriteOutput("Construído com sucesso");

            sourceMap = bytecode.SourceMap;

            return bytecode;
        }

        public void Run()
        {
            ExecView.ClearOutput();

            _motor.Load(Build(false));

            ExecView.WriteOutput("Saída:");
            ExecView.WriteLine();

            _motor.Executar();

            Fill();

            //ExecView.WriteOutput("Concluído");
        }

        public void Debug()
        {
            _motor.Debug = true;

            _motor.Load(Build(false));

            LoadDebugInfo();

            ExecView.ClearOutput();

            _motor.EnableGC = false;
        }

        private void LoadDebugInfo()
        {
            var dict = new Dictionary<int, int>();

            foreach (var item in sourceMap)
            {
                if (!dict.ContainsValue(item.Value) && item.Value > 0)
                    dict.Add(item.Key, item.Value);
            }

            _motor.LoadDebugInfo(dict);
        }

        public void Step()
        {
            _motor.Executar();

            if (_motor.RuntimeContext.Completed)
            {
                ClearLines();
                return;
            }

            SelectLine();

            Fill();
        }

        public void Stop()
        {
            _motor.Debugging = false;
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
            var stable = _motor.RuntimeContext.Runnable.ScriptSymbolTable;

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
            var line = _motor.RuntimeContext.CurrentInst.Index;

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