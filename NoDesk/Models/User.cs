using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoDesk.Dal;

namespace NoDesk
{
    public class User
    {
        public ObjectId id;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public UserType Type { get; set; }
        public string MailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public string Location { get; set; }

        public string PrintOutUser()
        {
            return ("id: " + id.ToString() +
                "\nFirst name: " + this.FirstName
                + "\nLast Name: " + this.LastName +
                "\nType: " + this.Type +
                "\nMail: " + this.MailAddress
                );
        }


        public void AddUser(User user)
        {
            UserDal userDal = new UserDal();
            userDal.InsertUser(user);
        }

        public BsonDocument CreateBson()
        {
            var document = new BsonDocument {
                {"user_id", this.id },
                {"First name", this.FirstName },
                {"Last name", this.LastName },
                {"Username", this.Username },
                {"Password", this.Password },
                {"Type", this.Type },
                {"Mail", this.MailAddress },
                {"Phone number", this.PhoneNumber },
                {"Location", this.Location }
            };

            return document;
        }
    }
}
