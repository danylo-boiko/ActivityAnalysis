using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;
using ActivityAnalysis.Domain.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ActivityAnalysis.MongoDB.Services
{
    public class UserDataService : IUserService
    {
        private readonly MongoDbContext _mongoDbContext;

        public UserDataService(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
        
        public async Task<User> Get(ObjectId id)
        {
            return await _mongoDbContext.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByLoginOrEmail(string loginOrEmail)
        {
            return await _mongoDbContext.Users.Find(u => u.Login == loginOrEmail || u.Email == loginOrEmail).FirstOrDefaultAsync();
        }

        public async Task<bool> IsLoginUsed(string login)
        { 
            return await _mongoDbContext.Users.Find(u => u.Login == login).FirstOrDefaultAsync() != null;
        }

        public async Task<bool> IsEmailUsed(string email)
        {
            return await _mongoDbContext.Users.Find(u => u.Email == email).FirstOrDefaultAsync() != null;
        }

        public async Task<User> Create(User entity)
        {
            await _mongoDbContext.Users.InsertOneAsync(entity);
            return entity;
        }

        public async Task<User> Update(User entity)
        {
            await _mongoDbContext.Users.ReplaceOneAsync(u => u.Id == entity.Id, entity);
            return entity;
        }

        public async Task<bool> Delete(ObjectId id)
        {
             await _mongoDbContext.Users.DeleteOneAsync(a => a.Id == id);
             return true;
        }
    }
}