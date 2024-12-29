using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SEP.CustomClassBuilder;
using SEP.Interfaces;
using SEP.Ultils;
using System.Data.SqlClient;
using MongoDB.Driver.Core.Configuration;

namespace SEP.ClientDatabase
{
    internal class ClientSqlServer: IDatabase
    {
        private string connectionString;
        private string databaseName;
        private SqlConnection connection;
        private IMongoDatabase database;
        public ClientSqlServer(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            this.connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                System.Diagnostics.Debug.WriteLine("Connected to SQL Server successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to connect to SQL Server: {ex.Message}");
            }
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
            try
            {
                List<dbCollection> collections = new List<dbCollection>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT name FROM sys.tables";
                    SqlCommand command = new SqlCommand(query, connection);

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        collections.Add(new dbCollection(reader.GetString(0)));
                    }
                }
                return collections;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<List<dbDocument>> GetCollection(string tableName)
        {
            List<dbDocument> dbDocuments = new List<dbDocument>();

            // Câu truy vấn SQL để lấy tất cả các dòng từ bảng
            string query = $"SELECT * FROM {tableName}";

            // Mở kết nối tới SQL Server trong một khối `using` để đảm bảo tài nguyên được giải phóng
            try
            {
                // Sử dụng 'using' để tự động đóng kết nối sau khi xong
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            List<dbDocumentField> fields = new List<dbDocumentField>();

                            // Duyệt qua tất cả các cột trong bảng và lấy giá trị
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var columnName = reader.GetName(i);
                                var columnValue = reader.GetValue(i).ToString();
                                var columnType = reader.GetFieldType(i).ToString();

                                fields.Add(new dbDocumentField(columnName, columnValue, Type.GetType(columnType)));
                            }

                            dbDocuments.Add(new dbDocument(fields));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

            return dbDocuments;
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
                    if (element.Name != "_id")
                    {
                        var newField = new dbSchemaField(element.Name, BsonHelper.BsonTypeToSystemType(element.Value.BsonType));
                        if (!schemaFields.Any(x => x.name == newField.name))
                        {
                            schemaFields.Add(newField);
                        }
                    }
                }
            }
            return new dbSchema(collectionName, schemaFields);
        }

        public async Task<string> GetPrimaryKeyColumn(string tableName)
        {
            string primaryKeyColumn = null;

            string query = @"
        SELECT COLUMN_NAME 
        FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
        WHERE TABLE_NAME = @TableName
        AND OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + CONSTRAINT_NAME), 'IsPrimaryKey') = 1";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", tableName);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                primaryKeyColumn = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving primary key column: {ex.Message}");
            }

            if (string.IsNullOrEmpty(primaryKeyColumn))
            {
                throw new Exception($"No primary key column found for table {tableName}");
            }

            return primaryKeyColumn;
        }


        public async Task<dbDocument> GetDocumentByID(string tableName, string id)
        {
            dbDocument dbDocument = null;

            try
            {
                // Lấy tên cột khóa chính
                string primaryKeyColumn = await GetPrimaryKeyColumn(tableName);

                string query = $"SELECT * FROM {tableName} WHERE {primaryKeyColumn} = @Id";

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                List<dbDocumentField> fields = new List<dbDocumentField>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var columnName = reader.GetName(i);
                                    var columnValue = reader.GetValue(i)?.ToString();
                                    var columnType = reader.GetFieldType(i).ToString();

                                    fields.Add(new dbDocumentField(columnName, columnValue, Type.GetType(columnType)));
                                }

                                dbDocument = new dbDocument(fields);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

            return dbDocument ?? throw new Exception("Document not found.");
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
