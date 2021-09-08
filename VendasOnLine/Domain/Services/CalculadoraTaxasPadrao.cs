using System.Collections.Generic;
using System.Linq;

namespace VendasOnLine.Domain
{
    public class CalculadoraTaxasPadrao : CalculadoraTaxas
    {
        public override double BuscarTaxa(List<TabelaTaxas> tabelaTaxas)
        {
            var tabela = tabelaTaxas.FirstOrDefault(t => t.Tipo == "default");
            if (tabela == null) return 0;
            return tabela.Valor;
        }
    }
}
