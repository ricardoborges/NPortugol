using System;
using System.IO;
using CommandLine.Utility;
using Npc.Modules;
using NPortugol.Runtime;

namespace Npc
{
    class Program
    {
        static void Main(string[] args)
        {
            bool compile, execute, bytecode;
            string function;
            string parameters;

            if (args.Length == 0) return;

            var arguments = new Arguments(args);

            compile = arguments.Contains("c");
            bytecode = arguments.Contains("b");
            execute = arguments.Contains("e");
            function = arguments.Contains("f") ? arguments["f"] : string.Empty;
            parameters = arguments.Contains("p") ? arguments["p"] : string.Empty;

            if (bytecode)
            {
                CompileBytecode(args[0]);
                return;
            }

            if (compile)
            {
                Compile(args[0]);
                return;
            }

            if (execute)
            {
                Execute(args[0]);
            }

            Run(args[0], function, parameters);
        }

        private static void CompileBytecode(string filename)
        {
            var compiler = new NPortugol.Npc();

            var bc = compiler.CompileFile(filename);

            using(var bcfile = new StreamWriter("bytecode.txt"))
            {
                foreach (var line in bc.Script)
                {
                    bcfile.WriteLine(line);
                }

                bcfile.Flush();
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(string.Format("Arquivo bytecode.txt gerado. <ENTER>", filename));
        }

        private static bool FileExists(string filename)
        {
            if (File.Exists(filename))
                return true;

            return false;
        }

        private static void Run(string filename, string function, string parameters)
        {
            if (!FileExists(filename)) return;

            var engine = new Engine();
            engine.Load(new NPortugol.Npc().CompileFile(filename));

            engine.Install(new ConsoleModule());

            if (string.IsNullOrEmpty(function))
                engine.Execute();
            else
            {
               // engine.Execute("")
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(string.Format("{0} executado com sucesso. <ENTER>", filename));
        }

        private static void Execute(string filename)
        {
            var engine = new Engine();
            engine.Load(new NPortugol.Npc().ReadFromDisk(filename));
            engine.Install(new ConsoleModule());
            engine.Execute();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(string.Format("{0} executado com sucesso. <ENTER>", filename));
        }

        private static void Compile(string filename)
        {
            var compiler = new NPortugol.Npc();
            compiler.WriteToDisk(filename);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(string.Format("{0} compilado com sucesso. <ENTER>", filename));
        }
    }
}
