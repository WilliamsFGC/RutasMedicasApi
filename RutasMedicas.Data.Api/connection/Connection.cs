using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RutasMedicas.Data.Api.interfaces;

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
            IMongoCollection<T> collection = database.GetCollection<T>(name);
            return collection;
        }
    }
}
