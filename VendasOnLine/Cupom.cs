namespace VendasOnLine
{
    public class Cupom
    {
        public string Codigo { get; private set; }
        public double Percentual { get; set; }

        public Cupom(string codigo, double percentual)
        {
            Codigo = codigo;
            Percentual = percentual;
        }
    }
}