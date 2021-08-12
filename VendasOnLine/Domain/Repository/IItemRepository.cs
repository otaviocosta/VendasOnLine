using System.Threading.Tasks;

namespace VendasOnLine.Domain
{
    public interface IItemRepository
    {
        Task<Item> Buscar(int id);
    }
}
