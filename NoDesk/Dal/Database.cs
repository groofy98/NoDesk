using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.Dal
{
    public class Database
    {
        public MongoClient dbClient;
        public IMongoDatabase database;
        
        public Database()
        {
            dbClient = new MongoClient("mongodb+srv://Sjors:Yolo1@cluster0-dg3ym.mongodb.net/test?retryWrites=true&w=majority");
            database = dbClient.GetDatabase("nodesk");
        }
    }
}
