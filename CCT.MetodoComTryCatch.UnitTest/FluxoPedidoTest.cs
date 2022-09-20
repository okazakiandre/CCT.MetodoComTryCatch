using CCT.MetodoComTryCatch.App;
using Moq;

namespace CCT.MetodoComTryCatch.UnitTest
{
    [TestClass]
    public class FluxoPedidoTest
    {
        [TestMethod]
        public async Task DeveriaSalvarPedido()
        {
            var mockTaxa = new Mock<ITaxaRepository>();
            mockTaxa.Setup(m => m.ObterFrete(It.IsAny<int>())).ReturnsAsync(1.1);
            var cliente = new Cliente
            {
                Nome = "cliente teste",
                NumeroCpf = 123
            };
            var mockCli = new Mock<IClienteApiClient>();
            mockCli.Setup(m => m.ObterCliente(It.IsAny<long>())).ReturnsAsync(cliente);
            var mockPed = new Mock<IPedidoRepository>();
            var flx = new FluxoPedido(mockTaxa.Object, mockPed.Object, mockCli.Object);

            var res = await flx.SalvarPedido(1111, "123,45");

            Assert.AreEqual("cliente teste", res.NomeCliente);
            Assert.AreEqual(123, res.CpfCliente);
            Assert.AreEqual(123.45, res.ValorTotal);
            Assert.AreEqual(1.1, res.ValorFrete);
            mockPed.Verify(m => m.Salvar(It.IsAny<Pedido>()), Times.Once);
        }

        [TestMethod]
        public async Task NaoDeveriaSalvarPedidoSeOcorrerErro()
        {
            var mockTaxa = new Mock<ITaxaRepository>();
            mockTaxa.Setup(m => m.ObterFrete(It.IsAny<int>())).ReturnsAsync(1.1);
            var mockCli = new Mock<IClienteApiClient>();
            mockCli.Setup(m => m.ObterCliente(It.IsAny<long>())).ThrowsAsync(new Exception("erro ao consultar o cliente"));
            var mockPed = new Mock<IPedidoRepository>();
            var flx = new FluxoPedido(mockTaxa.Object, mockPed.Object, mockCli.Object);

            var exc = await Assert.ThrowsExceptionAsync<Exception>(() =>
                        flx.SalvarPedido(1111, "123,45")
                      );

            Assert.IsTrue(exc.Message.Contains("erro ao consultar o cliente"));
        }
    }
}