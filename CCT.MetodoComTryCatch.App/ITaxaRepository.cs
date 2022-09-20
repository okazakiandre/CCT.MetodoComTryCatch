namespace CCT.MetodoComTryCatch.App
{
    public interface ITaxaRepository
    {
        Task<double> ObterFrete(int numeroCep);
    }
}
