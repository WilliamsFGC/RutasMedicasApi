using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RutasMedicas.Entities.Api.dto
{
    public class PersonDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int SchemaVersion { get; set; }
        public int DocumentVersion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaUltimaModificacion { get; set; }
        public string CodigoInterno { get; set; }
        public string NumeroDocumento { get; set; }
        public DocumentTypeDto TipoDocumento { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string EstadoCivil { get; set; }
        public SexDto Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public PersonIdentity Identidad { get; set; }
        public string[] CorreoElectronico { get; set; }
        public EpsPersonDto[] Eps { get; set; }
    }

    public class PersonIdentity
    {
        public DateTime FechaExpedicion { get; set; }
        public string LugarExpedicion { get; set; }
    }

    public class PersonSearchDto
    {
        public string NumeroDocumento { get; set; }
    }

    public class SexDto
    {
        public int _id { get; set; }
        public string Descripcion { get; set; }
        public string Otro { get; set; }
    }
}
