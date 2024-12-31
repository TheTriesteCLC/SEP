using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP
{
    public static class Constants
    {
        public static readonly string connectionString = "mongodb+srv://vmtriet21:X5djNq4hX4KKN3yR@sep.lpk2b.mongodb.net/";
        public static readonly string mainDBString = "SEP";
        public static readonly string usersDBName = "Users";

        //public static readonly string testSQLConnectionString = $"data source=TrishVoltman;initial catalog=master;user id=sa;password=svcntt";
        public static readonly string testSQLConnectionString = $"data source=localhost;user id=sa;password=svcntt;TrustServerCertificate=True;trusted_connection=true;";
        public static readonly string testSQLDB = $"master";
        public static readonly int stringMaxLength = 1000;
        public static readonly List<Type> supportedType = new List<Type>
        {
            typeof(double),      // BsonType.Double - REAL
            typeof(string),      // BsonType.String - NVARCHAR(stringMaxLength)
            typeof(bool),        // BsonType.Boolean - BIT
            typeof(DateTime),    // BsonType.DateTime - DATETIME
            typeof(int),         // BsonType.Int32 - INT
            typeof(long),        // BsonType.Int64 - BIGINT
            typeof(decimal),     // BsonType.Decimal128 - DECIMAL(18, 2)
        };
        public static readonly List<Type> unsupportedType = new List<Type>
        {
            typeof(BsonDocument),// BsonType.Document
            typeof(BsonArray),   // BsonType.Array
            typeof(byte[]),      // BsonType.Binary
            typeof(ObjectId),    // BsonType.ObjectId
            typeof(DBNull),      // BsonType.Null
            typeof(object),      // BsonType.MinKey
            typeof(object),      // BsonType.MaxKey
            typeof(object)       // BsonType.Undefined
        };
        public static readonly List<string> booleanTypeValidation = new List<string>
        {
            "true",
            "false",
            "True",
            "False",
            "TRUE",
            "FALSE"
        };
        public static readonly Dictionary<string, string> sqlQuery = new Dictionary<string, string>
        {
            { "getDatabaseNames", "SELECT DISTINCT name FROM sys.databases" },
            { "getAllTablesName", "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'" }
        };
    }
}
