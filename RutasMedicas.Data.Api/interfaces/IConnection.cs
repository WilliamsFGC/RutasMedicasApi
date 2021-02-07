using MongoDB.Driver;

namespace RutasMedicas.Data.Api.interfaces
{
    public interface IConnection
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
