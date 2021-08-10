using VendasOnLine.Domain;

namespace VendasOnLine.Application
{
    public class PedidoQueryService
    {
        private IPedidoRepository _pedidoRepository;

        public PedidoQueryService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public PedidoDto BuscarPedido(string id)
        {
            var pedido = _pedidoRepository.Buscar(id);
            return new PedidoDto
            {
                Id = pedido.Id.Value,
                Cpf = pedido.Cpf,
                Cep = pedido.Cep,
                QuantidadeItens = pedido.QuantidadeItens(),
                ValorTotal = pedido.ValorTotal(),
                Frete = pedido.Frete
            };
        }
    }
}
