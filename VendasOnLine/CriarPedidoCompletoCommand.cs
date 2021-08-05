using System.Collections.Generic;

namespace VendasOnLine
{
    public class CriarPedidoCompletoCommand
    {
        public string Cpf { get; set; }
        public string CodigoCupom { get; set; }
        public List<ItemPedido> Items { get; set; }
    }
}
