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
    
    public class IncidentTicket
    {
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; }
        public IncidentType Type { get; set; }
        public string By { get; set; }
        public int Priority { get; set; }
        public int Deadline { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public void SubmitTicket()
        {
            CreateBson();
            
        }

        public BsonDocument CreateBson()
        {
            var document = new BsonDocument {
                {"incident_id", this.Id },
                {"date", this.Date },
                {"type", this.Type },
                {"Reported by", this.By },
                {"Priority", this.Priority },
                {"Deadline", this.Deadline },
                {"Subject", this.Subject },  
                {"Description", this.Description }
            };

            return document;
        }
    }


}