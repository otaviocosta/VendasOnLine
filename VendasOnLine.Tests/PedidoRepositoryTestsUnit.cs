using Xunit;

namespace VendasOnLine.Tests
{
    public class PedidoRepositoryTestsUnit
    {
        [Fact]
        [Trait("Categoria", "PedidoRepository")]
        public void DeveInclurNoRepositorio()
        {
            //when - Arrange
            var cpf = "778.278.412-36";
            var pedido = new Pedido(1, cpf, "");
            pedido.AdicionarItem(new ItemPedido(1, 1000, 2));
            pedido.AdicionarItem(new ItemPedido(2, 5000, 1));
            pedido.AdicionarItem(new ItemPedido(3, 30, 3));

            IPedidoRepository repository = new PedidoRepository();

            //given - Act
            //then - Assert
            repository.Incluir(pedido);
        }

        [Fact]
        [Trait("Categoria", "PedidoRepository")]
        public void DeveBuscarDoRepositorio()
        {
            //when - Arrange
            var cpf = "778.278.412-36";
            var pedido = new Pedido(1, cpf, "");
            pedido.AdicionarItem(new ItemPedido(1, 1000, 2));
            pedido.AdicionarItem(new ItemPedido(2, 5000, 1));
            pedido.AdicionarItem(new ItemPedido(3, 30, 3));

            IPedidoRepository repository = new PedidoRepository();
            repository.Incluir(pedido);

            //given - Act
            var pedidoResgatado = repository.Buscar(pedido.Id.Value);

            //then - Assert
            Assert.Equal(pedido.Id.Value, pedidoResgatado.Id.Value);
        }

        [Fact]
        [Trait("Categoria", "PedidoRepository")]
        public void DeveRetornarSequencial()
        {
            //when - Arrange
            IPedidoRepository repository = new PedidoRepository();

            //given - Act
            var seq = repository.ProximoSequencial();

            //then - Assert
            Assert.Equal(1, seq);
        }

        [Fact]
        [Trait("Categoria", "PedidoRepository")]
        public void DeveRetornarProximoSequencial()
        {
            //when - Arrange
            IPedidoRepository repository = new PedidoRepository();
            var cpf = "778.278.412-36";
            var pedido = new Pedido(1, cpf, "");
            pedido.AdicionarItem(new ItemPedido(1, 1000, 2));
            pedido.AdicionarItem(new ItemPedido(2, 5000, 1));
            pedido.AdicionarItem(new ItemPedido(3, 30, 3));
            repository.Incluir(pedido);

            //given - Act
            var seq = repository.ProximoSequencial();

            //then - Assert
            Assert.Equal(2, seq);
        }

    }
}
