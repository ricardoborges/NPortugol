namespace NPortugol.Runtime.Interop
{
    public interface IHostFunction
    {
        object Execute(params object[] parameters);
        
        string Name { get; }
    }
}