using System;
using Xunit;

namespace VendasOnLine.Tests
{
    public class PedidoTestsUnit
    {
        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void NaoDeveCriarUmPedidoComCpfInvalido()
        {
            //when - Arrange
            var cpf = "00000000000";

            //given - Act
            var ex = Assert.Throws<Exception>(() => new Pedido(cpf));

            //then - Assert
            Assert.Equal("CPF inválido", ex.Message);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void DeveCriarUmPedidoComTresItens()
        {
            //when - Arrange
            var cpf = "778.278.412-36";

            //given - Act
            var pedido = new Pedido(cpf);
            pedido.AdicionarItem(new ItemPedido(1, 1000, 2));
            pedido.AdicionarItem(new ItemPedido(2, 5000, 1));
            pedido.AdicionarItem(new ItemPedido(3, 30, 3));

            //then - Assert
            Assert.Equal(6, pedido.QuantidadeItens());
            Assert.Equal(7090, pedido.ValorTotal());
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void DeveCriarUmPedidoComCupomDeDesconto()
        {
            //when - Arrange
            var cpf = "778.278.412-36";

            //given - Act
            var pedido = new Pedido(cpf);
            pedido.AdicionarItem(new ItemPedido(1, 1000, 2));
            pedido.AdicionarItem(new ItemPedido(2, 5000, 1));
            pedido.AdicionarItem(new ItemPedido(3, 30, 3));
            pedido.AdicionarCupom(new Cupom("VALE20", 20, DateTime.Now)) ;

            //then - Assert
            Assert.Equal(6, pedido.QuantidadeItens());
            Assert.Equal(5672, pedido.ValorTotal());
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void NaoDeveAplicarCupomDeDescontoExpirado()
        {
            //when - Arrange
            var cpf = "778.278.412-36";
            var pedido = new Pedido(cpf);
            pedido.AdicionarItem(new ItemPedido(1, 1000, 2));
            pedido.AdicionarItem(new ItemPedido(2, 5000, 1));
            pedido.AdicionarItem(new ItemPedido(3, 30, 3));

            //given - Act
            pedido.AdicionarCupom(new Cupom("VALE20", 20, new DateTime(2021, 07, 15)));

            //then - Assert
            Assert.Equal(6, pedido.QuantidadeItens());
            Assert.Equal(7090, pedido.ValorTotal());
        }

    }
}
