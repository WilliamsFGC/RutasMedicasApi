using RutasMedicas.Data.Api.interfaces;
using RutasMedicas.Entities.Api.dto;
using System.Collections.Generic;
using MongoDB.Driver;

namespace RutasMedicas.Data.Api.repositories
{
    public class EpsRepository : IEpsRepository
    {
        private readonly IMongoCollection<EpsDto> epsCollection;
        public EpsRepository(IConnection connection)
        {
            epsCollection = connection.GetCollection<EpsDto>("eps");
        }

        public IEnumerable<EpsDto> GetEps()
        {
            // Consultar las Eps
            IFindFluent<EpsDto, EpsDto> find = epsCollection.Find(f => true);
            IEnumerable<EpsDto> result = find.ToList();
            return result;
        }
    }
}
