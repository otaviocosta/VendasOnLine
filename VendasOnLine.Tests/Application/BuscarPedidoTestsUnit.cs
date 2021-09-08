using System;
using System.Collections.Generic;
using VendasOnLine.Application;
using VendasOnLine.Domain;
using VendasOnLine.Infra;
using Xunit;

namespace VendasOnLine.Tests
{
    public class BuscarPedidoTestsUnit
    {
        [Fact]
        [Trait("Categoria", "BuscarPedido")]
        public async void DeveBuscarUmPedido()
        {
            //when - Arrange
            var pedidoRepository = new PedidoRepositoryMemory();
            var cupomRepository = new CupomRepositoryMemory();
            var itemRepository = new ItemRepositoryMemory();
            var calculadoraCepApi = new CalculadoraCepApi();
            var tabelaTaxasRepository = new TabelaTaxasRepositoryMemory();
            var movimentacaoEstoqueRepository = new MovimentacaoEstoqueRepositoryMemory();
            var criarPedidoApplication = new CriarPedidoApplication(pedidoRepository, cupomRepository, itemRepository, calculadoraCepApi, tabelaTaxasRepository, movimentacaoEstoqueRepository);
            var buscarPedido = new BuscarPedidoApplication(pedidoRepository, itemRepository);
            var criarPedidoInput = new CriarPedidoInput
            {
                Cpf = "04831420000",
                Cep = "11.111-111",
                CodigoCupom = "VALE20",
                Items = new List<PedidoItemInput>
                {
                    new PedidoItemInput{Id = 1, Quantidade = 2 },
                    new PedidoItemInput{Id = 2, Quantidade =  1 },
                    new PedidoItemInput{Id = 3, Quantidade = 3 }
                }
            };
            var pedido = await criarPedidoApplication.Execute(criarPedidoInput);

            //given - Act
            var buscarPedidoOutput = await buscarPedido.Execute(pedido.Id);

            //then - Assert
            Assert.Equal(5982, buscarPedidoOutput.ValorTotal);
        }
    }
}
