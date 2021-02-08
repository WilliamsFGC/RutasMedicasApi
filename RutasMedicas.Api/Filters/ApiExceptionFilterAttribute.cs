using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RutasMedicas.Entities.Api.entities;
using System;
using System.Net;
using RutasMedicas.Utilities.Api.exceptions;
using MongoDB.Driver;

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
            
            if (context.Exception is MongoDbConnectionException)
            {
                response.Message = message;
                response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            else if (context.Exception is MongoWriteException)
            {
                MongoWriteException mdbException = context.Exception as MongoWriteException;
                response.Message = message;
                if (mdbException.WriteError.Category == ServerErrorCategory.DuplicateKey)
                {
                    string messageField = mdbException.WriteError.Message;
                    int indexPesosSign = messageField.IndexOf("$");
                    string campo = messageField.Substring(indexPesosSign, (messageField.Length - indexPesosSign));
                    int indexUnderLine = campo.IndexOf("_");
                    campo = campo.Substring(1, indexUnderLine - 1);
                    response.Message = $"No se puede duplicar el valor para '{campo}'";
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                }
            }
            else if (context.Exception is MongoAuthenticationException)
            {
                response.Message = "No hay acceso al servidor BD solicitado";
                response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            else if (context.Exception is Exception)
            {
                response.Message = message;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            ObjectResult objectResult = new ObjectResult(response);
            objectResult.StatusCode = response.StatusCode;
            context.Result = objectResult;
        }
    }
}
