namespace VendasOnLine.Domain
{
    public class TabelaTaxas
    {
        private int idItem;
        private string tipo;
        private int valor;

        public TabelaTaxas(int idItem, string tipo, int valor)
        {
            this.idItem = idItem;
            this.tipo = tipo;
            this.valor = valor;
        }

        public int IdItem { get => idItem; }
        public string Tipo { get => tipo; }
        public int Valor { get => valor; }
    }
}
