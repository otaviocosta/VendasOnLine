namespace VendasOnLine.Domain
{
    public interface IPedidoRepository
    {
        void Incluir(Pedido pedido);
        Pedido Buscar(string id);
        int UltimoSequencial();
    }
}
