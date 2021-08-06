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
            Assert.Equal("CPF inv�lido", ex.Message);
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

1 - N�o deve aplicar cupom de desconto expirado
2 - Deve calcular o valor do frete com base na dist�ncia (analisando o CEP de origem e destino), as dimens�es (altura, largura e profundidade em cm) e o peso dos produtos (em kg)
3 - Deve retornar o pre�o m�nimo de frete caso ele seja superior ao valor calculado


Considere


A dist�ncia entre os dois CEPs deve ser resolvida por uma API externa
O valor do frete ser� calculado de acordo com a f�rmula
O valor m�nimo � de R$10,00
N�o existem diferentes modalidades de frete (normal, expresso, �) e a origem dos produtos � sempre a mesma, al�m disso n�o existe diferen�a no destino, se � capital ou interior, o c�lculo � feito basicamente considerando a dist�ncia, o volume e a densidade transportados

Sugest�es


Crie uma implementa��o em mem�ria para a API, que retorna sempre um valor fixo como a dist�ncia entre os CEPs

F�rmula de C�lculo do Frete

Pre�o do Frete = dist�ncia (km) * volume (m3) * (densidade/100)

Exemplos de volume ocupado (cubagem)

Camera: 20cm x 15 cm x 10 cm = 0,003 m3
Guitarra: 100cm x 30cm x 10cm = 0,03 m3
Geladeira: 200cm x 100cm x 50cm = 1 m3

Exemplos de densidade

Camera: 1kg / 0,003 m3 = 333kg/m3
Guitarra: 3kg / 0,03 m3 = 100kg/m3
Geladeira: 40kg / 1 m3 = 40kg/m3

Exemplos

dist�ncia: 1000
volume: 0,003
densidade: 333
pre�o: R$9,90 (1000 * 0,003 * (333/100))

dist�ncia: 1000
volume: 0,03
densidade: 100
pre�o: R$30,00 (1000 * 0,03 * (100/100))

dist�ncia: 1000
volume: 1
densidade: 40
pre�o: R$400,00 (1000 * 1 * (40/100))


*/