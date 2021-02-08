using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using RutasMedicas.Data.Api.interfaces;
using RutasMedicas.Utilities.Api.exceptions;
using System;

namespace RutasMedicas.Data.Api.connection
{
    public class Connection : IConnection
    {
        private readonly IConfiguration configuration;
        public Connection(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            IConfigurationSection mongoDbSection = configuration.GetSection("MongoDb");
            string connectionName = mongoDbSection.GetSection("ConnectionName").Value ?? "";
            string databaseName = mongoDbSection.GetSection("DatabaseName").Value ?? "";
            string connectionString = configuration.GetConnectionString(connectionName);
            IMongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            // Verificar la conexión a la base de datos
            bool aliveService = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            if (!aliveService)
            {
                throw new MongoDbConnectionException("No se pudo conectar al servidor BD solicitado");
            }
            IMongoCollection<T> collection = database.GetCollection<T>(name);
            return collection;
        }
    }
}
