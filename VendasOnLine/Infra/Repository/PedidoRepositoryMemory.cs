using System;
using System.Collections.Generic;
using System.Linq;

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

        public Pedido Buscar(string id)
        {
            return Pedidos.FirstOrDefault(p => p.Id.Value.Equals(id));
        }

        public Pedido Buscar(Id id)
        {
            return Pedidos.FirstOrDefault(p => p.Id.Value.Equals(id.Value));
        }

        public void Incluir(Pedido pedido)
        {
            Pedidos.Add(pedido);
        }

        public int ProximoSequencial()
        {
            var id = Pedidos.OrderBy(m => m.Id.Value).LastOrDefault()?.Id.Value[^8..] ?? "0";
            var seq = Convert.ToInt32(id);
            return ++seq;
        }
    }
}
