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
        //public User(string firstName, string lastName, UserType type, string mailAdress, int phoneNumber, string location)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Type = type;
        //    MailAdress = mailAdress;
        //    PhoneNumber = phoneNumber;
        //    Location = location;
        //    this.PrintOutUser();
        //}
        public ObjectId Id;       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType Type { get; set; }
        public string MailAdress { get; set; }
        public int PhoneNumber { get; set; }
        public string Location { get; set; }

        public string PrintOutUser()
        {
            return ("First name: " + this.FirstName
                + "\nLast Name: " + this.LastName +
                "\nType: " + this.Type +
                "\nMail: " + this.MailAdress
                );
        }

    }
}
