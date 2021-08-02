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
            pedido.AdicionarItem(new Item("Guitarra", 1000, 2));
            pedido.AdicionarItem(new Item("Amplificador", 5000, 1));
            pedido.AdicionarItem(new Item("Cabo", 30, 3));

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
            pedido.AdicionarItem(new Item("Guitarra", 1000, 2));
            pedido.AdicionarItem(new Item("Amplificador", 5000, 1));
            pedido.AdicionarItem(new Item("Cabo", 30, 3));
            pedido.AdicionarCupom(new Cupom("VALE20", 20));

            //then - Assert
            Assert.Equal(6, pedido.QuantidadeItens());
            Assert.Equal(5672, pedido.ValorTotal());
        }
    }
}
