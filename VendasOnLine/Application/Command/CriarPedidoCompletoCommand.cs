using System.Collections.Generic;

namespace VendasOnLine.Application
{
    public class CriarPedidoCompletoCommand
    {
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public string CodigoCupom { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
