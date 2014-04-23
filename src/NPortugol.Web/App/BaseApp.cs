using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using NPortugol.Runtime;
using NPortugol.Runtime.Interop;
using NPortugol.Web.Modules;

namespace NPortugol.Web.App
{
    public abstract class BaseApp : IHttpHandler
    {
        protected Motor CreateEngine(string script)
        {
            return CreateEngine(script, null);
        }

        protected Motor CreateEngine(string script, SymbolTable model)
        {
            var engine = new Motor();

            engine.Instalar(new WebModulo());
            engine.Instalar(new HtmlModulo());

            var modules = RetrieveModules();

            foreach (var module in modules)
                engine.Instalar(module);

            engine.Compilar(script);

            if (model != null)
                engine.RuntimeContext.Runnable.ScriptSymbolTable = model;

            return engine;
        }

        protected static List<IModulo> RetrieveModules()
        {
            var appInst = HttpContext.Current.ApplicationInstance as INPortugolWebApp;

            return appInst == null ? new List<IModulo>() : appInst.Container.Modules;
        }

        public static string ExtractContent(string path)
        {
            if (!File.Exists(path))
            {
                var parts = path.Split('\\');

                var file = parts[parts.Length - 1];

                if (path.EndsWith(".html"))
                    throw new Exception("Visão não encontrada. " + file);
                
                throw new Exception("Controle não encontrado. " + file);
            }

            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        public abstract void ProcessRequest(HttpContext context);

        public bool IsReusable { get { return false; } }
    }
}