using SEP.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace SEP.ClientDatabase
{
    internal class ClientSQL
    {
        private string connectionString;
        private Server server;
        private SqlConnection dbConnection;
        private string databaseName;

        public ClientSQL(string connectionString, string databaseName)
        {
            // connectionString = @"Server=serverName;Database=databaseName;User ID=sa;Password=password;"
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            try
            {
                this.server = new Server(new ServerConnection(new SqlConnection(connectionString)));
            } 
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public async Task<dbResponse> CreateNewDatabase(string databaseName)
        {
            var db = new Database(this.server, databaseName);
            try
            {
                db.Create();
                MessageBox.Show($"DataBase '{databaseName}' is created successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new dbResponse(true, $"DataBase '{databaseName}' created successfully.");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new dbResponse(false, $"Error: {ex.Message}");
            }
        }

        public async Task<dbResponse> CreateNewDatabaseByCommand(string databaseName)
        {
            var conn = this.dbConnection;
            var str = $"CREATE DATABASE '{databaseName}'";
            SqlCommand command = new SqlCommand(str, conn);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show($"DataBase '{databaseName}' is created successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                return new dbResponse(true, $"DataBase '{databaseName}' created successfully.");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                return new dbResponse(false, $"Error: {ex.Message}");
            }
        }
        public async Task<dbResponse> DeleteDatabaseByName(string databaseName)
        {
            var db = new Database(this.server, databaseName);
            try
            {
                db.Drop();
                MessageBox.Show($"DataBase '{databaseName}' is deleted successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new dbResponse(true, $"DataBase '{databaseName}' deleted successfully.");
            }
            catch (System.Exception ex)
            {
                db.Drop();
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new dbResponse(false, $"Error: {ex.Message}");
            }
        }

        public async Task<dbResponse> DeleteDatabaseByNameByCommand(string databaseName)
        {

            var str = $"DROP DATABASE '{databaseName}'";
            var conn = this.dbConnection;
            SqlCommand command = new SqlCommand(str, conn);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show($"DataBase '{databaseName}' is deleted successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                return new dbResponse(true, $"DataBase '{databaseName}' deleted successfully.");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                return new dbResponse(false, $"Error: {ex.Message}");
            }
        }

    }
}
