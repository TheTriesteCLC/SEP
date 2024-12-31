using SEP.CustomClassBuilder;
using SEP.Interfaces;
using SEP.Ultils;
using Microsoft.Data.SqlClient;

namespace SEP.ClientDatabase
{
    internal class ClientSQL : IDatabase
    {
        private string connectionString;
        private string databaseName;
        private SqlConnection connection;
        public ClientSQL(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;

            // Modify connection string to use the specified database
            var builder = new SqlConnectionStringBuilder(this.connectionString)
            {
                InitialCatalog = this.databaseName
            };

            string fullConnectionString = builder.ToString();
            try
            {
                this.connection = new SqlConnection(fullConnectionString);
                this.connection.Open();
                Console.WriteLine($"Successfully connected to database: {this.databaseName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to database {this.databaseName}: {ex.Message}");
                throw;
            }
        }
        public Task<dbResponse> AddNewDocument(string collectionName, CustomClass newDocumentObject)
        {
            throw new NotImplementedException();
        }

        public async Task<dbResponse> CreateNewCollection(string collectionName, dbSchema schema = null)
        {
            try
            {
                if (schema == null || schema.fields == null || schema.fields.Count == 0)
                {
                    return new dbResponse(false, "Schema must have at least one field.");
                }
                var columnDefinitions = schema.fields.Select(field =>
                {
                    string sqlType = SQLHelper.ConvertTypeToSql(field.type);
                    return $"{field.name} {sqlType}";
                });
                // Construct the CREATE TABLE query
                string query = $"CREATE TABLE [{collectionName}] (" +
                    "_id INT NOT NULL IDENTITY," +
                    $"{string.Join(", ", columnDefinitions)}," +
                    "PRIMARY KEY (_id))";

                System.Diagnostics.Debug.WriteLine(query);
                using (SqlCommand command = new SqlCommand(query, this.connection))
                {
                    await command.ExecuteNonQueryAsync();
                }

                return new dbResponse(false, $"Table '{collectionName}' created successfully.");
            }
            catch (Exception ex)
            {
                return new dbResponse(false, $"Error: {ex.Message}");
            }
        }

        public Task<dbResponse> DeleteDocumentByID(string collectionName, string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<dbCollection>> GetAllCollections()
        {
            List<dbCollection> tables = new List<dbCollection>();
            string query = Constants.sqlQuery["getAllTablesName"];
            using (var command = new SqlCommand(query, this.connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        tables.Add(new dbCollection(reader.GetString(0))); // Retrieve table name
                    }
                }
            }
            return tables;
        }

        public Task<dbCollection> GetCollection(string collectionName)
        {
            throw new NotImplementedException();
        }

        public Task<dbSchema> GetCollectionSchema(string collectionName)
        {
            throw new NotImplementedException();
        }

        public Task<dbDocument> GetDocumentByID(string collectionName, string id)
        {
            throw new NotImplementedException();
        }

        public Task<dbResponse> UpdateDocumentByID(string collectionName, string id, CustomClass newDocumentObject)
        {
            throw new NotImplementedException();
        }

        Task<List<dbDocument>> IDatabase.GetCollection(string collectionName)
        {
            throw new NotImplementedException();
        }
    }
}
