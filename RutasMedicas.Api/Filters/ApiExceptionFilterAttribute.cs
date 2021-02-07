using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RutasMedicas.Entities.Api.entities;
using System;
using System.Net;

namespace RutasMedicas.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string message = "";
#if DEBUG
            message = context.Exception.Message;
#else
            message = "Ocurrió un error al procesar la solicitud";
#endif
            GenericResponse<object> response = new GenericResponse<object>()
            {
                IsSuccessful = false
            };
            
            if (context.Exception is Exception)
            {
                response.Message = message;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            ObjectResult objectResult = new ObjectResult(response);
            objectResult.StatusCode = (int)response.StatusCode;
            context.Result = objectResult;
        }
    }
}
