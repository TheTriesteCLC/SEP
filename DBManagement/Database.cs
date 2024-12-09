using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP.DBManagement
{
    internal class Database
    {
        private static Database _instance;
        private static object syncLock = new object();

        public IMongoDatabase _database { get; set; }

        protected Database()
        {
            MongoClient dbClient = new MongoClient(Constants.connectionString);
            _database = dbClient.GetDatabase(Constants.mainDBString);
        }

        public static Database GetDatabase()
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
                        _instance = new Database();
                    }
                }
            }

            return _instance;
        }

    }
}
