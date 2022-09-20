namespace CCT.MetodoComTryCatch.App
{
    public interface IPedidoRepository
    {
        Task Salvar(Pedido pedido);
    }
}
