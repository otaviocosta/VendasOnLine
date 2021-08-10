using VendasOnLine.Domain;
using Xunit;

namespace VendasOnLine.Tests
{
    public class CPFTestsUnit
    {
        [Theory]
        [InlineData("01234567890")]
        [InlineData("398.031.720-00")]
        [InlineData("36898600087 ")]
        [Trait("Categoria", "ValidarCPFs")]
        public void CPF_TestarCPFValidos(string cpf)
        {
            var valido = CpfValidator.Validar(cpf);

            Assert.True(valido);
        }

        [Theory]
        [InlineData("39485394394")]
        [InlineData("34759347534")]
        [InlineData("83765483475")]
        [InlineData("83765483abc")]
        [InlineData("8376548347")]
        [InlineData("837654834752")]
        [InlineData(null)]
        [Trait("Categoria", "ValidarCPFs")]
        public void CPF_TestarCPFInvalidos(string cpf)
        {
            var valido = CpfValidator.Validar(cpf);

            Assert.False(valido);
        }

        [Theory]
        [InlineData("00000000000")]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        [InlineData("44444444444")]
        [InlineData("55555555555")]
        [InlineData("66666666666")]
        [InlineData("77777777777")]
        [InlineData("88888888888")]
        [InlineData("99999999999")]
        [Trait("Categoria", "ValidarCPFs")]
        public void CPF_TestarCPFCaracteresIguais(string cpf)
        {
            var valido = CpfValidator.Validar(cpf);

            Assert.False(valido);
        }
    }
}
