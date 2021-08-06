namespace VendasOnLine
{
    public class ItemPedido
    {
        public int Id { get; private set; }
        public double Valor { get; private set; }
        public int Quantidade { get; private set; }
        public double Total => Quantidade * Valor;

        public ItemPedido(int id, double valor, int quantidade)
        {
            Id = id;
            Valor = valor;
            Quantidade = quantidade;
        }
    }
}
