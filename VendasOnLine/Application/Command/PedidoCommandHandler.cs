using System;
using System.Collections.Generic;
using System.Linq;

using VendasOnLine.Domain;
using VendasOnLine.Infra;

namespace VendasOnLine.Application
{
    public class PedidoCommandHandler
    {
        private IPedidoRepository _pedidoRepository;
        private List<Cupom> _cupomRepository;
        private List<Item> _itemRepository;
        private ICalculadoraCepApi _calculadoraCepApi;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, ICalculadoraCepApi calculadoraCepApi)
        {
            _pedidoRepository = pedidoRepository;
            _cupomRepository = new List<Cupom>
            {
                new Cupom("VALE20", 20, DateTime.Now),
                new Cupom("VALEEXP", 10, DateTime.Now.AddDays(-1))
            };
            _itemRepository = new List<Item>
            {
                new Item(1, "Guitarra", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", 5000, 50, 50, 50, 22),
                new Item(3, "Cabo", 30, 10, 10, 10, 1)
            };
            _calculadoraCepApi = calculadoraCepApi;
        }

        public Pedido Handle(CriarPedidoCommand command)
        {
            var pedido = new Pedido(1, command.Cpf, command.Cep);
            _pedidoRepository.Incluir(pedido);
            return pedido;
        }

        public void Handle(AdicionarItemCommand command)
        {
            var item = _itemRepository.FirstOrDefault(i => i.Id == command.Id);
            if (item == null) throw new Exception("Item não encontrado");
            var itemPedido = new ItemPedido(command.Id, item.Preco, command.Quantidade);
            var pedido = _pedidoRepository.Buscar(command.IdPedido);
            pedido.AdicionarItem(itemPedido);
        }

        public void Handle(AdicionarCupomDescontoCommand command)
        {
            var pedido = _pedidoRepository.Buscar(command.IdPedido);
            var cupom = _cupomRepository.FirstOrDefault(c => c.Codigo.Equals(command.CodigoCupom));
            if (cupom != null) pedido.AdicionarCupom(cupom);
        }

        public PedidoResponse Handle(CriarPedidoCompletoCommand command)
        {
            var seq = _pedidoRepository.ProximoSequencial();

            var pedido = new Pedido(seq, command.Cpf, command.Cep);
            var distancia = _calculadoraCepApi.Calcular("11.111-111", command.Cep);
            foreach (var itemPedido in command.Items)
            {
                var item = _itemRepository.FirstOrDefault(i => i.Id == itemPedido.Id);
                if (item == null) throw new Exception("Item não encontrado");
                pedido.AdicionarItem(new ItemPedido(itemPedido.Id, item.Preco, itemPedido.Quantidade));
                pedido.AdicionarFrete(CalculadoraFrete.Calcular(distancia, item) * itemPedido.Quantidade);
            }
            var cupom = _cupomRepository.FirstOrDefault(c => c.Codigo.Equals(command.CodigoCupom));
            if (cupom != null) pedido.AdicionarCupom(cupom);

            _pedidoRepository.Incluir(pedido);
            return new PedidoResponse
            {
                Id = pedido.Id.Value,
                Cpf = pedido.Cpf,
                QuantidadeItens = pedido.QuantidadeItens(),
                ValorTotal = pedido.ValorTotal(),
                Frete = pedido.Frete
            };
        }

    }
}
