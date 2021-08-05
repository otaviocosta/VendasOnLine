using System;

namespace VendasOnLine
{
    public class Cupom
    {
        public string Codigo { get; private set; }
        public double Percentual { get; private set; }
        public DateTime DataExpiracao { get; private set; }

        public Cupom(string codigo, double percentual, DateTime dataExpiracao)
        {
            if (string.IsNullOrEmpty(codigo)) throw new Exception("Código inválido");
            if (percentual < 0 || percentual > 100) throw new Exception("Percentual de desconto inválido");
            if (dataExpiracao == default) throw new Exception("Data inválida");

            Codigo = codigo;
            Percentual = percentual;
            DataExpiracao = dataExpiracao;
        }

        public bool Expirado() => DataExpiracao < DateTime.Today;
    }
}