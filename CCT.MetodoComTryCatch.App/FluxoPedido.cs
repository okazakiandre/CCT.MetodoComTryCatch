using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace CCT.MetodoComTryCatch.App
{
    public class FluxoPedido
    {
        private ITaxaRepository TaxaRepo { get; }
        private IPedidoRepository PedRepo { get; }
        private IClienteApiClient CliApi { get; }

        public FluxoPedido(ITaxaRepository taxaRepo,
                           IPedidoRepository pedRepo,
                           IClienteApiClient cliApi)
        {
            TaxaRepo = taxaRepo;
            PedRepo = pedRepo;
            CliApi = cliApi;
        }

        public async Task<Pedido> SalvarPedido(long cpfCliente,
                                               string valorTotal)
        {
            try
            {
                var cliente = await CliApi.ObterCliente(cpfCliente);
                var ped = new Pedido
                {
                    CpfCliente = cliente.NumeroCpf,
                    NomeCliente = cliente.Nome
                };

                ped.ValorFrete = await TaxaRepo.ObterFrete(cliente.NumeroCep);
                ped.ValorTotal = double.Parse(valorTotal, new CultureInfo("pt-BR"));

                await PedRepo.Salvar(ped);

                return ped;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro inesperado. Mensagem: {ex.Message}");
            }
        }
    }
}
