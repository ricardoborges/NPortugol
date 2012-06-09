using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine.Utility;
using Npc.Modules;
using NPortugol.Runtime;

namespace Npc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Do(args);
            }
            catch (Exception ex)
            {
                Console.Write("Erro: programa inválido.");
            }
        }

        private static void Do(string[] args)
        {
            bool compile, execute, bytecode, help;
            string function;
            string parameters;

            var arguments = new Arguments(args);

            help = arguments.Contains("a");
            compile = arguments.Contains("c");
            bytecode = arguments.Contains("b");
            execute = arguments.Contains("e");
            function = arguments.Contains("f") ? arguments["f"] : string.Empty;
            parameters = arguments.Contains("p") ? arguments["p"] : string.Empty;

            if (help || args.Length == 0)
            {
                Console.WriteLine("--- Ajuda ---");
                Console.WriteLine("");
                Console.WriteLine("Para executar interpretando um programa: npc.exe _arquivo_");
                Console.WriteLine("");
                Console.WriteLine("Outras opções: ");
                Console.WriteLine("");
                Console.WriteLine("-c    Compila o codigo fonte para o formato binario. Ex: npc -c ola.txt");
                Console.WriteLine("-b    Gera o bytecode em arquivo texto para visualização. Ex: npc ola.txt -b");
                Console.WriteLine("-e    Executa um programa compilado. Ex: npc -e ola.npx");
                Console.WriteLine("-a    Ajuda.");
                return;
            }

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
                Execute(args[0], function, parameters);
                return;
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
                var list = new List<object>();
                
                foreach (var parameter in parameters.Split(','))
                {
                    list.Add(parameter.Trim());
                }

                engine.Execute(function, list.ToArray());
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(string.Format("{0} executado com sucesso. <ENTER>", filename));
        }

        private static void Execute(string filename, string function, string parameters)
        {
            var engine = new Engine();
            engine.Load(new NPortugol.Npc().ReadFromDisk(filename));
            engine.Install(new ConsoleModule());

            if (string.IsNullOrEmpty(function))
                engine.Execute();
            else
            {
                var list = new List<object>();

                foreach (var parameter in parameters.Split(','))
                {
                    list.Add(parameter.Trim());
                }

                engine.Execute(function, list.ToArray());
            }

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
