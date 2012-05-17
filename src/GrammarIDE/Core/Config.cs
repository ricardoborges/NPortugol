using System;
using System.IO;
using System.Xml.Serialization;

namespace GrammarIDE.Core
{
    public class Config
    {
        private static string name = "config.xml";

        public string DotPath { get; set; }

        public string Script { get; set; }

        public void Save()
        {
            if (File.Exists(name)) File.Delete(name);

            using (var writter = new StreamWriter(name))
            {
                new XmlSerializer(typeof(Config)).Serialize(writter, this);            
            }
        }

        public static Config Load()
        {
            if (!File.Exists(name)) return new Config
                                               {
                                                   DotPath = AppDomain.CurrentDomain.BaseDirectory + "dot\\"
                                               };

            using (var reader = new StreamReader(name))
            {
                return new XmlSerializer(typeof (Config)).Deserialize(reader) as Config;
            }
        }
    }
}