using System.Collections.Generic;
using Xunit;

namespace VendasOnLine.Tests
{
    public class PedidoQueryServiceTestsUnit
    {
        [Fact]
        [Trait("Categoria", "PedidoQueryService")]
        public void DeveRetornarInformacoesDoPedido()
        {
            //when - Arrange
            var repository = new PedidoRepository();
            var pedidoCommandHandler = new PedidoCommandHandler(repository);
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
            var pedidoDto = pedidoCommandHandler.Handle(criarPedidoCommand);
            var pedidoQueryService = new PedidoQueryService(repository);

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
