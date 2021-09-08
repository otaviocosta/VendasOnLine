using System;
using System.Collections.Generic;
using VendasOnLine.Domain;
using Xunit;

namespace VendasOnLine.Tests
{
    public class CalculadoraEstoqueTestsUnit
    {
        [Fact]
        [Trait("Category", "CalculadoraEstoque")]
        public void DeveCalcularEstoqueDeUmItem()
        {
            var movimentacoesEstoque = new List<MovimentacaoEstoque>() {
                new MovimentacaoEstoque(1, "in", 3, new DateTime(2021,10,10)),
                new MovimentacaoEstoque(1, "out", 2, new DateTime(2021,10,10)),
                new MovimentacaoEstoque(1, "in", 2, new DateTime(2021,10,10))
            };
            var calculadoraEstoque = new CalculadoraEstoque();
            var quantidade = calculadoraEstoque.Calcular(movimentacoesEstoque);
            Assert.Equal(3, quantidade);
        }
    }
}
