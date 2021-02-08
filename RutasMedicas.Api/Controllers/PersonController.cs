using Microsoft.AspNetCore.Mvc;
using RutasMedicas.Business.Api.interfaces;
using RutasMedicas.Entities.Api.dto;
using RutasMedicas.Entities.Api.entities;
using System.Collections.Generic;

namespace RutasMedicas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpDelete("DeletePerson/{idPerson}")]
        public IActionResult DeletePerson(string idPerson)
        {
            GenericResponse<bool> response = personService.DeletePerson(idPerson);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("GetPerson")]
        public IActionResult GetPerson([FromBody] PersonSearchDto person)
        {
            GenericResponse<IEnumerable<PersonDto>> response = personService.GetPerson(person);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("SavePerson")]
        public IActionResult SavePerson([FromBody]PersonDto person)
        {
            GenericResponse<string> response = personService.SavePerson(person);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("UpdatePerson")]
        public IActionResult UpdatePerson([FromBody] PersonDto person)
        {
            GenericResponse<bool> response = personService.UpdatePerson(person);
            return StatusCode(response.StatusCode, response);
        }
    }
}
