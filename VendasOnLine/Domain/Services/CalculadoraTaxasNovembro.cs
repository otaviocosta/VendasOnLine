using System;
using System.Collections.Generic;
using System.Linq;

namespace VendasOnLine.Domain
{
    public class CalculadoraTaxasNovembro : CalculadoraTaxas
    {
        public override double BuscarTaxa(List<TabelaTaxas> tabelaTaxas)
        {
            var tabela = tabelaTaxas.FirstOrDefault(t => t.Tipo == "novembro");
            if (tabela == null) return 0;
            return tabela.Valor;
        }
    }
}
