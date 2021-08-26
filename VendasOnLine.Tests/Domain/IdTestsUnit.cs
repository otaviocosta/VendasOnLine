using System;
using VendasOnLine.Domain;
using Xunit;

namespace VendasOnLine.Tests
{
    public class IdTestsUnit
    {
        [Fact]
        [Trait("Categoria", "ValueObjects Id")]
        public void DeveCriarId()
        {
            //when - Arrange
            var seq = 123;

            //given - Act
            var id = new Id(DateTime.Now, seq);

            //then - Assert
            Assert.Equal("202100000123", id.Value);
        }
    }
}
