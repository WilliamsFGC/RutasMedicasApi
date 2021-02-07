using System.Net;

namespace RutasMedicas.Entities.Api.entities
{
    public class GenericResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Result { get; set; }
        public GenericResponse()
        {
            this.IsSuccessful = true;
            this.StatusCode = (int)HttpStatusCode.OK;
            this.Message = "";
        }
    }
}
