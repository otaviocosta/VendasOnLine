using System;

namespace VendasOnLine
{
    public class AdicionarItemCommand
    {
        public Guid IdPedido { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
    }
}
