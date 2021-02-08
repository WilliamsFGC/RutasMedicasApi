using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RutasMedicas.Entities.Api.dto
{
    public class EpsDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Codigo { get; set; }
        public string Entidad { get; set; }
        public string Nit { get; set; }
        public string RegimenCodigo { get; set; }
        public string RegimenDescripcion { get; set; }
    }

    public class EpsPersonDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdEntidad { get; set; }
        public string Entidad { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool EstadoAfiliacion { get; set; }
    }
}