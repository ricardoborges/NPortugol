using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NPortugol.Runtime
{
    public class BytecodeSerializer
    {
        public void Create(string filename, Bytecode bytecode)
        {
            using (var stream = File.Open(filename, FileMode.Create))
            {
                var bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, bytecode);
            }
        }

        public Bytecode Read(string filename)
        {
            Bytecode bytecode;

            try
            {
                using (var stream = File.Open(filename, FileMode.Open))
                {
                    var bFormatter = new BinaryFormatter();

                    bytecode = (Bytecode)bFormatter.Deserialize(stream);
                }

                return bytecode;
            }
            catch
            {
                throw new Exception(string.Format("Este arquivo não é um programa válido. {0}", filename));
            }
        }
    }
}