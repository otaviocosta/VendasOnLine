using System.Collections.Generic;
using System.Threading.Tasks;
using VendasOnLine.Domain;

namespace VendasOnLine.Application
{
    public class BuscarPedidoApplication
    {
        private IItemRepository _itemRepository;
        private IPedidoRepository _pedidoRepository;

        public BuscarPedidoApplication(IPedidoRepository pedidoRepository, IItemRepository itemRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemRepository = itemRepository;
        }

        public async Task<BuscarPedidoOutput> Execute(string id)
        {
            var pedido = await _pedidoRepository.Buscar(id);
            var itens = new List<ItemOutput>();
            foreach (var itemPedido in pedido.Itens)
            {
                var item = await _itemRepository.Buscar(itemPedido.Id);
                var itemOutput = new ItemOutput(item.Id, item.Descricao, itemPedido.Valor, itemPedido.Quantidade);
                itens.Add(itemOutput);
            }

            return new BuscarPedidoOutput
            {
                Id = pedido.Id.Value,
                ValorTotal = pedido.ValorTotal(),
                Taxas = pedido.Taxas,
                Frete = pedido.Frete,
                Itens = itens
            };
        }
    }
}
