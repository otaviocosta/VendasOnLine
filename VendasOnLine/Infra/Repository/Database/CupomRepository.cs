using System.Threading.Tasks;
using Dapper;
using VendasOnLine.Domain;

namespace VendasOnLine.Infra
{
    public class CupomRepository : ICupomRepository
    {
        IDatabase database;

        public CupomRepository(IDatabase database)
        {
            this.database = database;
        }

        public async Task<Cupom> Buscar(string id)
        {
            using var db = database.CreateConnection();

            var query = @"
                    SELECT 
	                    i.Id, 
	                    i.Descricao, 
	                    i.Preco, 
	                    i.Largura, 
	                    i.Altura, 
	                    i.Profundidade, 
	                    i.Peso
                    FROM 
	                    Item AS i
                    WHERE
	                    i.Id = @ID
                ";
            return await db.QueryFirstAsync<Cupom>(query, new { ID = id });
        }
    }
}
