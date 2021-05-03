using ActivityAnalysis.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityAnalysis.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {  
                string connectionString = context.Configuration.GetConnectionString("MongoConnection");
                string mongoDatabase = context.Configuration.GetConnectionString("MongoDatabase");
                
                services.AddSingleton(new MongoDbContext(connectionString, mongoDatabase));
            });

            return host;
        }
    }
}