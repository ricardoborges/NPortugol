namespace NPortugol.Runtime
{
    public class Function
    {
        public Function(string name, int entryPoint)
        {
            Name = name;
            EntryPoint = entryPoint;
        }

        public const string MainName = "main";

        public string Name { get; private set; }

        public int EntryPoint { get; private set; }

        public override string ToString()
        {
            return string.Format("Function {0}", Name);
        }
    }
}