using System;

namespace VendasOnLine
{
    public class AdicionarCupomDescontoCommand
    {
        public AdicionarCupomDescontoCommand()
        {
        }
        public Id IdPedido { get; set; }
        public string CodigoCupom { get; set; }
    }
}