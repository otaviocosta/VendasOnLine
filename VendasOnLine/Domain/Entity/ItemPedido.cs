namespace VendasOnLine.Domain
{
    public class ItemPedido
    {
        int id;
        double valor;
        int quantidade;

        public ItemPedido(int id, double valor, int quantidade)
        {
            this.id = id;
            this.valor = valor;
            this.quantidade = quantidade;
        }
        public double Total => quantidade * valor;
    }
}
