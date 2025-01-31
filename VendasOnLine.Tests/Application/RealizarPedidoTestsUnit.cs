using System;
using System.Collections.Generic;
using VendasOnLine.Application;
using VendasOnLine.Infra;
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
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var pedidoCommandHandler = new PedidoCommandHandler(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "00000000000"
            };

            //given - Act
            var ex = Assert.Throws<Exception>(() => pedidoCommandHandler.Handle(criarPedidoCommand));

            //then - Assert
            Assert.Equal("CPF inv�lido", ex.Message);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoComCpfValido()
        {
            //when - Arrange
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var pedidoCommandHandler = new PedidoCommandHandler(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
            var criarPedidoCommand = new CriarPedidoCommand
            {
                Cpf = "04831420000"
            };

            //given - Act
            var pedido = pedidoCommandHandler.Handle(criarPedidoCommand);

            //then - Assert
            Assert.Equal(criarPedidoCommand.Cpf, pedido.Cpf.Numero);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public void RealizarPedidoAdicionarItem()
        {
            //when - Arrange
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var pedidoCommandHandler = new PedidoCommandHandler(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
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
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var pedidoCommandHandler = new PedidoCommandHandler(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
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
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var pedidoCommandHandler = new PedidoCommandHandler(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
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
        public async void DeveFazerUmPedido()
        {
            //when - Arrange
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepository();
            var calculadoraCepApi = new CalculadoraCepApi();
            var pedidoCommandHandler = new PedidoCommandHandler(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
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
            var pedido = await pedidoCommandHandler.Handle(criarPedidoCommand);

            //then - Assert
            Assert.Equal(criarPedidoCommand.Cpf, pedido.Cpf);
            Assert.Equal(6, pedido.QuantidadeItens);
            Assert.Equal(5982, pedido.ValorTotal);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public async void NaoDeveAplicarCupomDeDescontoExpirado()
        {
            //when - Arrange
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var pedidoCommandHandler = new PedidoCommandHandler(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
            var criarPedidoCommand = new CriarPedidoCompletoCommand
            {
                Cpf = "04831420000",
                Cep = "11.111-111",
                CodigoCupom = "VALE20_EXPIRED",
                Items = new List<ItemDto>
                {
                    new ItemDto{Id = 1, Quantidade = 2 },
                    new ItemDto{Id = 2, Quantidade =  1 },
                    new ItemDto{Id = 3, Quantidade = 3 }
                }
            };

            //given - Act
            var pedido = await pedidoCommandHandler.Handle(criarPedidoCommand);

            //then - Assert
            Assert.Equal(6, pedido.QuantidadeItens);
            Assert.Equal(7400, pedido.ValorTotal);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public async void DeveCalcularValorDoFrete()
        {
            //when - Arrange
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var pedidoCommandHandler = new PedidoCommandHandler(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
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
            var pedido = await pedidoCommandHandler.Handle(criarPedidoCommand);

            //then - Assert
            Assert.Equal(6, pedido.QuantidadeItens);
            Assert.Equal(7400, pedido.ValorTotal);
            Assert.Equal(310, pedido.Frete);
        }
    }
}
