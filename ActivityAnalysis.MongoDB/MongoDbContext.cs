using System;
using ActivityAnalysis.Domain.Models;
using MongoDB.Driver;

namespace ActivityAnalysis.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Activity> Activities => _database.GetCollection<Activity>("activities");

        public MongoDbContext(string connectionString, string mongoDatabase)
        {
            try
            {
                IMongoClient client = new MongoClient(connectionString);
                _database = client.GetDatabase(mongoDatabase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}