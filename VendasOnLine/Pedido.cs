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
        private List<Item> Itens;

        public Pedido(string cpf)
        {
            if (!CpfValidator.Validar(cpf)) throw new Exception("CPF inválido");
            Cpf = cpf;
            Itens = new List<Item>();
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
            return total;
        }

        public void AdicionarItem(Item item)
        {
            Itens.Add(item);
        }

        public void AdicionarCupom(Cupom cupom)
        {
            CupomDesconto = cupom;
        }
    }
}
