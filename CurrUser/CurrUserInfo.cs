using SEP.ClientDatabase;
using SEP.DBManagement.UsersCollection;
using SEP.Interfaces;
using SEP.Ultils;

namespace SEP.CurrUser
{
    internal class CurrUserInfo
    {
        private static CurrUserInfo _instance;
        private static object syncLock = new object();
        private IDatabase _database { get; set; }
        private User currUser { get; set; }

        protected CurrUserInfo(User user)
        {
            currUser = user;
            _database = HandleConnectoDatabase(user.connectionString, user.databaseName);
            //_database = HandleConnectoDatabase(Constants.testSQLConnectionString, Constants.testSQLDB); // TEST
        }
        private static IDatabase HandleConnectoDatabase(string connectionString, string databaseName)
        {
            if (connectionString == null
                || databaseName == null)
            {
                throw new ArgumentNullException("Connection string cannot be null !!!");
            }
            if (ConnectionHelper.IsValidMongoDBConnection(connectionString))
            {
                return new ClientMongoDB(connectionString, databaseName);
            } else if (ConnectionHelper.IsSQLServerConnectionString(connectionString))
            {
                return new ClientSQLServer(connectionString, databaseName);
            } else
            {
                throw new InvalidOperationException("Cannot connect to database !!!");
            }
        }
        public static User getCurrUser()
        {
            return _instance.currUser;
        }
        public static IDatabase getUserDB()
        {
            return _instance._database;
        }
        public static CurrUserInfo GetCurrUserInfo()
        {
            return _instance;
        }

        public static void login(User newUser)
        {
            // Support multithreaded applications through

            // 'Double checked locking' pattern which (once

            // the instance exists) avoids locking each

            // time the method is invoked
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new CurrUserInfo(newUser);
                    }
                }
            }
        }

        public static void logout()
        {
            _instance = null;
        }
    }
}
