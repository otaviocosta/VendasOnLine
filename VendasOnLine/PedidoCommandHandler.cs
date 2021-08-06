using System;
using System.Collections.Generic;
using System.Linq;

namespace VendasOnLine
{
    public class PedidoCommandHandler
    {
        private List<Pedido> Pedidos;
        private List<Cupom> Cupons;
        private List<Item> Itens;
        private ICalculadoraCepApi calculadoraCepApi;

        public PedidoCommandHandler()
        {
            Pedidos = new List<Pedido>();
            Cupons = new List<Cupom>
            {
                new Cupom("VALE20", 20, DateTime.Now),
                new Cupom("VALEEXP", 10, DateTime.Now.AddDays(-1))
            };
            Itens = new List<Item>
            {
                new Item(1, "Guitarra", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", 5000, 50, 50, 50, 22),
                new Item(3, "Cabo", 30, 10, 10, 10, 1)
            };
            calculadoraCepApi = new CalculadoraCepApi();
        }

        public Pedido Handle(CriarPedidoCommand command)
        {
            var pedido = new Pedido(command.Cpf);
            Pedidos.Add(pedido);
            return pedido;
        }

        public void Handle(AdicionarItemCommand command)
        {
            var item = Itens.FirstOrDefault(i => i.Id == command.Id);
            if (item == null) throw new Exception("Item não encontrado");
            var itemPedido = new ItemPedido(command.Id, item.Preco, command.Quantidade);
            var pedido = Pedidos.First(p => p.Id.Equals(command.IdPedido));
            pedido.AdicionarItem(itemPedido);
        }

        public void Handle(AdicionarCupomDescontoCommand command)
        {
            var pedido = Pedidos.First(p => p.Id.Equals(command.IdPedido));
            var cupom = Cupons.FirstOrDefault(c => c.Codigo.Equals(command.CodigoCupom));
            if (cupom != null) pedido.AdicionarCupom(cupom);
        }

        public PedidoDto Handle(CriarPedidoCompletoCommand command)
        {
            var pedido = new Pedido(command.Cpf);
            var distancia = calculadoraCepApi.Calcular("11.111-111", command.Cep);
            foreach (var itemPedido in command.Items)
            {
                var item = Itens.FirstOrDefault(i => i.Id == itemPedido.Id);
                if (item == null) throw new Exception("Item não encontrado");
                pedido.AdicionarItem(new ItemPedido(itemPedido.Id, item.Preco, itemPedido.Quantidade));
                pedido.AdicionarFrete(CalculadoraFrete.Calcular(distancia, item) * itemPedido.Quantidade);
            }
            var cupom = Cupons.FirstOrDefault(c => c.Codigo.Equals(command.CodigoCupom));
            if (cupom != null) pedido.AdicionarCupom(cupom);

            Pedidos.Add(pedido);
            return new PedidoDto
            {
                Cpf = pedido.Cpf,
                QuantidadeItens = pedido.QuantidadeItens(),
                ValorTotal = pedido.ValorTotal(),
                Frete = pedido.Frete
            };
        }

    }
}
