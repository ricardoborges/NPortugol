namespace NPortugol.Runtime.Interop
{
    public interface IFuncaoHospedada
    {
        object Executar(params object[] parameters);
        
        string Nome { get; }
    }
}