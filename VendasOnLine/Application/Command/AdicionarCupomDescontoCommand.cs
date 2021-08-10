using VendasOnLine.Domain;

namespace VendasOnLine.Application
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