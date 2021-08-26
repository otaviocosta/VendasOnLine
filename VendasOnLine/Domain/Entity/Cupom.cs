using System;

namespace VendasOnLine.Domain
{
    public class Cupom
    {
        string codigo;
        double percentual;
        DateTime dataExpiracao;

        public Cupom(string codigo, double percentual, DateTime dataExpiracao)
        {
            if (string.IsNullOrEmpty(codigo)) throw new Exception("Código inválido");
            if (percentual < 0 || percentual > 100) throw new Exception("Percentual de desconto inválido");
            if (dataExpiracao == default) throw new Exception("Data inválida");

            this.codigo = codigo;
            this.percentual = percentual;
            this.dataExpiracao = dataExpiracao;
        }

        public string Codigo { get => codigo; }

        public double Percentual { get => percentual; }

        public bool Expirado() => dataExpiracao < DateTime.Today;
    }
}