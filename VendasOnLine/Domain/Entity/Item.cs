namespace VendasOnLine.Domain
{
    public class Item
    {
        string descricao;
        double largura;
        double altura;
        double profundidade;
        double peso;

        protected Item() { }

        public Item(int id, string descricao, double preco, double largura, double altura, double profundidade, double peso)
        {
            Id = id;
            this.descricao = descricao;
            Preco = preco;
            this.largura = largura;
            this.altura = altura;
            this.profundidade = profundidade;
            this.peso = peso;
        }

        public int Id { get; private set; }
        public double Preco { get; private set; }

        public double Densidade() => peso / Volume();

        public double Volume() => largura / 100 * altura / 100 * profundidade / 100;
    }
}
