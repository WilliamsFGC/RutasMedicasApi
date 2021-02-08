using RutasMedicas.Entities.Api.dto;
using RutasMedicas.Entities.Api.entities;
using System.Collections.Generic;

namespace RutasMedicas.Business.Api.interfaces
{
    public interface IEpsService
    {
        GenericResponse<IEnumerable<EpsDto>> GetEps(string entidad);
    }
}
