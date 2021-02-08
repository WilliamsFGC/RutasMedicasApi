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

        private bool ValidateEmails(string[] emails)
        {
            emails = emails ?? new string[0];
            return emails.Length > 1;
        }

        public GenericResponse<string> SavePerson(PersonDto person)
        {
            GenericResponse<string> response = new GenericResponse<string>()
            {
                // Validar los correos electrónicos
                IsSuccessful = ValidateEmails(person.CorreoElectronico)
            };

            if (!response.IsSuccessful)
            {
                response.Message = Messages.InvalidEmailsCount;
                return response;
            }
            // Guardar paciente
            string result = personRepository.SavePerson(person);

            response.Message = Messages.SuccessAddPerson;
            response.Result = result;
            return response;
        }

        public GenericResponse<bool> UpdatePerson(PersonDto person)
        {
            GenericResponse<bool> response = new GenericResponse<bool>()
            {
                // Validar los correos electrónicos
                IsSuccessful = ValidateEmails(person.CorreoElectronico)
            };

            if (!response.IsSuccessful)
            {
                response.Message = Messages.InvalidEmailsCount;
                return response;
            }

            bool result = personRepository.UpdatePerson(person) != null;
            response.IsSuccessful = result;
            response.Message = (result) ? Messages.SuccessUpdatePerson : Messages.NoFoundUpdatePerson;
            response.Result = result;
            return response;
        }
    }
}
