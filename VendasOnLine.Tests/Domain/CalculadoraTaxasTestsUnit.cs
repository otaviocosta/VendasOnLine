using System.Collections.Generic;
using System;
using VendasOnLine.Domain;
using Xunit;

namespace VendasOnLine.Tests.Domain
{
    public class CalculadoraTaxasTestsUnit
    {
        [Fact]
        [Trait("Category", "CalculadoraTaxas")]
        public void DeveCalcularImpostoDeUmItemGuitarraEmMesesNormais()
        {
            var item = new Item(1, "Guitarra", 1000, 100, 50, 30, 8);
            var tabelaTaxas = new List<TabelaTaxas>() { new TabelaTaxas(1, "default", 15), new TabelaTaxas(1, "novembro", 5), };
            var data = new DateTime(2021, 10, 10);
            var calculadoraTaxas = CalculadoraTaxasFactory.Criar(data);
            var total = calculadoraTaxas.Calcular(item, tabelaTaxas);
            Assert.Equal(150, total);
        }

        [Fact]
        [Trait("Category", "CalculadoraTaxas")]
        public void DeveCalcularImpostoDeUmItemGuitarraEmNovembro()
        {
            var item = new Item(1, "Guitarra", 1000, 100, 50, 30, 8);
            var tabelaTaxas = new List<TabelaTaxas>() { new TabelaTaxas(1, "default", 15), new TabelaTaxas(1, "novembro", 5), };
            var data = new DateTime(2021, 11, 10);
            var calculadoraTaxas = CalculadoraTaxasFactory.Criar(data);
            var total = calculadoraTaxas.Calcular(item, tabelaTaxas);
            Assert.Equal(50, total);
        }

        [Fact]
        [Trait("Category", "CalculadoraTaxas")]
        public void DeveCalcularImpostoDeUmItemCaboEmMesesNormais()
        {
            var item = new Item(3, "Cabo", 30, 10, 10, 10, 1);
            var tabelaTaxas = new List<TabelaTaxas>() { new TabelaTaxas(3, "default", 5), new TabelaTaxas(3, "novembro", 1), };
            var data = new DateTime(2021, 10, 10);
            var calculadoraTaxas = CalculadoraTaxasFactory.Criar(data);
            var total = calculadoraTaxas.Calcular(item, tabelaTaxas);
            Assert.Equal(1.5, total);
        }

        [Fact]
        [Trait("Category", "CalculadoraTaxas")]
        public void DeveCalcularImpostoDeUmItemCaboEmNovembro()
        {
            var item = new Item(3, "Cabo", 30, 10, 10, 10, 1);
            var tabelaTaxas = new List<TabelaTaxas>() { new TabelaTaxas(3, "default", 5), new TabelaTaxas(3, "novembro", 1), };
            var data = new DateTime(2021, 11, 10);
            var calculadoraTaxas = CalculadoraTaxasFactory.Criar(data);
            var total = calculadoraTaxas.Calcular(item, tabelaTaxas);
            Assert.Equal(0.3, total);
        }
    }
}
