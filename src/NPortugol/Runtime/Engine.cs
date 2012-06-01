using System;
using System.Collections.Generic;
using NPortugol.Runtime.Asm;
using NPortugol.Runtime.Interop;

namespace NPortugol.Runtime
{
    public class Engine
    {
        private readonly ICompiler compiler;

        public Engine()
        {
            compiler = new Npc();
        }

        public Engine(ICompiler compiler)
        {
            this.compiler = compiler;
        }

        public bool Debug { get; set; }

        public bool Debugging { get; set; }

        public bool EnableGC { get; set; }

        public IRuntimeContext RuntimeContext { get; set; }

        private IHostContainer hostContainer;

        public IHostContainer HostContainer 
        { 
            get { return hostContainer ?? (hostContainer = new HostContainer()); }
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

            var script = new RuntimeScript(runnable);

            RuntimeContext = new RuntimeContext(script)
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

        public object Execute(string function, params object[] parameters)
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
    }
}