using BearTracApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BearTracApi.Services
{
    public class TicketService
    {
        private readonly IMongoCollection<Ticket> _tickets;

        public TicketService(IBearTracDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _tickets = database.GetCollection<Ticket>(settings.TicketsCollectionName);
        }

        public List<Ticket> Get() =>
            _tickets.Find(ticket => true).ToList();

        public Ticket Get(string id) =>
            _tickets.Find<Ticket>(ticket => ticket.Id == id).FirstOrDefault();

        public Ticket Create(Ticket ticket)
        {
            _tickets.InsertOne(ticket);
            return ticket;
        }

        public void Update(string id, Ticket ticketIn) =>
            _tickets.ReplaceOne(ticket => ticket.Id == id, ticketIn);

        public void Remove(Ticket ticketIn) =>
            _tickets.DeleteOne(ticket => ticket.Id == ticketIn.Id);

        public void Remove(string id) => 
            _tickets.DeleteOne(ticket => ticket.Id == id);
    }
}