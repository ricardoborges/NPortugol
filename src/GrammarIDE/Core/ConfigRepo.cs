namespace GrammarIDE.Core
{
    public interface IConfigRepo
    {
        Config GetConfig();
        void SaveScript(string script);
        void SavePath(string path);
    }

    public class ConfigRepo : IConfigRepo
    {
        public Config GetConfig()
        {
            return Config.Load();
        }

        public void SaveScript(string script)
        {
            var current = GetConfig();

            new Config { DotPath = current.DotPath, Script = script }.Save();            
        }

        public void SavePath(string path)
        {
            var current = GetConfig();

            new Config { DotPath = path, Script = current.Script }.Save();
        }
    }
}