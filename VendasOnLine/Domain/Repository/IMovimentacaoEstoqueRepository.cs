using System.Collections.Generic;
using System.Threading.Tasks;

namespace VendasOnLine.Domain
{
    public interface IMovimentacaoEstoqueRepository
    {
        Task<List<MovimentacaoEstoque>> Buscar(int idItem);
        void Salvar(MovimentacaoEstoque movimentacaoEstoque);
    }
}
