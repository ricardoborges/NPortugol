using System.Text;

namespace NPortugol.Runtime
{
    public class Instruction
    {
        public Instruction(OpCode opcode, int index, params Operand[] operands)
        {
            OpCode = opcode;

            Operands = operands;

            Index = index;
        }

        public OpCode OpCode { get; private set; }

        public Operand[] Operands { get; set; }

        public int OperandCount
        {
            get { return Operands.Length; }
        }

        public int Index { get; set; }

        public string IndexName
        {
            get
            {
                return string.Format("{0}:", Index.ToString().PadLeft(5, '0'));
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", IndexName, OpCode, GetOps());
        }

        public string GetOps()
        {
            var builder = new StringBuilder();

            foreach (var operand in Operands)
            {
                if (builder.Length == 0)
                    builder.Append(operand);
                else
                    builder.Append(string.Format(", {0}", operand));
            }

            return builder.ToString();
        }
    }
}