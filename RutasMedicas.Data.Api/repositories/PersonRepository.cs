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
        public object SavePerson(PersonDto person)
        {
            person.FechaCreacion = DateTime.Now;
            person.SchemaVersion = 1;
            person.DocumentVersion = 1;
            personCollection.InsertOne(person);
            return person._id;
        }
        public object UpdatePerson(PersonDto person)
        {
            FilterDefinition<PersonDto> filter = Builders<PersonDto>.Filter.Eq(e => e._id, person._id);
            UpdateDefinition<PersonDto> update = Builders<PersonDto>.Update
                .Inc(i => i.DocumentVersion, 1)
                .Set(f => f.CodigoInterno, person.CodigoInterno)
                .Set(f => f.PrimerNombre, person.PrimerNombre)
                .Set(f => f.SegundoNombre, person.SegundoNombre)
                .Set(f => f.PrimerApellido, person.PrimerApellido)
                .Set(f => f.SegundoApellido, person.SegundoApellido)
                .Set(f => f.CorreoElectronico, person.CorreoElectronico)
                .Set(f => f.EstadoCivil, person.EstadoCivil)
                .Set(f => f.FechaUltimaModificacion, DateTime.Now);
            person = personCollection.FindOneAndUpdate(filter, update);
            return person._id;
        }
    }
}