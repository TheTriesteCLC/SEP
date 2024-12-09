using MongoDB.Driver;
using SEP.Ultils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEP.DBManagement.UsersCollection
{
    public class UsersCollection
    {
        private static UsersCollection _instance;
        private static object syncLock = new object();

        public IMongoCollection<User> _userCollection { get; set; }

        protected UsersCollection()
        {
            var database = Database.GetDatabase()._database;
            _userCollection = database.GetCollection<User>(Constants.usersDBName);
        }

        public static UsersCollection GetUsersCollection()
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
                        _instance = new UsersCollection();
                    }
                }
            }

            return _instance;
        }

        public Boolean addNewUser(User newUser)
        {
            var filterUser = Builders<User>.Filter
                .Eq(user => user.username, newUser.username);
            var foundUser = _userCollection.Find<User>(filterUser).FirstOrDefault();
            if (foundUser == null)
            {
                var hashedUser = new User
                {
                    username = newUser.username,
                    password = SecretHasher.Hash(newUser.password),
                    connectionString = newUser.connectionString
                };
                _userCollection.InsertOne(hashedUser);
                return true;
            }else
            {
                return false;
            }
        }

        public User loginToUser(User loginUser)
        {
            var filterUser = Builders<User>.Filter
                .Eq(user => user.username, loginUser.username);

            var foundUser = _userCollection.Find<User>(filterUser).FirstOrDefault();
            if(SecretHasher.Verify(loginUser.password, foundUser.password))
            {
                return foundUser;
            }else
            {
                return null;
            }
        }

    }
}
