namespace BearTracApi.Models
{
    public class BearTracDatabaseSettings : IBearTracDatabaseSettings
    {
        public string ApplicationsCollectionName { get; set; }
        public string TicketsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBearTracDatabaseSettings
    {
        string ApplicationsCollectionName { get; set; }
        string TicketsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}