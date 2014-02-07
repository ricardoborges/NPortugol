using System.Collections.Generic;
using System.IO;
using System.Web;
using NPortugol.Runtime;
using NPortugol.Runtime.Interop;
using NPortugol.Web.Modules;

namespace NPortugol.Web.Core
{
    public class BaseLayer
    {
        protected Engine CreateEngine(string script)
        {
            return CreateEngine(script, null);
        }

        protected Engine CreateEngine(string script, SymbolTable model)
        {
            var engine = new Engine();

            engine.Install(new WebModule());
            engine.Install(new HtmlModule());

            var modules = RetrieveModules();

            foreach (var module in modules)
                engine.Install(module);

            engine.Compile(script);

            if (model != null)
                engine.RuntimeContext.Runnable.ScriptSymbolTable = model;

            return engine;
        }

        protected static List<IModule> RetrieveModules()
        {
            var appInst = HttpContext.Current.ApplicationInstance as INPortugolWebApp;

            return appInst == null ? new List<IModule>() : appInst.Container.Modules;
        }

        protected static string ExtractContent(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}