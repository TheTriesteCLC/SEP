using SEP.Interfaces;
using System;
using SEP.Ultils;
using SEP.CustomClassBuilder;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;

namespace SEP.ClientDatabase
{
    internal class ClientSQL
    {
        private string connectionString;
        private SqlConnection dbConnection;
        public ClientSQL(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.dbConnection = new SqlConnection(connectionString);
        }

        public async Task<dbResponse> CreateNewDatabase(string databaseName)
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
