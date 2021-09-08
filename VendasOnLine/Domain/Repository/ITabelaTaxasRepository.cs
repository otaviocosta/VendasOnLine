using System.Collections.Generic;
using System.Threading.Tasks;

namespace VendasOnLine.Domain
{
    public interface ITabelaTaxasRepository
    {
        Task<List<TabelaTaxas>> Buscar(int idItem);
    }
}
