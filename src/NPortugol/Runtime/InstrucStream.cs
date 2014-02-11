using System;
using System.Text;

namespace NPortugol.Runtime
{
    public class InstrucStream
    {
        public InstrucStream(params Instruction[] instructions)
        {
            Instructions = instructions;
        }

        public Instruction[] Instructions { get; set; }

        public int Size
        {
            get { return Instructions.Length; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var instruction in Instructions)
            {
                sb.AppendLine(instruction.ToString());
            }

            return sb.ToString();
        }
    }
}