using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.Dal {
    class TicketDal : Database {
        public List<IncidentTicket> GetTickets() {
            var collection = database.GetCollection<IncidentTicket>("users");
            var filter = Builders<IncidentTicket>.Filter.Empty;
            return collection.Find(filter).ToList();
        }
    }
}
