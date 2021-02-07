using RutasMedicas.Entities.Api.dto;
using System.Collections.Generic;

namespace RutasMedicas.Data.Api.interfaces
{
    public interface IPersonRepository
    {
        object SavePerson(PersonDto person);
        object UpdatePerson(PersonDto person);
        long DeletePerson(string idPerson);
        IEnumerable<PersonDto> GetPerson(PersonSearchDto person);
    }
}
