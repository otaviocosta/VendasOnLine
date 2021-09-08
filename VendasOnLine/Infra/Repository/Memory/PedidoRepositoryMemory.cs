using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasOnLine.Domain;

namespace VendasOnLine.Infra
{
    public class PedidoRepositoryMemory : IPedidoRepository
    {
        private List<Pedido> Pedidos;
        public PedidoRepositoryMemory()
        {
            Pedidos = new List<Pedido>();
        }

        public async Task<Pedido> Buscar(string id)
        {
            return await Task.FromResult(Pedidos.FirstOrDefault(p => p.Id.Value.Equals(id)));
        }

        public async Task<Pedido> Buscar(Id id)
        {
            return await Task.FromResult(Pedidos.FirstOrDefault(p => p.Id.Value.Equals(id.Value)));
        }

        public void Incluir(Pedido pedido)
        {
            Pedidos.Add(pedido);
        }

        public async Task<int> UltimoSequencial()
        {
            var id = Pedidos.OrderBy(m => m.Id.Value).LastOrDefault()?.Id.Value[^8..] ?? "0";
            var seq = Convert.ToInt32(id);
            return await Task.FromResult(seq);
        }
    }
}
