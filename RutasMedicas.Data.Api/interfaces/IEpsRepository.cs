using RutasMedicas.Entities.Api.dto;
using System.Collections.Generic;

namespace RutasMedicas.Data.Api.interfaces
{
    public interface IEpsRepository
    {
        IEnumerable<EpsDto> GetEps(string entidad);
    }
}
