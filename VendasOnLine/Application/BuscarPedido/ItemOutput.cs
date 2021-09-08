namespace VendasOnLine.Application
{
    public class ItemOutput
    {
        int id;
        string descricao;
        double valor;
        int quantidade;

        public ItemOutput(int id, string descricao, double valor, int quantidade)
        {
            this.id = id;
            this.descricao = descricao;
            this.valor = valor;
            this.quantidade = quantidade;
        }

        public int Id { get => id; }
        public string Descricao { get => descricao; }
        public double Valor { get => valor; }
        public int Quantidade { get => quantidade; }
    }
}
