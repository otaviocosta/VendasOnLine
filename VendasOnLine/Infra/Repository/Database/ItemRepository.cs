﻿using System.Threading.Tasks;
using Dapper;
using VendasOnLine.Domain;

namespace VendasOnLine.Infra
{
    public class ItemRepository : IItemRepository
    {
        IDatabase database;

        public ItemRepository(IDatabase database)
        {
            this.database = database;
        }

        public async Task<Item> Buscar(int id)
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
            return await db.QueryFirstAsync<Item>(query, new { ID = id });
        }
    }
}
