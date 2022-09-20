namespace CCT.MetodoComTryCatch.App
{
    public class ClienteApiClient : IClienteApiClient
    {
        public async Task<Cliente> ObterCliente(long cpfCnpj)
        {
            var cliente = new Cliente
            {
                NumeroCpf = cpfCnpj,
                Nome = "Jose da Silva",
                NumeroCep = 12345
            };
            return cliente;
        }
    }

}
