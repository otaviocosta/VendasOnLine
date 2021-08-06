using System;

namespace VendasOnLine
{
    public class AdicionarItemCommand
    {
        public Guid IdPedido { get; set; }
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }
}
