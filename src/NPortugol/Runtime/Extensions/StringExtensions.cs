namespace NPortugol.Runtime.Extensions
{
    public static class StringExtensions
    {
        public static string ToSymbolId(this string name, IRuntimeContext context)
        {
            return SymbolTable.BuildSymbolId(context.CurrentFunction.Function.Name, 
                                               name, context.CurrentFunction.Index);
        }
    }
}