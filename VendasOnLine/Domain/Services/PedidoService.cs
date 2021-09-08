using System;
using System.Threading.Tasks;

namespace VendasOnLine.Domain
{
    public class PedidoService
    {
        private ICalculadoraCepApi _calculadoraCepApi;
        private IItemRepository _itemRepository;
        private ICupomRepository _cupomRepository;
        private IPedidoRepository _pedidoRepository;
        private ITabelaTaxasRepository _tabelaTaxasRepository;
        private IMovimentacaoEstoqueRepository _movimentacaoEstoqueRepository;

        public PedidoService(IPedidoRepository pedidoRepository, ICupomRepository cupomRepository, IItemRepository itemRepository, ICalculadoraCepApi calculadoraCepApi, ITabelaTaxasRepository tabelaTaxasRepository, IMovimentacaoEstoqueRepository movimentacaoEstoqueRepository)
        {
            _pedidoRepository = pedidoRepository;
            _calculadoraCepApi = calculadoraCepApi;
            _cupomRepository = cupomRepository;
            _itemRepository = itemRepository;
            _tabelaTaxasRepository = tabelaTaxasRepository;
            _movimentacaoEstoqueRepository = movimentacaoEstoqueRepository;
        }

        public async Task<Pedido> Criar(CriarPedidoInput input)
        {
            var seq = await _pedidoRepository.UltimoSequencial() + 1;
            var pedido = new Pedido(input.Cpf, input.DataEmissao, seq);
            var distancia = _calculadoraCepApi.Calcular("11.111-111", input.Cep);
            var calculadoraTaxas = CalculadoraTaxasFactory.Criar(input.DataEmissao);
            var calculadoraEstoque = new CalculadoraEstoque();
            foreach (var itemPedido in input.Items)
            {
                var item = await _itemRepository.Buscar(itemPedido.Id);
                if (item == null) throw new Exception("Item não encontrado");
                pedido.AdicionarItem(new ItemPedido(itemPedido.Id, item.Preco, itemPedido.Quantidade));
                pedido.AdicionarFrete(CalculadoraFrete.Calcular(distancia, item) * itemPedido.Quantidade);
                var tabelasTaxas = await _tabelaTaxasRepository.Buscar(item.Id);
                var taxas = calculadoraTaxas.Calcular(item, tabelasTaxas);
                pedido.AdicionarTaxas(taxas * itemPedido.Quantidade);
                var movimentacoes = await _movimentacaoEstoqueRepository.Buscar(item.Id);
                var quantidade = calculadoraEstoque.Calcular(movimentacoes);
                if (quantidade < itemPedido.Quantidade) throw new Exception("Item sem estoque");
                _movimentacaoEstoqueRepository.Salvar(new MovimentacaoEstoque(item.Id, "out", itemPedido.Quantidade, input.DataEmissao));
            }
            if (!string.IsNullOrEmpty(input.CodigoCupom))
            {
                var cupom = await _cupomRepository.Buscar(input.CodigoCupom);
                if (cupom != null) pedido.AdicionarCupom(cupom);
            }
            
            return pedido;
        }
    }
}
