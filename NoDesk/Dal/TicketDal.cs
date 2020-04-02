using MongoDB.Driver;
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
    
        public void UpdateTicket(IncidentTicket ticket)
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Eq("_id", ticket.Id);
            collection.ReplaceOne(filter, ticket);
        }

        public void InsertTicket(IncidentTicket ticket)
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            collection.InsertOne(ticket);
        }

        public void DeleteTicket(IncidentTicket ticket) {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Eq("_id", ticket.Id);
            collection.DeleteOne(filter);
        }
    }
}
