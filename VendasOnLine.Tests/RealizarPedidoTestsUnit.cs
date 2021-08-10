using System;
using System.Collections.Generic;
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
            var pedidoCommandHandler = new PedidoCommandHandler(new PedidoRepository());
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
            var pedidoCommandHandler = new PedidoCommandHandler(new PedidoRepository());
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
            var pedidoCommandHandler = new PedidoCommandHandler(new PedidoRepository());
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "04831420000"
            };
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);
            var adicionarItemCommand = new AdicionarItemCommand
            {
                IdPedido = pedido.Id,
                Id = 1,
                Quantidade = 1
            };

            //given - Act
            pedidoCommandHandler.Handle(adicionarItemCommand);

            //then - Assert
            Assert.Equal(1, pedido.QuantidadeItens());
            Assert.Equal(1000, pedido.ValorTotal());
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoAdicionarCupomDesconto()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler(new PedidoRepository());
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "04831420000"
            };
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);
            var adicionarItemCommand = new AdicionarItemCommand
            {
                IdPedido = pedido.Id,
                Id = 1,
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
            Assert.Equal(800, pedido.ValorTotal());
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoAdicionarCupomDescontoInvalido()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler(new PedidoRepository());
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "04831420000"
            };
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);
            var adicionarItemCommand = new AdicionarItemCommand
            {
                IdPedido = pedido.Id,
                Id = 1,
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
            Assert.Equal(1000, pedido.ValorTotal());
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void DeveFazerUmPedido()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler(new PedidoRepository());
            var criarPedidoCommand = new CriarPedidoCompletoCommand
            {
                Cpf = "04831420000",
                Cep = "11.111-111",
                CodigoCupom = "VALE20",
                Items = new List<ItemDto>
                {
                    new ItemDto{Id = 1, Quantidade = 2 },
                    new ItemDto{Id = 2, Quantidade =  1 },
                    new ItemDto{Id = 3, Quantidade = 3 }
                }
            };

            //given - Act
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);

            //then - Assert
            Assert.Equal(criarPedidoCommand.Cpf, pedido.Cpf);
            Assert.Equal(6, pedido.QuantidadeItens);
            Assert.Equal(5982, pedido.ValorTotal);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void NaoDeveAplicarCupomDeDescontoExpirado()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler(new PedidoRepository());
            var criarPedidoCommand = new CriarPedidoCompletoCommand
            {
                Cpf = "04831420000",
                Cep = "11.111-111",
                CodigoCupom = "VALEEXP",
                Items = new List<ItemDto>
                {
                    new ItemDto{Id = 1, Quantidade = 2 },
                    new ItemDto{Id = 2, Quantidade =  1 },
                    new ItemDto{Id = 3, Quantidade = 3 }
                }
            };

            //given - Act
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);

            //then - Assert
            Assert.Equal(6, pedido.QuantidadeItens);
            Assert.Equal(7400, pedido.ValorTotal);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void DeveCalcularValorDoFrete()
        {
            //when - Arrange
            var pedidoCommandHandler = new PedidoCommandHandler(new PedidoRepository());
            var criarPedidoCommand = new CriarPedidoCompletoCommand
            {
                Cpf = "04831420000",
                Cep = "11.111-111",
                Items = new List<ItemDto>
                {
                    new ItemDto{Id = 1, Quantidade = 2 },
                    new ItemDto{Id = 2, Quantidade =  1 },
                    new ItemDto{Id = 3, Quantidade = 3 }
                }
            };

            //given - Act
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);

            //then - Assert
            Assert.Equal(6, pedido.QuantidadeItens);
            Assert.Equal(7400, pedido.ValorTotal);
            Assert.Equal(310, pedido.Frete);
        }
    }
}
