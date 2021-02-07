using RutasMedicas.Business.Api.interfaces;
using RutasMedicas.Business.Api.resources;
using RutasMedicas.Data.Api.interfaces;
using RutasMedicas.Entities.Api.dto;
using RutasMedicas.Entities.Api.entities;
using System.Collections.Generic;

namespace RutasMedicas.Business.Api.services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        public GenericResponse<bool> DeletePerson(string idPerson)
        {
            bool result = personRepository.DeletePerson(idPerson) > 0;
            GenericResponse<bool> response = new GenericResponse<bool>()
            {
                IsSuccessful = result,
                Message = (result) ? Messages.SuccessDeletePerson : Messages.NoFoundDeletePerson,
                Result = result
            };
            return response;
        }

        public GenericResponse<IEnumerable<PersonDto>> GetPerson(PersonSearchDto person)
        {
            IEnumerable<PersonDto> result = personRepository.GetPerson(person);
            GenericResponse<IEnumerable<PersonDto>> response = new GenericResponse<IEnumerable<PersonDto>>()
            {
                Result = result
            };
            return response;
        }

        public GenericResponse<object> SavePerson(PersonDto person)
        {
            object result = personRepository.SavePerson(person);
            GenericResponse<object> response = new GenericResponse<object>()
            {
                Message = Messages.SuccessAddPerson,
                Result = result
            };
            return response;
        }

        public GenericResponse<bool> UpdatePerson(PersonDto person)
        {
            bool result = personRepository.UpdatePerson(person) != null;
            GenericResponse<bool> response = new GenericResponse<bool>()
            {
                IsSuccessful = result,
                Message = (result) ? Messages.SuccessUpdatePerson : Messages.NoFoundUpdatePerson,
                Result = result
            };
            return response;
        }
    }
}
