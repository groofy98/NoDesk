﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.Dal {
    public class TicketDal : Database {
        public List<IncidentTicket> GetTickets() {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Empty;
            return collection.Find(filter).ToList();
        }
    
        public void UpdateUser(IncidentTicket ticket)
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Eq("_id", ticket.Id);
            collection.ReplaceOne(filter, ticket);
        }

        public void InsertUser(IncidentTicket ticket)
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            collection.InsertOne(ticket);
        }


        public int GetTotalTicketAmount()
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Empty;            
            return (int) collection.Find(filter).CountDocuments();
        }

        public int GetSolvedTicketAmount()
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Eq("Status", true);
            return (int)collection.Find(filter).CountDocuments();
        }

        public int GetOpenTicketAmount()
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Eq("Status", false);
            return (int)collection.Find(filter).CountDocuments();
        }

        public int GetTicketPastDeadline()
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Lte("Deadline", DateTime.Now);
            filter = filter & Builders<IncidentTicket>.Filter.Eq("Status", false);
            return (int)collection.Find(filter).CountDocuments();
        }


    }
}
