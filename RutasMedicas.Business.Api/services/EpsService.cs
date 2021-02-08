using RutasMedicas.Business.Api.interfaces;
using RutasMedicas.Data.Api.interfaces;
using RutasMedicas.Entities.Api.dto;
using RutasMedicas.Entities.Api.entities;
using System.Collections.Generic;

namespace RutasMedicas.Business.Api.services
{
    public class EpsService : IEpsService
    {
        private readonly IEpsRepository epsRepository;
        public EpsService(IEpsRepository epsRepository)
        {
            this.epsRepository = epsRepository;
        }
        public GenericResponse<IEnumerable<EpsDto>> GetEps(string entidad)
        {
            IEnumerable<EpsDto> result = epsRepository.GetEps(entidad);
            GenericResponse<IEnumerable<EpsDto>> response = new GenericResponse<IEnumerable<EpsDto>>()
            {
                Result = result
            };
            return response;
        }
    }
}
