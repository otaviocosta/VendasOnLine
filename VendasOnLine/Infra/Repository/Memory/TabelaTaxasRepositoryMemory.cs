using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendasOnLine.Domain
{
    public class TabelaTaxasRepositoryMemory : ITabelaTaxasRepository
    {
        private List<TabelaTaxas> tabelas;

        public TabelaTaxasRepositoryMemory()
        {
            tabelas = new List<TabelaTaxas>()
            {
                new TabelaTaxas(1, "default", 15),
                new TabelaTaxas(2, "default", 15),
                new TabelaTaxas(3, "default", 5),
                new TabelaTaxas(1, "novembro", 5),
                new TabelaTaxas(2, "novembro", 5),
                new TabelaTaxas(3, "novembro", 1),
            };
        }

        public async Task<List<TabelaTaxas>> Buscar(int idItem)
        {
            return tabelas.Where(m => m.IdItem == idItem).ToList();
        }
    }
}
