using System.Collections.Generic;

namespace VendasOnLine.Domain
{
    public class CalculadoraEstoque
    {
        public double Calcular(List<MovimentacaoEstoque> movimentacoesEstoque)
        {
            double quantidade = 0;
            foreach (var movimentacao in movimentacoesEstoque)
            {
                quantidade += movimentacao.Operacao == "in" ? movimentacao.Quantidade : movimentacao.Quantidade * -1;
            }

            return quantidade;
        }
    }
}
