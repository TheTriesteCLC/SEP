﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP
{
    public static class Constants
    {
        public const string connectionString = "mongodb+srv://vmtriet21:X5djNq4hX4KKN3yR@sep.lpk2b.mongodb.net/";
        public const string mainDBString = "SEP";
        public const string usersDBName = "Users";

        public const string testSQLConenctionString = $"data source=TrishVoltman;initial catalog={mainDBString};user id=sa;password=svcntt";
        public static readonly List<Type> supportedType = new List<Type>
        {
            typeof(double),      // BsonType.Double
            typeof(string),      // BsonType.String
            typeof(bool),        // BsonType.Boolean
            typeof(DateTime),    // BsonType.DateTime
            typeof(int),         // BsonType.Int32
            typeof(long),        // BsonType.Int64
            typeof(decimal),     // BsonType.Decimal128
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
    }
}
