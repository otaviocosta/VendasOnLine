using System;

namespace VendasOnLine
{
    public class AdicionarCupomDescontoCommand
    {
        public AdicionarCupomDescontoCommand()
        {
        }
        public Guid IdPedido { get; set; }
        public string CodigoCupom { get; set; }
    }
}