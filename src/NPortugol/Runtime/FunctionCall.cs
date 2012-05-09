namespace NPortugol.Runtime
{
    public class FunctionCall : IStackItem
    {
        public FunctionCall(Function function, int index)
        {
            Function = function;
            Index = index;
        }

        public Function Function { get; set; }

        public int Index { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Function.Name, Index);
        }

        public string Name
        {
            get { return ToString(); }
        }
    }
}