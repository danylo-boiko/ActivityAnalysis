using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.Domain.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ActivityAnalysis.MongoDB.Services
{
    public class ActivityDataService : IActivityService
    {
        private readonly MongoDbContext _mongoDbContext;

        public ActivityDataService(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
        
        public async Task<Activity> Get(ObjectId id)
        {
            return await _mongoDbContext.Activities.Find(u => u.Id == id).FirstAsync();
        }
        
        public async Task<ICollection<Activity>> GetUserActivities(ObjectId userId)
        {
            return await _mongoDbContext.Activities.Find(a=> a.UserId == userId).ToListAsync();
        }

        public async Task<Activity> Create(Activity entity)
        {
            await _mongoDbContext.Activities.InsertOneAsync(entity);
            return entity;
        }

        public async Task<Activity> Update(Activity entity)
        {
            await _mongoDbContext.Activities.ReplaceOneAsync(a => a.Id == entity.Id, entity);
            return entity;
        }

        public async Task<bool> Delete(ObjectId id)
        {
            await _mongoDbContext.Activities.DeleteOneAsync(a => a.Id == id);
            return true;
        }
    }
}