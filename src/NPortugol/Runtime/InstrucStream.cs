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
    }
}