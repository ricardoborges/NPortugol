namespace NPortugol.Runtime
{
    public class Operand : IStackItem
    {
        public Operand(OperandType type, object value)
        {
            Type = type;
            Value = value;
        }

        public Operand(OperandType type, object value, object indexOffSet)
        {
            Type = type;
            Value = value;
            IndexOffSet = indexOffSet;
        }

        public OperandType Type { get; set; }

        public object Value { get; set; }

        public object IndexOffSet { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} <{1}>", Value, Type);
        }

        public string Name
        {
            get { return ToString(); }
        }
    }
}