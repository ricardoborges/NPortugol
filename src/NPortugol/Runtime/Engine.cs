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

        public void LoadAsm(string asm)
        {
            LoadAsm(asm.Split('\n'));
        }

        public void LoadAsm(IList<string> lines)
        {
            var loader = new Loader(new AntlrRunnableBuilder {DebugInfo = Debug});

            var runnable = loader.Load(lines);

            var script = new RuntimeScript(runnable);

            RuntimeContext = new RuntimeContext(script)
                                 {
                                     HostContainer = HostContainer, 
                                     EnableGC = EnableGC
                                 };
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
            LoadAsm(compiler.Compile(script));
        }
    }
}