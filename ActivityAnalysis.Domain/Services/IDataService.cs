using System.Threading.Tasks;
using MongoDB.Bson;

namespace ActivityAnalysis.Domain.Services
{
    public interface IDataService<T>
    {
        Task<T> Get(ObjectId id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(ObjectId id);
    }
}