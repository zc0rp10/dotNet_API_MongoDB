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
        public string Description { get; set; }
        public Type Type { get; set; }
        public String Application { get; set; } //TODO: Change return type to Application
        public Status Status { get; set; }
    }

    public enum Type {  
        Bug,
        Request
    }

    public enum Status {
        Open,
        InProgress,
        Completed,
    }

}