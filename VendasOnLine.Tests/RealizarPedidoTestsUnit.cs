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
            var pedidoCommandHandler = new PedidoCommandHandler();
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
            var pedidoCommandHandler = new PedidoCommandHandler();
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
            var pedidoCommandHandler = new PedidoCommandHandler();
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
            var pedidoCommandHandler = new PedidoCommandHandler();
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
            var pedidoCommandHandler = new PedidoCommandHandler();
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

/*
Testes

1 - Não deve aplicar cupom de desconto expirado
2 - Deve calcular o valor do frete com base na distância (analisando o CEP de origem e destino), as dimensões (altura, largura e profundidade em cm) e o peso dos produtos (em kg)
3 - Deve retornar o preço mínimo de frete caso ele seja superior ao valor calculado


Considere


A distância entre os dois CEPs deve ser resolvida por uma API externa
O valor do frete será calculado de acordo com a fórmula
O valor mínimo é de R$10,00
Não existem diferentes modalidades de frete (normal, expresso, …) e a origem dos produtos é sempre a mesma, além disso não existe diferença no destino, se é capital ou interior, o cálculo é feito basicamente considerando a distância, o volume e a densidade transportados

Sugestões


Crie uma implementação em memória para a API, que retorna sempre um valor fixo como a distância entre os CEPs

Fórmula de Cálculo do Frete

Preço do Frete = distância (km) * volume (m3) * (densidade/100)

Exemplos de volume ocupado (cubagem)

Camera: 20cm x 15 cm x 10 cm = 0,003 m3
Guitarra: 100cm x 30cm x 10cm = 0,03 m3
Geladeira: 200cm x 100cm x 50cm = 1 m3

Exemplos de densidade

Camera: 1kg / 0,003 m3 = 333kg/m3
Guitarra: 3kg / 0,03 m3 = 100kg/m3
Geladeira: 40kg / 1 m3 = 40kg/m3

Exemplos

distância: 1000
volume: 0,003
densidade: 333
preço: R$9,90 (1000 * 0,003 * (333/100))

distância: 1000
volume: 0,03
densidade: 100
preço: R$30,00 (1000 * 0,03 * (100/100))

distância: 1000
volume: 1
densidade: 40
preço: R$400,00 (1000 * 1 * (40/100))


*/