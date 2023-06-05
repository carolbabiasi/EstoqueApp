using EstoqueApp.Application.Interfaces.Persistences;
using EstoqueApp.Application.Models.Queries;
using EstoqueApp.Infra.Data.MongoDB.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Infra.Data.MongoDB.Persistences
{
    public class ProdutoPersintences : IProdutoPersinteces
    {

        private readonly MongoDBContext? _mongoDBContext;

        public ProdutoPersintences(MongoDBContext? mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        public void Add(ProdutoQuery model)
        {
            _mongoDBContext.Produtos.InsertOne(model);
        }

        public void Delete(ProdutoQuery model)
        {
            var filter = Builders<ProdutoQuery>.Filter.Eq(e => e.Id, model.Id);
            _mongoDBContext.Produtos.DeleteOne(filter);
        }

        public List<ProdutoQuery> GetAll()
        {
            var filter = Builders<ProdutoQuery>.Filter.Where(e => true);
            return _mongoDBContext.Produtos.Find(filter).ToList();
        }

        public ProdutoQuery GetById(Guid id)
        {
            var filter = Builders<ProdutoQuery>.Filter.Eq(e => e.Id, id);
            return _mongoDBContext.Produtos.Find(filter).FirstOrDefault();
        }

        public void Update(ProdutoQuery model)
        {
            var filter = Builders<ProdutoQuery>.Filter.Eq(e => e.Id, model.Id);
            _mongoDBContext.Produtos.ReplaceOne(filter, model);
        }
    }
}
