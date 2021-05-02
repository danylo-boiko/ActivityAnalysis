using System.Threading.Tasks;
using ActivityAnalysis.Domain.Models;

namespace ActivityAnalysis.Domain.Services.ProcessService
{
    public interface IProcessService
    {
        public Task Start(Account account);
        public void Stop();
    }
}