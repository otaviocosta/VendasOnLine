using Xunit;

namespace VendasOnLine.Tests
{
    public class ItemTestsUnit
    {
        [Fact]
        [Trait("Categoria", "Item")]
        public void DeveCalcularVolumeDeUmItem()
        {
            var item = new Item(1, "Amplificador", 5000, 50, 50, 50, 22);
            var volume = item.Volume();
            Assert.Equal(0.125, volume);
        }

        [Fact]
        [Trait("Categoria", "Item")]
        public void DeveCalcularDensidadeDeUmItem()
        {
            var item = new Item(1, "Amplificador", 5000, 50, 50, 50, 22);
            var densidade = item.Densidade();
            Assert.Equal(176D, densidade);
        }
    }
}
