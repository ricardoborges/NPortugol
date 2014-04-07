using System;
using System.Collections.Generic;
using NPortugol.Modules;
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

        public void Init(ICompilador _compilador)
        {
            this._compilador = _compilador;

            hospedagem = new Hospedagem();
            
            Install(new DefaultModule());
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

        public void Execute()
        {
            RuntimeContext.Debug = Debug;

            RuntimeContext.Execute();

            Debugging = RuntimeContext.Debugging;
        }

        public object Execute(string function, object parameter)
        {
            return Execute(function, new[] {parameter});
        }

        public object Execute(string function, object[] parameters)
        {
            RuntimeContext.Debug = Debug;

            return RuntimeContext.Execute(function, parameters);
        }

        public void Compile(string script)
        {
            if (_compilador == null)
                throw new Exception("Nenhum compilador configurado para o engine.");

            Load(_compilador.Compilar(script));
        }

        public void Install(IModule module)
        {
            foreach (var function in module.Functions)
            {
                Hospedagem.Registrar(function.Key, function.Value, false);
            }
        }
    }
}