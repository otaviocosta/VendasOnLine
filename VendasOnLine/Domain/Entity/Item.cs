namespace VendasOnLine.Domain
{
    public class Item
    {
        int id;
        string descricao;
        double preco;
        double largura;
        double altura;
        double profundidade;
        double peso;


        protected Item() { }

        public Item(int id, string descricao, double preco, double largura, double altura, double profundidade, double peso)
        {
            this.id = id;
            this.descricao = descricao;
            this.preco = preco;
            this.largura = largura;
            this.altura = altura;
            this.profundidade = profundidade;
            this.peso = peso;
        }
        
        public int Id { get => id; }        
        public string Descricao { get => descricao; }
        public double Preco { get => preco; }
        public double Volume() => largura / 100 * altura / 100 * profundidade / 100;
        public double Densidade() => peso / Volume();

    }
}
