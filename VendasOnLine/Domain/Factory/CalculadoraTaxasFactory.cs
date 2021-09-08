using System;

namespace VendasOnLine.Domain
{
    public class CalculadoraTaxasFactory
    {
        public static CalculadoraTaxas Criar(DateTime data)
        {
            if (data.Month == 11)
                return new CalculadoraTaxasNovembro();

            return new CalculadoraTaxasPadrao();
        }
    }
}
