using System;
using System.Collections.Generic;
using System.Linq;

namespace VendasOnLine
{
    public class PedidoCommandHandler
    {
        public List<Pedido> Pedidos { get; private set; }
        public List<Cupom> Cupons { get; private set; }

        public PedidoCommandHandler()
        {
            Pedidos = new List<Pedido>();
            Cupons = new List<Cupom> { new Cupom("VALE20", 20, DateTime.Now), new Cupom("VALEEXP", 10, DateTime.Now.AddDays(-1)) };
        }

        public Pedido Handle(CriarPedidoCommand command)
        {
            var pedido = new Pedido(command.Cpf);
            Pedidos.Add(pedido);
            return pedido;
        }

        public void Handle(AdicionarItemCommand command)
        {
            var item = new ItemPedido(command.Descricao, command.Valor, command.Quantidade);
            var pedido = Pedidos.First(p => p.Id.Equals(command.IdPedido));
            pedido.AdicionarItem(item);
        }

        public void Handle(AdicionarCupomDescontoCommand command)
        {
            var pedido = Pedidos.First(p => p.Id.Equals(command.IdPedido));
            var cupom = Cupons.FirstOrDefault(c => c.Codigo.Equals(command.CodigoCupom));
            if (cupom!=null) pedido.AdicionarCupom(cupom);
        }

        public Pedido Handle(CriarPedidoCompletoCommand command)
        {
            var pedido = new Pedido(command.Cpf);
            foreach (var item in command.Items)
            {
                pedido.AdicionarItem(item);
            }
            var cupom = Cupons.FirstOrDefault(c => c.Codigo.Equals(command.CodigoCupom));
            if (cupom != null) pedido.AdicionarCupom(cupom);

            Pedidos.Add(pedido);
            return pedido;
        }

    }
}
