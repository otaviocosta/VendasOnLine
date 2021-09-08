using System;

namespace VendasOnLine.Domain
{
    public class MovimentacaoEstoque
    {
        private int idItem;
        private string operacao;
        private double quantidade;
        private DateTime data;

        public MovimentacaoEstoque(int idItem, string operacao, double quantidade, DateTime data)
        {
            this.idItem = idItem;
            this.operacao = operacao;
            this.quantidade = quantidade;
            this.data = data;
        }

        public int IdItem { get => idItem; }
        public string Operacao { get => operacao; }
        public double Quantidade { get => quantidade; }
    }
}
