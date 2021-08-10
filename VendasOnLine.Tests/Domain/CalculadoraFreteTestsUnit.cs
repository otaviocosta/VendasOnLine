using VendasOnLine.Domain;
using Xunit;

namespace VendasOnLine.Tests
{
    public class CalculadoraFreteTestsUnit
    {
        [Fact]
        [Trait("Category", "CalculadoraFrete")]
        public void DeveCalcularFreteProduto()
        {
            var item = new Item(1, "Guitarra", 1000, 100, 50, 15, 3);
            var distancia = 1000;
            var preco = CalculadoraFrete.Calcular(distancia, item);
            Assert.Equal(30D, preco);
        }

        [Fact]
        [Trait("Category", "CalculadoraFrete")]
        public void DeveCalcularFreteProduto2()
        {
            var item = new Item(2, "Amplificador", 5000, 50, 50, 50, 22);
            var distancia = 1000;
            var preco = CalculadoraFrete.Calcular(distancia, item);
            Assert.Equal(220D, preco);
        }

        [Fact]
        [Trait("Category", "CalculadoraFrete")]
        public void DeveCalcularFreteMinimo()
        {
            var item = new Item(3, "Cabo", 30, 9, 9, 9, 0.1);
            var distancia = 1000;
            var preco = CalculadoraFrete.Calcular(distancia, item);
            Assert.Equal(10D, preco);
        }
    }
}
