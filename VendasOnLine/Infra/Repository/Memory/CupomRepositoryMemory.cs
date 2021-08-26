using System;
using System.Collections.Generic;
using System.Linq;
using VendasOnLine.Domain;

namespace VendasOnLine.Infra
{
    public class CupomRepositoryMemory : ICupomRepository
    {
        private List<Cupom> Cupons;

        public CupomRepositoryMemory()
        {
            Cupons = new List<Cupom> {
                new Cupom("VALE20", 20, new DateTime(2021,10,10)),
                new Cupom("VALE20_EXPIRED", 20, new DateTime(2020,10,10))
            };
        }

        public Cupom Buscar(string codigo)
        {
            return Cupons.FirstOrDefault(p => p.Codigo.Equals(codigo));
        }
    }
}
