using System;
using Xunit;

namespace VendasOnLine.Tests
{
    public class RealizarPedidoTestsUnit
    {
        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoComCpfInvalido()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler();
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "00000000000"
            };

            //given - Act
            var ex = Assert.Throws<Exception>(() => pedidoCommandHandler.Handle(criarPedidoCommand));

            //then - Assert
            Assert.Equal("CPF inválido", ex.Message);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoComCpfValido()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler();
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "04831420000"
            };

            //given - Act
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);

            //then - Assert
            Assert.Equal(criarPedidoCommand.Cpf, pedido.Cpf);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoAdicionarItem()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler();
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "04831420000"
            };
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);

            var adicionarItemCommand = new AdicionarItemCommand
            {
                IdPedido = pedido.Id,
                Descricao = "Cerveja",
                Valor = 23.5,
                Quantidade = 1
            };

            //given - Act
            pedidoCommandHandler.Handle(adicionarItemCommand);

            //then - Assert
            Assert.Equal(1, pedido.QuantidadeItens());
            Assert.Equal(23.5, pedido.ValorTotal());
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoAdicionarCupomDesconto()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler();
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "04831420000"
            };
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);

            var adicionarItemCommand = new AdicionarItemCommand
            {
                IdPedido = pedido.Id,
                Descricao = "Cerveja",
                Valor = 23.5,
                Quantidade = 1
            };
            pedidoCommandHandler.Handle(adicionarItemCommand);

            var adicionarCupomDescontoCommand = new AdicionarCupomDescontoCommand
            {
                IdPedido = pedido.Id,
                CodigoCupom = "VALE20"
            };

            //given - Act
            pedidoCommandHandler.Handle(adicionarCupomDescontoCommand);

            //then - Assert
            Assert.NotNull(pedido.CupomDesconto);
            Assert.Equal(1, pedido.QuantidadeItens());
            Assert.Equal(18.8, pedido.ValorTotal());
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoAdicionarCupomDescontoInvalido()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler();
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "04831420000"
            };
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);

            var adicionarItemCommand = new AdicionarItemCommand
            {
                IdPedido = pedido.Id,
                Descricao = "Cerveja",
                Valor = 23.5,
                Quantidade = 1
            };
            pedidoCommandHandler.Handle(adicionarItemCommand);

            var adicionarCupomDescontoCommand = new AdicionarCupomDescontoCommand
            {
                IdPedido = pedido.Id,
                CodigoCupom = "VALE30"
            };

            //given - Act
            pedidoCommandHandler.Handle(adicionarCupomDescontoCommand);

            //then - Assert
            Assert.Null(pedido.CupomDesconto);
            Assert.Equal(1, pedido.QuantidadeItens());
            Assert.Equal(23.5, pedido.ValorTotal());
        }
    }
}
