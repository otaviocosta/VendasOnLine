using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasOnLine.Domain;
using VendasOnLine.Infra;

namespace VendasOnLine.Application
{
    public class PedidoCommandHandler
    {
        private IPedidoRepository _pedidoRepository;
        private ICupomRepository _cupomRepository;
        private IItemRepository _itemRepository;
        private ICalculadoraCepApi _calculadoraCepApi;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, ICupomRepository cupomRepository, IItemRepository itemRepository, ICalculadoraCepApi calculadoraCepApi)
        {
            _pedidoRepository = pedidoRepository;
            _calculadoraCepApi = calculadoraCepApi;
            _cupomRepository = cupomRepository;
            _itemRepository = itemRepository;
        }

        public Pedido Handle(CriarPedidoCommand command)
        {
            var pedido = new Pedido(1, command.Cpf, command.Cep);
            _pedidoRepository.Incluir(pedido);
            return pedido;
        }

        public async void Handle(AdicionarItemCommand command)
        {
            var item = await _itemRepository.Buscar(command.Id);
            if (item == null) throw new Exception("Item não encontrado");
            var itemPedido = new ItemPedido(command.Id, item.Preco, command.Quantidade);
            var pedido = _pedidoRepository.Buscar(command.IdPedido);
            pedido.AdicionarItem(itemPedido);
        }

        public void Handle(AdicionarCupomDescontoCommand command)
        {
            var pedido = _pedidoRepository.Buscar(command.IdPedido);
            var cupom = _cupomRepository.Buscar(command.CodigoCupom);
            if (cupom != null) pedido.AdicionarCupom(cupom);
        }

        public async Task<PedidoResponse> Handle(CriarPedidoCompletoCommand command)
        {
            var seq = _pedidoRepository.ProximoSequencial();

            var pedido = new Pedido(seq, command.Cpf, command.Cep);
            var distancia = _calculadoraCepApi.Calcular("11.111-111", command.Cep);
            foreach (var itemPedido in command.Items)
            {
                var item = await _itemRepository.Buscar(itemPedido.Id);
                if (item == null) throw new Exception("Item não encontrado");
                pedido.AdicionarItem(new ItemPedido(itemPedido.Id, item.Preco, itemPedido.Quantidade));
                pedido.AdicionarFrete(CalculadoraFrete.Calcular(distancia, item) * itemPedido.Quantidade);
            }
            if (!string.IsNullOrEmpty(command.CodigoCupom))
            {
                var cupom = _cupomRepository.Buscar(command.CodigoCupom);
                if (cupom != null) pedido.AdicionarCupom(cupom);
            }

            _pedidoRepository.Incluir(pedido);
            return new PedidoResponse
            {
                Id = pedido.Id.Value,
                Cpf = pedido.Cpf.Numero,
                QuantidadeItens = pedido.QuantidadeItens(),
                ValorTotal = pedido.ValorTotal(),
                Frete = pedido.Frete
            };
        }

    }
}
