using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasOnLine.Domain;
using VendasOnLine.Infra;

namespace VendasOnLine.Application
{
    public class CriarPedidoApplication
    {
        private IPedidoRepository _pedidoRepository;
        private ICupomRepository _cupomRepository;
        private IItemRepository _itemRepository;
        private ICalculadoraCepApi _calculadoraCepApi;

        public CriarPedidoApplication(IPedidoRepository pedidoRepository, ICupomRepository cupomRepository, IItemRepository itemRepository, ICalculadoraCepApi calculadoraCepApi)
        {
            _pedidoRepository = pedidoRepository;
            _calculadoraCepApi = calculadoraCepApi;
            _cupomRepository = cupomRepository;
            _itemRepository = itemRepository;
        }

        public async Task<CriarPedidoOutput> Execute(CriarPedidoInput input)
        {
            var seq = _pedidoRepository.UltimoSequencial() + 1;
            var pedido = new Pedido(input.Cpf, input.DataEmissao, seq);
            var distancia = _calculadoraCepApi.Calcular("11.111-111", input.Cep);
            foreach (var itemPedido in input.Items)
            {
                var item = await _itemRepository.Buscar(itemPedido.Id);
                if (item == null) throw new Exception("Item não encontrado");
                pedido.AdicionarItem(new ItemPedido(itemPedido.Id, item.Preco, itemPedido.Quantidade));
                pedido.AdicionarFrete(CalculadoraFrete.Calcular(distancia, item) * itemPedido.Quantidade);
            }
            if (!string.IsNullOrEmpty(input.CodigoCupom))
            {
                var cupom = _cupomRepository.Buscar(input.CodigoCupom);
                if (cupom != null) pedido.AdicionarCupom(cupom);
            }

            _pedidoRepository.Incluir(pedido);
            return new CriarPedidoOutput
            {
                Id = pedido.Id.Value,
                ValorTotal = pedido.ValorTotal(),
                Frete = pedido.Frete
            };
        }

    }
}
