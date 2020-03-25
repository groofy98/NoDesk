﻿using MongoDB.Driver;
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
    }
}