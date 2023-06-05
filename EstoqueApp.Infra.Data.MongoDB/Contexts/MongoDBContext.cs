using EstoqueApp.Application.Models.Queries;
using EstoqueApp.Infra.Data.MongoDB.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Infra.Data.MongoDB.Contexts
{
    public class MongoDBContext
    {
        private readonly MongoDBSettings _mongoDBSettings;
        private IMongoDatabase? _mongoDataBase;

        public MongoDBContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings.Value;

            #region Conexão com o banco de dados

            var client = MongoClientSettings.FromUrl(new MongoUrl(_mongoDBSettings.Host));
            if (_mongoDBSettings.IsSSL)
                client.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                };

            _mongoDataBase = new MongoClient(client)
                .GetDatabase(_mongoDBSettings.Name);

            #endregion
        }

        public IMongoCollection<EstoqueQuery> Estoques
            => _mongoDataBase.GetCollection<EstoqueQuery>("estoques");
        public IMongoCollection<ProdutoQuery> Produtos
            => _mongoDataBase.GetCollection<ProdutoQuery>("produtos");
    }
}
