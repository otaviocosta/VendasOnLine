using System;

namespace VendasOnLine
{
    public class Item
    {
        string descricao;
        double largura;
        double altura;
        double profundidade;
        double peso;

        public Item(int id, string descricao, double preco, double largura, double altura, double profundidade, double peso)
        {
            this.Id = id;
            this.descricao = descricao;
            this.Preco = preco;
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
