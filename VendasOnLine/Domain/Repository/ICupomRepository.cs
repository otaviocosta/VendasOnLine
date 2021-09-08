using System.Threading.Tasks;

namespace VendasOnLine.Domain
{
    public interface ICupomRepository
    {
        Task<Cupom> Buscar(string id);
    }
}
