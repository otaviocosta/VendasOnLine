using System;
using System.Collections.Generic;
using System.Linq;

namespace VendasOnLine.Domain
{
    public class Pedido
    {
        Cpf cpf;
        List<ItemPedido> itens;
        Cupom cupomDesconto;
        double frete;
        double taxas;
        Id id;
        int sequencial;
        DateTime dataEmissao;

        public Pedido(string cpf, DateTime dataEmissao = new DateTime(), int sequencial = 1)
        {
            this.cpf = new Cpf(cpf);
            this.itens = new List<ItemPedido>();
            this.frete = 0;
            this.dataEmissao = dataEmissao;
            this.sequencial = sequencial;
            this.id = new Id(dataEmissao, sequencial);
        }

        public void AdicionarItem(ItemPedido item)
        {
            itens.Add(item);
        }

        public void AdicionarCupom(Cupom cupom)
        {
            if (!cupom.Expirado())
                cupomDesconto = cupom;
        }

        public double ValorTotal()
        {
            var total = itens.Sum(i => i.Total);
            total -= (total * (cupomDesconto?.Percentual ?? 0) / 100);
            total += frete;
            return total;
        }

        public void AdicionarFrete(double valor)
        {
            frete += valor;
        }

        public void AdicionarTaxas(double valor)
        {
            taxas += valor;
        }

        public Id Id { get => id;  }
        public double Frete { get => frete; }
        public double Taxas { get => taxas; }
        public IEnumerable<ItemPedido> Itens { get => itens; }
    }
}
