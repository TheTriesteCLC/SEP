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
using System.Data.Common;
using System.Data;

namespace SEP.ClientDatabase
{
    internal class ClientSQLServer: IDatabase
    {
        private string connectionString;
        private string databaseName;
        private SqlConnection connection;

        public ClientSQLServer(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            var builder = new SqlConnectionStringBuilder(this.connectionString)
            {
                InitialCatalog = this.databaseName
            };

            string fullConnectionString = builder.ToString();
            try
            {
                this.connection = new SqlConnection(fullConnectionString);
                this.connection.Open();
                System.Diagnostics.Debug.WriteLine("Connected to SQL Server successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to connect to SQL Server: {ex.Message}");
            }


        }

        public async Task<dbResponse> AddNewDocument(string collectionName, CustomClass newDocumentObject)
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
                    return $"[{field.name}] {sqlType}";
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

        public async Task<dbResponse> DeleteDocumentByID(string collectionName, string id)
        {
            try
            {
                string query = $"DELETE FROM [{collectionName}] WHERE _id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return new dbResponse(true, $"Row deleted successfully.");
                    }
                    else
                    {
                        return new dbResponse(false, $"No row found with ID {id}.");
                    }
                }
            }
            catch (Exception ex)
            {
                return new dbResponse(false, $"An error occurred: {ex.Message}");
            }
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

        public async Task<List<dbDocument>> GetCollection(string tableName)
        {
            var dbDocuments = new List<dbDocument>();

            string query = $"SELECT * FROM [{tableName}]"; // Ensure table name is delimited

            try
            {
                using (var command = new SqlCommand(query, this.connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var fields = new List<dbDocumentField>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var fieldName = reader.GetName(i);
                            var fieldValue = reader.IsDBNull(i) ? null : reader.GetValue(i).ToString();
                            var fieldType = reader.GetFieldType(i);

                            fields.Add(new dbDocumentField(fieldName, fieldValue, fieldType));
                        }

                        dbDocuments.Add(new dbDocument(fields));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving table data: {ex.Message}");
            }

            return dbDocuments;
        }

        public async Task<dbSchema> GetCollectionSchema(string collectionName)
        {
            List<dbSchemaField> fields = new List<dbSchemaField>();

            string query = @"
            SELECT COLUMN_NAME, DATA_TYPE
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @CollectionName";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CollectionName", collectionName);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var columnName = reader.GetString(0);
                        var dataType = reader.GetString(1);

                        // Convert SQL data type string to Type (this is simplified, you might want a more complete mapping)
                        Type fieldType = SQLHelper.ConvertSqlToType(dataType);
                        fields.Add(new dbSchemaField(columnName, fieldType));
                    }
                }
            }
            return new dbSchema(collectionName, fields);
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
                                var columnType = reader.GetFieldType(i);

                                System.Diagnostics.Debug.WriteLine(columnName);
                                System.Diagnostics.Debug.WriteLine(columnValue);
                                System.Diagnostics.Debug.WriteLine(columnType);

                                fields.Add(new dbDocumentField(columnName, columnValue, columnType));
                            }

                            dbDocument = new dbDocument(fields);
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

        public async Task<dbResponse> UpdateDocumentByID(string tableName, string id, CustomClass updateDocumentObject)
        {
            try
            {
                // Get the primary key column
                string primaryKeyColumn = await GetPrimaryKeyColumn(tableName);

                // Build the dynamic UPDATE query
                var updateQueryBuilder = new StringBuilder($"UPDATE {tableName} SET ");

                // List of parameters for the update query
                var parameters = new List<SqlParameter>();

                foreach (var property in updateDocumentObject.properties)
                {
                    var propertyName = property.PropertyName;
                    var propertyValue = updateDocumentObject.getProp(propertyName);

                    // Append column and value to the query
                    updateQueryBuilder.Append($"{propertyName} = @{propertyName}, ");

                    // Add parameter to the list
                    parameters.Add(new SqlParameter($"@{propertyName}", propertyValue ?? DBNull.Value));
                }

                // Remove the trailing comma after the last column-value pair
                updateQueryBuilder.Length--;
                updateQueryBuilder.Length--;

                // Append WHERE condition with the primary key
                updateQueryBuilder.Append($" WHERE {primaryKeyColumn} = @Id");

                // Add the primary key parameter
                parameters.Add(new SqlParameter("@Id", id));

                // Final SQL query
                string updateQuery = updateQueryBuilder.ToString();
                System.Diagnostics.Debug.WriteLine("Update query:");
                System.Diagnostics.Debug.WriteLine(updateQuery);
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(updateQuery, connection))
                    {
                        // Add all parameters to the command
                        command.Parameters.AddRange(parameters.ToArray());

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new dbResponse(true, "Document updated successfully.");
                        }
                        else
                        {
                            return new dbResponse(false, "No rows were updated. The document might not exist.");
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                return new dbResponse(false, $"Error: {ex.Message}");
            }
        }
    }

}
