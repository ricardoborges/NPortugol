using System;
using System.Collections.Generic;
using NPortugol.Modules;
using NPortugol.Runtime.Asm;
using NPortugol.Runtime.Interop;

namespace NPortugol.Runtime
{
    public class Engine
    {
        private ICompiler compiler;
        private IHostContainer hostContainer;

        public Engine()
        {
            Init(new Npc());
        }

        public Engine(ICompiler compiler)
        {
            Init(compiler);
        }

        public void Init(ICompiler compiler)
        {
            this.compiler = compiler;

            hostContainer = new HostContainer();
            
            Install(new DefaultModule());
        }

        public bool Debug { get; set; }

        public bool Debugging { get; set; }

        public bool EnableGC { get; set; }

        public IRuntimeContext RuntimeContext { get; set; }

        public IHostContainer HostContainer 
        { 
            get { return hostContainer; }
            set { hostContainer = value; }
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
                                     HostContainer = HostContainer, 
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
            if (compiler == null)
                throw new Exception("Nenhum compilador configurado para o engine.");

            Load(compiler.Compile(script));
        }

        public void Install(IModule module)
        {
            foreach (var function in module.Functions)
            {
                HostContainer.Register(function.Key, function.Value, false);
            }
        }
    }
}