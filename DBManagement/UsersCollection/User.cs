using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SEP.DBManagement.UsersCollection
{
    [Serializable]
    public class User
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string UserID { get; set; }

        [BsonElement("username"), BsonRepresentation(BsonType.String)]
        public string username { get; set; }

        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string password { get; set; }

        [BsonElement("connectionString"), BsonRepresentation(BsonType.String)]
        public string connectionString { get; set; }
    }
}
