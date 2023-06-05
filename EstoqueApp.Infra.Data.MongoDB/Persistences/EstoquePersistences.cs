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
    public class EstoquePersistences : IEstoquePersistences
    {

        private readonly MongoDBContext? _mongoDBContext;

        public EstoquePersistences(MongoDBContext? mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        public void Add(EstoqueQuery model)
        {
            _mongoDBContext.Estoques.InsertOne(model);
        }

        public void Delete(EstoqueQuery model)
        {
            var filter = Builders<EstoqueQuery>.Filter.Eq(e => e.Id, model.Id);
            _mongoDBContext.Estoques.DeleteOne(filter);
        }

        public List<EstoqueQuery> GetAll()
        {
            var filter = Builders<EstoqueQuery>.Filter.Where(e => true);
            return _mongoDBContext.Estoques.Find(filter).ToList();
        }

        public EstoqueQuery GetById(Guid id)
        {
            var filter = Builders<EstoqueQuery>.Filter.Eq(e => e.Id, id);
            return _mongoDBContext.Estoques.Find(filter).FirstOrDefault();

        }

        public void Update(EstoqueQuery model)
        {
            var filter = Builders<EstoqueQuery>.Filter.Eq(e => e.Id, model.Id);
            _mongoDBContext.Estoques.ReplaceOne(filter, model);
        }
    }
}
