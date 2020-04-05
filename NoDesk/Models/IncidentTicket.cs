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
    
    public class IncidentTicket
    {
        public ObjectId Id;
        public DateTime Date { get; set; }
        public IncidentType Type { get; set; }
        public string By { get; set; }
        public string Priority { get; set; }
        public DateTime Deadline { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Solution { get; set; }

        public void SubmitTicket(IncidentTicket incidentTicket)
        {
            incidentTicket.Status = false;

            TicketDal ticketDal = new TicketDal();
            ticketDal.InsertTicket(incidentTicket);
        }

        public string PrintOutTickets() {
            return ("id: " + Id.ToString());
        }

    }


}