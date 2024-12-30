using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP.Ultils
{
    internal static class ConnectionHelper
    {
        public static bool Ping(this IMongoDatabase db, int secondToWait = 1)
        {
            if (secondToWait <= 0)
                throw new ArgumentOutOfRangeException("secondToWait", secondToWait, "Must be at least 1 second");

            return db.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}").Wait(secondToWait * 1000);
        }
        public static bool IsValidMongoDBConnection(string connectionString)
        {
            System.Diagnostics.Debug.WriteLine("IsValidMongoDBConnection");
            try
            {
                // Create a MongoClient instance
                var client = new MongoClient(connectionString);

                // Try to access the server list (forces a connection attempt)
                var serverList = client.ListDatabaseNames();

                return true;
            }
            catch (FormatException ex)
            {
                // Connection string format is invalid
                System.Diagnostics.Debug.WriteLine($"Invalid connection string format: {ex.Message}");
            }
            catch (MongoConfigurationException ex)
            {
                // Configuration in the connection string is invalid
                System.Diagnostics.Debug.WriteLine($"Invalid MongoDB configuration: {ex.Message}");
            }
            catch (MongoConnectionException ex)
            {
                // Unable to connect to the server
                System.Diagnostics.Debug.WriteLine($"Failed to connect to MongoDB server: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General exception handling
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
            }
            return false;
        }
        public static bool IsMongoDBConnectionString(string connectionString)
        {
            // Common patterns for MongoDB connection strings
            return connectionString.StartsWith("mongodb://", StringComparison.OrdinalIgnoreCase) ||
                   connectionString.StartsWith("mongodb+srv://", StringComparison.OrdinalIgnoreCase) ||
                   connectionString.Contains("MongoDB", StringComparison.OrdinalIgnoreCase);
        }
        public static bool IsSQLServerConnectionString(string connectionString)
        {
            // Common patterns for SQL Server connection strings
            return connectionString.Contains("Server=", StringComparison.OrdinalIgnoreCase) ||
                   connectionString.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
                   connectionString.Contains("Initial Catalog=", StringComparison.OrdinalIgnoreCase) ||
                   connectionString.Contains("User ID=", StringComparison.OrdinalIgnoreCase) ||
                   connectionString.Contains("Password=", StringComparison.OrdinalIgnoreCase);
        }
    }
}
