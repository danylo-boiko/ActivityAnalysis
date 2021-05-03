using ActivityAnalysis.Domain.Services;
using ActivityAnalysis.Domain.Services.AuthenticationServices;
using ActivityAnalysis.Domain.Services.EmailService;
using ActivityAnalysis.Domain.Services.ProcessService;
using ActivityAnalysis.MongoDB.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityAnalysis.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IPasswordHasher, PasswordHasher>();
                services.AddSingleton<IAuthenticationService, AuthenticationService>();
                services.AddSingleton<IProcessService, ProcessService>();
                services.AddSingleton<IUserService, UserDataService>();
                services.AddSingleton<IActivityService, ActivityDataService>();
            });

            host.ConfigureServices((context, services) =>
            {
                EmailAssistant emailAssistant = new EmailAssistant(
                    context.Configuration.GetConnectionString("AssistantName"),
                    context.Configuration.GetConnectionString("AssistantEmail"),
                    context.Configuration.GetConnectionString("AssistantPassword")
                );
                
                IUserService userService = services.BuildServiceProvider().GetRequiredService<IUserService>();
                services.AddSingleton<IEmailService>(new EmailService(userService, emailAssistant));
            });

            return host;
        }
    }
}