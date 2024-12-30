using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using SEP.CustomClassBuilder;
using SEP.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using SEP.CustomClassBuilder;

namespace SEP.ClientDatabase
{
    internal class ClientSQL //: IDatabase
    {
        private Server server;
        private SqlConnection dbConnection;
        private Database database;

        public ClientSQL(string connectionString, string databaseName)
        {
            // connectionString = @"Server=serverName;Database=databaseName;User ID=sa;Password=password;"
            try
            {
                this.server = new Server(new ServerConnection(new SqlConnection(connectionString)));
                this.database = new Database(this.server, databaseName);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public Task<dbResponse> AddNewDocument(string collectionName, CustomClass newDocumentObject)
        {
            throw new NotImplementedException();
        }

        public Task<dbResponse> CreateNewCollection(string collectionName)
        {
            throw new NotImplementedException();
                }

        //public async Task<dbResponse> CreateNewDatabase(string databaseName)
        //{
        //    var db = new Database(this.server, databaseName);
        //    try
        //    {
        //        db.Create();
        //        MessageBox.Show($"DataBase '{databaseName}' is created successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return new dbResponse(true, $"DataBase '{databaseName}' created successfully.");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return new dbResponse(false, $"Error: {ex.Message}");
        //    }
        //}

        //public async Task<dbResponse> CreateNewDatabaseByCommand(string databaseName)
        //{
        //    var conn = this.dbConnection;
        //    var str = $"CREATE DATABASE '{databaseName}'";
        //    SqlCommand command = new SqlCommand(str, conn);
        //    try
        //    {
        //        conn.Open();
        //        command.ExecuteNonQuery();
        //        MessageBox.Show($"DataBase '{databaseName}' is created successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        conn.Close();
        //        return new dbResponse(true, $"DataBase '{databaseName}' created successfully.");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        conn.Close();
        //        return new dbResponse(false, $"Error: {ex.Message}");
        //    }
        //}

        //public async Task<dbResponse> DropDatabaseByName(string databaseName)
        //{
        //    var db = new Database(this.server, databaseName);
        //    try
        //    {
        //        db.Drop();
        //        MessageBox.Show($"DataBase '{databaseName}' is deleted successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return new dbResponse(true, $"DataBase '{databaseName}' deleted successfully.");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        db.Drop();
        //        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return new dbResponse(false, $"Error: {ex.Message}");
        //    }
        //}

        //public async Task<dbResponse> DropDatabaseByNameByCommand(string databaseName)
        //{
        //    var str = $"DROP DATABASE '{databaseName}'";
        //    var conn = this.dbConnection;
        //    SqlCommand command = new SqlCommand(str, conn);

        //    try
        //    {
        //        conn.Open();
        //        command.ExecuteNonQuery();
        //        MessageBox.Show($"DataBase '{databaseName}' is deleted successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        conn.Close();
        //        return new dbResponse(true, $"DataBase '{databaseName}' deleted successfully.");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        conn.Close();
        //        return new dbResponse(false, $"Error: {ex.Message}");
        //    }
        //}

        public async Task<TableCollection> GetAllTables()
                    {
            TableCollection tables = this.database.Tables;
            return tables;
        }

        public async Task<Table> GetTable(int tableId)
        {
            return this.database.Tables.ItemById(tableId);
        }

        //public async Task<dbSchema> GetCollectionSchema(string collectionName)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<dbDocument> GetDocumentByID(string collectionName, string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<dbResponse> UpdateDocumentByID(string collectionName, string id, CustomClass newDocumentObject)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
