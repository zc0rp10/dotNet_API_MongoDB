using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace BearTracApi.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string TicketName { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public string Description { get; set; }
        public Type Type { get; set; }
        public Application Application { get; set; }
    }

    public enum Type {  
        Bug,
        Request
    }

}