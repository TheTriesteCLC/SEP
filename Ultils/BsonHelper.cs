using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP.Ultils
{
    internal static class BsonHelper
    {
        public static Type BsonTypeToSystemType(BsonType bsonType)
        {
            return bsonType switch
            {
                BsonType.Double => typeof(double),
                BsonType.String => typeof(string),
                BsonType.Document => typeof(BsonDocument),
                BsonType.Array => typeof(BsonArray),
                BsonType.Binary => typeof(byte[]),
                BsonType.ObjectId => typeof(ObjectId),
                BsonType.Boolean => typeof(bool),
                BsonType.DateTime => typeof(DateTime),
                BsonType.Null => typeof(DBNull),
                BsonType.Int32 => typeof(int),
                BsonType.Int64 => typeof(long),
                BsonType.Decimal128 => typeof(decimal),
                BsonType.MinKey => typeof(object), // MinKey has no direct .NET equivalent
                BsonType.MaxKey => typeof(object), // MaxKey has no direct .NET equivalent
                BsonType.Undefined => typeof(object), // Undefined is rarely used
                _ => typeof(object), // Default fallback
            };
        }
    }
}
