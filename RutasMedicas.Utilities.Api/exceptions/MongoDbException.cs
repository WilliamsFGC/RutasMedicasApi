using System;

namespace RutasMedicas.Utilities.Api.exceptions
{
    public class MongoDbException : Exception
    {
        public MongoDbException(string message)
            :base(message)
        {

        }
    }

    public class MongoDbConnectionException : Exception
    {
        public MongoDbConnectionException(string message)
            : base(message)
        {

        }
    }
}
