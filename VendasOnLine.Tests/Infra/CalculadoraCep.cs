using VendasOnLine.Infra;
using Xunit;

namespace VendasOnLine.Tests
{
    public class CalculadoraCepTestsUnit
    {
        [Fact]
        [Trait("Categoria", "CalculadoraCep")]
        public void DeveCalcularDistanciaEntreDoisCeps()
        {
            var calculadoraCepApi = new CalculadoraCepApi();
            var distancia = calculadoraCepApi.Calcular("11.111-111", "11.111-111");
            Assert.Equal(1000, distancia);
        }
    }
}
