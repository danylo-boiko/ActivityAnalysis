using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;
using MongoDB.Bson;

namespace ActivityAnalysis.Domain.Services
{
    public interface IActivityService : IDataService<Activity>
    { 
        Task<ICollection<Activity>> GetUserActivities(ObjectId userId);
    }
}