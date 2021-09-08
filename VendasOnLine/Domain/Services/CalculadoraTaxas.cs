using System.Collections.Generic;

namespace VendasOnLine.Domain
{
    public abstract class CalculadoraTaxas
    {
        public double Calcular(Item item, List<TabelaTaxas> tabelaTaxas)
        {
            return (item.Preco * BuscarTaxa(tabelaTaxas)) / 100;
        }

        public abstract double BuscarTaxa(List<TabelaTaxas> tabelaTaxas);
    }
}
