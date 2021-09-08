using System.Threading.Tasks;

namespace VendasOnLine.Domain
{
    public interface IPedidoRepository
    {
        void Incluir(Pedido pedido);
        Task<Pedido> Buscar(string id);
        Task<int> UltimoSequencial();
    }
}
