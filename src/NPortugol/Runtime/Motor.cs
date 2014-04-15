using System;
using System.Collections.Generic;
using NPortugol.Modulos;
using NPortugol.Runtime.Asm;
using NPortugol.Runtime.Interop;

namespace NPortugol.Runtime
{
    public class Motor
    {
        private ICompilador _compilador;
        private IHospedagem hospedagem;

        public Motor()
        {
            Init(new Npc());
        }

        public Motor(ICompilador _compilador)
        {
            Init(_compilador);
        }

        private void Init(ICompilador compilador)
        {
            this._compilador = compilador;

            hospedagem = new Hospedagem();
            
            Instalar(new ModuloPadrao());
        }

        public bool Debug { get; set; }

        public bool Debugging { get; set; }

        public bool EnableGC { get; set; }

        public IRuntimeContext RuntimeContext { get; set; }

        public IHospedagem Hospedagem 
        { 
            get { return hospedagem; }
            set { hospedagem = value; }
        }
        
        public void Load(string asm)
        {
            var bytecode = new Bytecode(asm.Split('\n'), null);
            
            Load(bytecode);
        }

        public void Load(Bytecode bytecode)
        {
            var loader = new Loader(new AntlrRunnableBuilder {DebugInfo = Debug});

            var runnable = loader.Load(bytecode.Script);

            RuntimeContext = new RuntimeContext(runnable)
                                 {
                                     Hospedagem = Hospedagem, 
                                     EnableGC = EnableGC
                                 };
        }

        public void LoadDebugInfo(Dictionary<int, int> info)
        {
            RuntimeContext.LoadDebugInfo(info);
        }

        public void Executar()
        {
            RuntimeContext.Debug = Debug;

            RuntimeContext.Execute();

            Debugging = RuntimeContext.Debugging;
        }

        public object Executar(string function, object parameter)
        {
            return Executar(function, new[] {parameter});
        }

        public object Executar(string function, object[] parameters)
        {
            RuntimeContext.Debug = Debug;

            return RuntimeContext.Execute(function, parameters);
        }

        public void Compilar(string script)
        {
            if (_compilador == null)
                throw new Exception("Nenhum compilador configurado para o engine.");

            Load(_compilador.Compilar(script));
        }

        public void Instalar(IModulo modulo)
        {
            foreach (var function in modulo.Functions)
            {
                Hospedagem.Registrar(function.Key, function.Value, false);
            }
        }
    }
}