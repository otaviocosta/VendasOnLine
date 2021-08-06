using System;

namespace VendasOnLine
{
    public static class CalculadoraFrete
    {
        public static double Calcular(double distancia, Item item)
        {
            var preco = distancia * item.Volume() * (item.Densidade() / 100);
            return preco < 10 ? 10 : preco;
        }
    }
}