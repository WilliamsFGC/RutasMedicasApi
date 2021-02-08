using RutasMedicas.Data.Api.interfaces;
using RutasMedicas.Entities.Api.dto;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RutasMedicas.Data.Api.repositories
{
    public class EpsRepository : IEpsRepository
    {
        private readonly IMongoCollection<EpsDto> epsCollection;
        public EpsRepository(IConnection connection)
        {
            epsCollection = connection.GetCollection<EpsDto>("eps");
        }

        public IEnumerable<EpsDto> GetEps(string entidad)
        {
            // Consultar las Eps
            entidad = entidad ?? "";
            FilterDefinition<EpsDto> filter = Builders<EpsDto>.Filter.Regex(f => f.Entidad, new BsonRegularExpression(entidad, "i"));
            IFindFluent<EpsDto, EpsDto> find = epsCollection.Find(filter: filter);
            IEnumerable<EpsDto> result = find.ToList();
            return result;
        }
    }
}
