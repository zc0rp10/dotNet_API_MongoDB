using BearTracApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BearTracApi.Services
{
    public class ApplicationService
    {
        private readonly IMongoCollection<Application> _applications;

        public ApplicationService(IBearTracDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _applications = database.GetCollection<Application>(settings.ApplicationsCollectionName);
        }

        public List<Application> Get() =>
            _applications.Find(application => true).ToList();

        public Application Get(string id) =>
            _applications.Find<Application>(application => application.Id == id).FirstOrDefault();

        public Application Create(Application application)
        {
            _applications.InsertOne(application);
            return application;
        }

        public void Update(string id, Application applicationIn) =>
            _applications.ReplaceOne(application => application.Id == id, applicationIn);

        public void Remove(Application applicationIn) =>
            _applications.DeleteOne(application => application.Id == applicationIn.Id);

        public void Remove(string id) => 
            _applications.DeleteOne(application => application.Id == id);
    }
}