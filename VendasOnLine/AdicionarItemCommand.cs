using System;

namespace VendasOnLine
{
    public class AdicionarItemCommand
    {
        public Id IdPedido { get; set; }
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }
}
