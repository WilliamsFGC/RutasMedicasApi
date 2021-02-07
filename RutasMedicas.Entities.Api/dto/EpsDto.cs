using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

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

    public class JsonObjectIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ObjectId));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var objectIds = new List<ObjectId>();

            if (token.Type == JTokenType.Array)
            {
                foreach (var item in token.ToObject<string[]>())
                {
                    objectIds.Add(new ObjectId(item));
                }
            }

            if (token.ToObject<string[]>().Equals("Mongo.DB.ObjectId[]"))
            {
                return objectIds.ToArray();
            }
            else
                return new ObjectId(token.ToObject<string>());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType().IsArray)
            {
                writer.WriteStartArray();
                foreach (var item in (Array)value)
                {
                    serializer.Serialize(writer, item);
                }
                writer.WriteEndArray();
            }
            else
                serializer.Serialize(writer, value.ToString());
        }
    }
}
