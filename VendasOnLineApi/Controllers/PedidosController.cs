using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VendasOnLine.Application;
using VendasOnLine.Domain;
using System.Threading.Tasks;
using System;
using System.Net;

namespace VendasOnLineApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> logger;
        private IItemRepository _itemRepository;
        private IPedidoRepository _pedidoRepository;

        private ICalculadoraCepApi _calculadoraCepApi;
        private ICupomRepository _cupomRepository;
        private ITabelaTaxasRepository _tabelaTaxasRepository;
        private IMovimentacaoEstoqueRepository _movimentacaoEstoqueRepository;

        public PedidosController(ILogger<PedidosController> logger, IItemRepository itemRepository, IPedidoRepository pedidoRepository, ICalculadoraCepApi calculadoraCepApi, ICupomRepository cupomRepository, ITabelaTaxasRepository tabelaTaxasRepository, IMovimentacaoEstoqueRepository movimentacaoEstoqueRepository)
        {
            this.logger = logger;
            _itemRepository = itemRepository;
            _pedidoRepository = pedidoRepository;
            _calculadoraCepApi = calculadoraCepApi;
            _cupomRepository = cupomRepository;
            _tabelaTaxasRepository = tabelaTaxasRepository;
            _movimentacaoEstoqueRepository = movimentacaoEstoqueRepository;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BuscarPedidoOutput), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            logger.LogInformation("Buscando pedido {0}", id);
            BuscarPedidoApplication buscarPedidoApplication = new BuscarPedidoApplication(_pedidoRepository, _itemRepository);
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var pedido = await buscarPedidoApplication.Execute(id);
            return pedido == null ? NotFound() : Ok(pedido);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CriarPedidoOutput), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CriarPedidoInput input)
        {
            logger.LogInformation("Criando pedido {0}", input);
            CriarPedidoApplication criarPedidoApplication = new CriarPedidoApplication(_pedidoRepository, _cupomRepository, _itemRepository, _calculadoraCepApi, _tabelaTaxasRepository, _movimentacaoEstoqueRepository);
            var pedido = await criarPedidoApplication.Execute(input);
            return CreatedAtAction("Get", new { id = pedido.Id }, pedido);  
        }
    }
}
