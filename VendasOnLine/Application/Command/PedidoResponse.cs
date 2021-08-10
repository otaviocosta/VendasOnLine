namespace VendasOnLine.Application
{
    public class PedidoResponse
    {
        public string Id { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public double ValorTotal { get; set; }
        public int QuantidadeItens { get; set; }
        public double Frete { get; set; }
    }
}
