using MongoDB.Driver;
using SEP.Interfaces;
using Newtonsoft.Json;
using System;
using MongoDB.Bson;
using SEP.Ultils;
using SEP.CustomClassBuilder;

namespace SEP.ClientDatabase
{
    internal class ClientMongoDB : IDatabase
    {
        private string conenctionString;
        private string databaseName;
        private IMongoDatabase database;
        ClientMongoDB(string conenctionString)
        {
            this.conenctionString = conenctionString;
            MongoClient dbClient = new MongoClient(this.conenctionString);
            this.database = dbClient.GetDatabase(databaseName);
        }

        public async Task<dbResponse> AddNewDocument(string collectionName, CustomClass newDocumentObject)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            try
            {
                await collection.InsertOneAsync(newDocumentObject.ToBsonDocument());
                return new dbResponse(true, "Document added successfully.");
            }
            catch (Exception ex)
            {
                return new dbResponse(false, $"Error: {ex.Message}");
            }
        }

        public async Task<dbResponse> CreateNewCollection(string collectionName)
        {
            var collectionList = await database.ListCollectionNames().ToListAsync();
            if (collectionList.Contains(collectionName))
            {
                return new dbResponse(false, "Collection already exists.");
            }
            await database.CreateCollectionAsync(collectionName);
            return new dbResponse(false, $"Collection '{collectionName}' created successfully.");
        }

        public async Task<dbResponse> DeleteDocumentByID(string collectionName, string id)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var result = await collection.DeleteOneAsync(filter);
            if (result.DeletedCount > 0)
            {
                return new dbResponse(true, "Document deleted successfully.");
            }
            else
            {
                return new dbResponse(false, "Document not found.");
            }
        }

        public async Task<List<dbCollection>> GetAllCollections()
        {
            var collectionNames = await database.ListCollectionNames().ToListAsync();
            List<dbCollection> dbCollections = new List<dbCollection>();
            foreach (var collection in collectionNames)
            {
                dbCollections.Add(new dbCollection(collection));
            }
            return dbCollections;
        }

        public async Task<dbCollection> GetCollection(string collectionName)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var documents = await collection.Find(new BsonDocument()).ToListAsync();
            return new dbCollection(collectionName);
        }

        public async Task<dbSchema> GetCollectionSchema(string collectionName)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var documents = await collection.Find(new BsonDocument()).Limit(100).ToListAsync();
            var schemaFields = new List<dbSchemaField>();
            foreach (var doc in documents)
            {
                foreach (var element in doc.Elements)
                {
                    var newField = new dbSchemaField(element.Name, BsonHelper.BsonTypeToSystemType(element.Value.BsonType));
                    if (!schemaFields.Any(x => x.name == newField.name))
                    {
                        schemaFields.Add(newField);
                    }
                }
            }
            return new dbSchema(collectionName, schemaFields);
        }

        public async Task<dbDocument> GetDocumentByID(string collectionName, string id)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var document = await collection.Find(filter).FirstOrDefaultAsync();

            List<dbDocumentField> fields = new List<dbDocumentField>();
            foreach(var field in document)
            {
                foreach (var element in document.Elements)
                {
                    fields.Add(new dbDocumentField(element.Name, 
                        element.Value.ToString(), 
                        BsonHelper.BsonTypeToSystemType(element.Value.BsonType)));
                }
            }
            return new dbDocument(fields);
        }

        public async Task<dbResponse> UpdateDocumentByID(string collectionName, string id, CustomClass updateDocumentObject)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            try
            {
                await collection.ReplaceOneAsync(
                Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id)),
                updateDocumentObject.ToBsonDocument());
                return new dbResponse(true, "Document updated successfully.");
            }
            catch (Exception ex)
            {
                return new dbResponse(false, $"Error: {ex.Message}");
            }
        }
    }
}
