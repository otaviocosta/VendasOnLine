using System;
using VendasOnLine.Domain;
using Xunit;

namespace VendasOnLine.Tests
{
    public class CupomTestsUnit
    {
        [Fact]
        [Trait("Category", "Cupom")]
        public void CriarCupom()
        {
            //when - Arrange
            var codigoCupom = "codigo";

            //given - Act
            var cupom = new Cupom(codigoCupom, 20, DateTime.Today);

            //then - Assert
            Assert.False(cupom.Expirado());
        }

        [Fact]
        [Trait("Category", "Cupom")]
        public void CriarCupomExpirado()
        {
            //when - Arrange
            var codigoCupom = "codigo";

            //given - Act
            var cupom = new Cupom(codigoCupom, 20, DateTime.Today.AddDays(-1));

            //then - Assert
            Assert.True(cupom.Expirado());
        }

        [Fact]
        [Trait("Category", "Cupom")]
        public void CriarCupomInvalido_Codigo()
        {
            //when - Arrange
            //given - Act
            var ex = Assert.Throws<Exception>(() => new Cupom("", 110, new DateTime()));

            //then - Assert
            Assert.Equal("Código inválido", ex.Message);
        }

        [Fact]
        [Trait("Category", "Cupom")]
        public void CriarCupomInvalido_Percentual()
        {
            //when - Arrange
            //given - Act
            var ex = Assert.Throws<Exception>(() => new Cupom("Codigo", 110, new DateTime()));

            //then - Assert
            Assert.Equal("Percentual de desconto inválido", ex.Message);
        }

        [Fact]
        [Trait("Category", "Cupom")]
        public void CriarCupomInvalido_Data()
        {
            //when - Arrange
            //given - Act
            var ex = Assert.Throws<Exception>(() => new Cupom("Codigo", 10, new DateTime()));

            //then - Assert
            Assert.Equal("Data inválida", ex.Message);
        }
    }
}
