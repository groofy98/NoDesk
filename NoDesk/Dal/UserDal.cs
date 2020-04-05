using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.Dal
{
    public class UserDal : Database
    {
        public List<User> GetUsers()
        {
            var collection = database.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Empty;
            return collection.Find(filter).ToList();
        }

        public List<User> GetUserByUsername(string username) {
            try {
                var collection = database.GetCollection<User>("users");
                var filter = Builders<User>.Filter.Eq("Username", username); //filter to only get users with certain username
                return collection.Find(filter).ToList();
            } catch {
                return new List<User>();
            }
            
        }

        public List<User> GetUserbyEmail(string email)
        {
            try
            {
                var collection = database.GetCollection<User>("users");
                var filter = Builders<User>.Filter.Eq("MailAddress", email); //filter to only get users with a certain email
                return collection.Find(filter).ToList();
            }
            catch
            {
                return new List<User>();
            }

        }

        public void UpdateLastLogin(User user)
        {
            var oldLastLogin = user.LastLogin;
            user.LastLogin = DateTime.Now;
            UpdateUser(user);
            user.LastLogin = oldLastLogin;
        }

        public void UpdateUser(User user)
        {
            var collection = database.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Eq("_id", user.id);
            collection.ReplaceOne(filter, user);
        }

        public void InsertUser(User user)
        {
            var collection = database.GetCollection<User>("users");
            collection.InsertOne(user);
        }

        public void DeleteUser(User user) {
            var collection = database.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Eq("_id", user.id);
            collection.DeleteOne(filter);
        }
    }
}
