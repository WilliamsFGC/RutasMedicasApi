using Microsoft.AspNetCore.Mvc;
using RutasMedicas.Business.Api.interfaces;
using RutasMedicas.Entities.Api.dto;
using RutasMedicas.Entities.Api.entities;
using System.Collections.Generic;

namespace RutasMedicas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpsController : ControllerBase
    {
        private readonly IEpsService epsService;
        public EpsController(IEpsService epsService)
        {
            this.epsService = epsService;
        }

        [HttpGet("GetEps")]
        public IActionResult GetEps()
        {
            GenericResponse<IEnumerable<EpsDto>> response = epsService.GetEps();
            return StatusCode(response.StatusCode, response);
        }
    }
}
