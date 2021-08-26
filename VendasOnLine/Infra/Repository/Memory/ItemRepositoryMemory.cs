using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasOnLine.Domain;

namespace VendasOnLine.Infra
{
    public class ItemRepositoryMemory : IItemRepository
    {
        private List<Item> Itens;

        public ItemRepositoryMemory()
        {
            Itens = new List<Item> {
                new Item(1, "Guitarra", 1000, 100, 50, 15, 3),
                new Item(2, "Amplificador", 5000, 50, 50, 50, 22),
                new Item(3, "Cabo", 30, 10, 10, 10, 1)
            };
        }
        public Task<Item> Buscar(int id)
        {
            return Task.FromResult(Itens.FirstOrDefault(p => p.Id.Equals(id)));
        }
    }
}
