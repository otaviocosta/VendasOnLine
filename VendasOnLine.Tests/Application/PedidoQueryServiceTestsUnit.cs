using System.Collections.Generic;
using VendasOnLine.Application;
using VendasOnLine.Domain;
using VendasOnLine.Infra;
using Xunit;

namespace VendasOnLine.Tests
{
    public class PedidoQueryServiceTestsUnit
    {
        [Fact]
        [Trait("Categoria", "PedidoQueryService")]
        public async void DeveRetornarInformacoesDoPedido()
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
            var pedidoDto = await pedidoCommandHandler.Handle(criarPedidoCommand);
            var pedidoQueryService = new PedidoQueryService(pedidoRepository);

            //given - Act
            var pedido = pedidoQueryService.BuscarPedido(pedidoDto.Id);

            //then - Assert
            Assert.Equal(pedidoDto.Id, pedido.Id);
            Assert.Equal(criarPedidoCommand.Cpf, pedido.Cpf);
            Assert.Equal(criarPedidoCommand.Cep, pedido.Cep);
            Assert.Equal(6, pedido.QuantidadeItens);
            Assert.Equal(7400, pedido.ValorTotal);
            Assert.Equal(310, pedido.Frete);
        }
    }
}
