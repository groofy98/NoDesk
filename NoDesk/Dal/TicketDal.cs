using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.Dal {
    class TicketDal : Database {
        public List<IncidentTicket> GetTickets() {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Empty;
            return collection.Find(filter).ToList();
        }
    }

    public class TicketDal : Database
    {
        public void UpdateUser(IncidentTicket ticket)
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            var filter = Builders<IncidentTicket>.Filter.Eq("_id", ticket.incidentId);
            collection.ReplaceOne(filter, ticket);
        }

        public void InsertUser(IncidentTicket ticket)
        {
            var collection = database.GetCollection<IncidentTicket>("tickets");
            collection.InsertOne(ticket);
        }
    }
}
