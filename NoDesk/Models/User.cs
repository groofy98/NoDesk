using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk
{
    public enum UserType{ Employee, Admin };
    public class User
    {
        public ObjectId id;       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType Type { get; set; }
        public string MailAdress { get; set; }
        public int PhoneNumber { get; set; }
        public string Location { get; set; }

        public string PrintOutUser()
        {
            return ("id: " + id.ToString() +
                "\nFirst name: " + this.FirstName
                + "\nLast Name: " + this.LastName +
                "\nType: " + this.Type +
                "\nMail: " + this.MailAdress
                );
        }

    }
}
