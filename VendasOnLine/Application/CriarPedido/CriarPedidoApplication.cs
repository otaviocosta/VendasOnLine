using System.Threading.Tasks;
using VendasOnLine.Domain;

namespace VendasOnLine.Application
{
    public class CriarPedidoApplication
    {
        private ICalculadoraCepApi _calculadoraCepApi;
        private IItemRepository _itemRepository;
        private ICupomRepository _cupomRepository;
        private IPedidoRepository _pedidoRepository;
        private ITabelaTaxasRepository _tabelaTaxasRepository;
        private IMovimentacaoEstoqueRepository _movimentacaoEstoqueRepository;

        public CriarPedidoApplication(IPedidoRepository pedidoRepository, ICupomRepository cupomRepository, IItemRepository itemRepository, ICalculadoraCepApi calculadoraCepApi, ITabelaTaxasRepository tabelaTaxasRepository, IMovimentacaoEstoqueRepository movimentacaoEstoqueRepository)
        {
            _pedidoRepository = pedidoRepository;
            _calculadoraCepApi = calculadoraCepApi;
            _cupomRepository = cupomRepository;
            _itemRepository = itemRepository;
            _tabelaTaxasRepository = tabelaTaxasRepository;
            _movimentacaoEstoqueRepository = movimentacaoEstoqueRepository;
        }

        public async Task<CriarPedidoOutput> Execute(CriarPedidoInput input)
        {
            var pedidoService = new PedidoService(_pedidoRepository, _cupomRepository, _itemRepository, _calculadoraCepApi, _tabelaTaxasRepository, _movimentacaoEstoqueRepository);
            var pedido = await pedidoService.Criar(input);
            _pedidoRepository.Incluir(pedido);
            return new CriarPedidoOutput
            {
                Id = pedido.Id.Value,
                ValorTotal = pedido.ValorTotal(),
                Taxas = pedido.Taxas,
                Frete = pedido.Frete
            };
        }
    }
}
