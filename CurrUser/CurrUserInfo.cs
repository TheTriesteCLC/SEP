using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using SEP.ClientDatabase;
using SEP.DBManagement;
using SEP.DBManagement.UsersCollection;
using SEP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            _database = new ClientMongoDB(user.connectionString, user.databaseName);
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
