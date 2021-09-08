using System.Collections.Generic;

namespace VendasOnLine.Application
{
    public class BuscarPedidoOutput
    {
        public string Id { get; set; }
        public double ValorTotal { get; set; }
        public double Frete { get; set; }
        public double Taxas { get; set; }
        public List<ItemOutput> Itens { get; set; }
    }
}
