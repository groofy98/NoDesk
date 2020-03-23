using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDesk.model
{
    public enum IncidentType{Software, Hardware, Service}
    public class IncidentTicket
    {
        public ObjectId incidentId { get; set; }
        public BsonDateTime incidentDate { get; set; }
        public IncidentType incidentType { get; set; }
        public string reportedBy { get; set; }
        public int priotity { get; set; }
        public int incidentDeadline { get; set; }

        public BsonDocument CreateBson()
        {
            var document = new BsonDocument {
                {"incident_id", this.incidentId },
                {"date", this.incidentDate },
                {"type", this.incidentType },
                {"Reported by", this.reportedBy },
                {"Priority", this.priotity },
                {"Deadline", this.incidentDeadline }
            };

            return document;
        }
    }

    
}
