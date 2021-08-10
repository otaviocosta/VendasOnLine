namespace VendasOnLine
{
    public interface IPedidoRepository
    {
        void Incluir(Pedido pedido);
        Pedido Buscar(string id);
        Pedido Buscar(Id id);
        int ProximoSequencial();
    }
}
