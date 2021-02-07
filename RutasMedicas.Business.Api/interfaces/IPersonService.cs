using RutasMedicas.Entities.Api.dto;
using RutasMedicas.Entities.Api.entities;
using System.Collections.Generic;

namespace RutasMedicas.Business.Api.interfaces
{
    public interface IPersonService
    {
        GenericResponse<object> SavePerson(PersonDto person);
        GenericResponse<bool> UpdatePerson(PersonDto person);
        GenericResponse<bool> DeletePerson(string idPerson);
        GenericResponse<IEnumerable<PersonDto>> GetPerson(PersonSearchDto person);
    }
}
