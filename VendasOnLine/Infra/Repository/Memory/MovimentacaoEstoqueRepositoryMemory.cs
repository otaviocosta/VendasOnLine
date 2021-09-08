using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendasOnLine.Domain
{
    public class MovimentacaoEstoqueRepositoryMemory : IMovimentacaoEstoqueRepository
    {
        private List<MovimentacaoEstoque> movimentacoesEstoque;

        public MovimentacaoEstoqueRepositoryMemory()
        {
            movimentacoesEstoque = new List<MovimentacaoEstoque>()
            {
                new MovimentacaoEstoque(1, "in", 10, new DateTime()),
                new MovimentacaoEstoque(2, "in", 10, new DateTime()),
                new MovimentacaoEstoque(3, "in", 10, new DateTime())
            };
        }

        public async Task<List<MovimentacaoEstoque>> Buscar(int idItem)
        {
            return movimentacoesEstoque.Where(m => m.IdItem == idItem).ToList();
        }

        public async void Salvar(MovimentacaoEstoque movimentacaoEstoque)
        {
            movimentacoesEstoque.Add(movimentacaoEstoque);
        }
    }
}
