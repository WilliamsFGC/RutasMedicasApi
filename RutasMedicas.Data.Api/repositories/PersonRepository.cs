using MongoDB.Driver;
using RutasMedicas.Data.Api.interfaces;
using RutasMedicas.Entities.Api.dto;
using System;
using System.Collections.Generic;

namespace RutasMedicas.Data.Api.repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IMongoCollection<PersonDto> personCollection;
        public PersonRepository(IConnection connection)
        {
            personCollection = connection.GetCollection<PersonDto>("person");
        }
        public long DeletePerson(string idPerson)
        {
            DeleteResult result = personCollection.DeleteOne(d => d._id == idPerson);
            return result.DeletedCount;
        }
        public IEnumerable<PersonDto> GetPerson(PersonSearchDto person)
        {            
            IFindFluent<PersonDto, PersonDto> find = personCollection.Find(f => true);
            IEnumerable<PersonDto> result = find.ToList();
            return result;
        }
        public string SavePerson(PersonDto person)
        {
            person.FechaCreacion = DateTime.Now;
            person.SchemaVersion = 1;
            person.DocumentVersion = 1;
            person.CodigoInterno = Guid.NewGuid().ToString();
            personCollection.InsertOne(person);
            return person._id;
        }
        public object UpdatePerson(PersonDto person)
        {
            FilterDefinition<PersonDto> filter = Builders<PersonDto>.Filter.Eq(e => e._id, person._id);
            // Actualizar campos de la colección
            UpdateDefinition<PersonDto> update = Builders<PersonDto>.Update
                .Inc(i => i.DocumentVersion, 1) // Incrementar el campo de 1 en 1
                .Set(s => s.CodigoInterno, person.CodigoInterno)
                .Set(s => s.PrimerNombre, person.PrimerNombre)
                .Set(s => s.SegundoNombre, person.SegundoNombre)
                .Set(s => s.PrimerApellido, person.PrimerApellido)
                .Set(s => s.SegundoApellido, person.SegundoApellido)
                .Set(s => s.CorreoElectronico, person.CorreoElectronico)
                .Set(s => s.EstadoCivil, person.EstadoCivil)
                .Set(s => s.FechaUltimaModificacion, DateTime.Now.AddHours(-5))
                .Set(s => s.CorreoElectronico, person.CorreoElectronico)
                .Set(s => s.Eps, person.Eps);
            person = personCollection.FindOneAndUpdate(filter, update);
            return person._id;
        }
    }
}