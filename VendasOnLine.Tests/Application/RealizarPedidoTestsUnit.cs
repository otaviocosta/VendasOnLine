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
        public async void RealizarPedidoComCpfInvalido()
        {
            //when - Arrange
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var criarPedidoApplication = new CriarPedidoApplication(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
            var criarPedidoInput = new CriarPedidoInput
            {
                Cpf = "00000000000",
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
            var ex = await Assert.ThrowsAsync<Exception>(() => criarPedidoApplication.Execute(criarPedidoInput));

            //then - Assert
            Assert.Equal("CPF inválido", ex.Message);
        }

        [Fact]
        [Trait("Categoria", "RealizarPedido")]
        public async void RealizarPedidoComCpfValido()
        {
            //when - Arrange
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var criarPedidoApplication = new CriarPedidoApplication(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
            var criarPedidoInput = new CriarPedidoInput
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
            var pedido = await criarPedidoApplication.Execute(criarPedidoInput);

            //then - Assert
            Assert.NotNull(pedido);
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
            var criarPedidoApplication = new CriarPedidoApplication(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
            var criarPedidoInput = new CriarPedidoInput
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
            var pedido = await criarPedidoApplication.Execute(criarPedidoInput);

            //then - Assert
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
            var criarPedidoApplication = new CriarPedidoApplication(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
            var criarPedidoInput = new CriarPedidoInput
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
            var pedido = await criarPedidoApplication.Execute(criarPedidoInput);

            //then - Assert
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
            var criarPedidoApplication = new CriarPedidoApplication(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi);
            var criarPedidoInput = new CriarPedidoInput
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
            var pedido = await criarPedidoApplication.Execute(criarPedidoInput);

            //then - Assert
            Assert.Equal(7400, pedido.ValorTotal);
            Assert.Equal(310, pedido.Frete);
        }
    }
}
