using System.Threading.Tasks;
using Dapper;
using VendasOnLine.Domain;

namespace VendasOnLine.Infra
{
    public class ItemRepository : Database, IItemRepository
    {
        public async Task<Item> Buscar(int id)
        {
            using var db = CreateConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=VendasOnLine;Integrated Security=True");

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
            return await db.QueryFirstAsync<Item>(query, new { ID = id });
        }
    }
}
