using System;
using System.Collections.Generic;

namespace VendasOnLine.Domain
{
    public class CriarPedidoInput
    {
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public List<PedidoItemInput> Items { get; set; }
        public string CodigoCupom { get; set; }
        public DateTime DataEmissao { get; set; }
    }
}
