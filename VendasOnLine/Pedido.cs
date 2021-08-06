using System;
using System.Collections.Generic;
using System.Linq;

namespace VendasOnLine
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public string Cpf { get; private set; }
        public Cupom CupomDesconto { get; private set; }
        public double Frete { get; private set; }

        private List<ItemPedido> Itens;

        public Pedido(string cpf)
        {
            if (!CpfValidator.Validar(cpf)) throw new Exception("CPF inválido");
            Cpf = cpf;
            Itens = new List<ItemPedido>();
            Id = Guid.NewGuid();
        }

        public int QuantidadeItens()
        {
            return Itens.Sum(i => i.Quantidade);
        }

        public double ValorTotal()
        {
            var total = Itens.Sum(i => i.Total);
            total -= (total * (CupomDesconto?.Percentual ?? 0) / 100);
            total += Frete;
            return total;
        }

        public void AdicionarItem(ItemPedido item)
        {
            Itens.Add(item);
        }

        public void AdicionarCupom(Cupom cupom)
        {
            if (!cupom.Expirado())
                CupomDesconto = cupom;
        }

        public void AdicionarFrete(double valor)
        {
            Frete += valor;
        }
    }
}
