namespace CCT.MetodoComTryCatch.App
{
    public interface IClienteApiClient
    {
        Task<Cliente> ObterCliente(long cpfCnpj);
    }

}
