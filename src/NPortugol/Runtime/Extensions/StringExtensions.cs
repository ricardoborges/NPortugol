namespace NPortugol.Runtime.Extensions
{
    public static class StringExtensions
    {
        public static string ToSymbolName(this string name, IRuntimeContext context)
        {
            return SymbolTable.BuildSymbolName(context.CurrentFunction.Function.Name, 
                                               name, context.CurrentFunction.Index);
        }
    }
}