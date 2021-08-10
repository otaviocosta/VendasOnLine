using System;
using System.Collections.Generic;
using System.Linq;

namespace VendasOnLine
{
    public class PedidoRepository : IPedidoRepository
    {
        private List<Pedido> Pedidos;
        public PedidoRepository()
        {
            Pedidos = new List<Pedido>();
        }

        public Pedido Buscar(string id)
        {
            return Pedidos.First(p => p.Id.Value.Equals(id));
        }

        public Pedido Buscar(Id id)
        {
            return Pedidos.First(p => p.Id.Value.Equals(id.Value));
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
