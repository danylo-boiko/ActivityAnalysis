using System;
using ActivityAnalysis.Domain.Services.EmailService;
using ActivityAnalysis.WPF.State.Accounts;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;
using ActivityAnalysis.WPF.ViewModels;
using ActivityAnalysis.WPF.ViewModels.Authentication;
using ActivityAnalysis.WPF.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityAnalysis.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<MainViewModel>();
                services.AddTransient(CreateHeaderViewModel);
                services.AddTransient<TimeAnalysisViewModel>();
                services.AddTransient<ProgramsAnalysisViewModel>();

                services.AddSingleton<CreateViewModel<HeaderViewModel>>(services => () => services.GetRequiredService<HeaderViewModel>());
                services.AddSingleton<CreateViewModel<TimeAnalysisViewModel>>(services => () => services.GetRequiredService<TimeAnalysisViewModel>());
                services.AddSingleton<CreateViewModel<ProgramsAnalysisViewModel>>(services => () => services.GetRequiredService<ProgramsAnalysisViewModel>());
                
                services.AddSingleton<CreateViewModel<SignInViewModel>>(services => () => CreateSignInViewModel(services));
                services.AddSingleton<CreateViewModel<SignUpViewModel>>(services => () => CreateSignUpViewModel(services));
                services.AddSingleton<CreateViewModel<PasswordRecoveryViewModel>>(services => () => CreatePasswordRecoveryViewModel(services));

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
                
                services.AddSingleton<ViewModelDelegateRenavigator<TimeAnalysisViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<ProgramsAnalysisViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<SignInViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<SignUpViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<PasswordRecoveryViewModel>>();
            });

            return host;
        }

        private static HeaderViewModel CreateHeaderViewModel(IServiceProvider services)
        { 
            return new HeaderViewModel(
                services.GetRequiredService<IAccountStore>(),
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<SignInViewModel>>());
        }

        private static SignInViewModel CreateSignInViewModel(IServiceProvider services)
        {
            return new SignInViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<TimeAnalysisViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<PasswordRecoveryViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<SignUpViewModel>>());
        }
        
        private static PasswordRecoveryViewModel CreatePasswordRecoveryViewModel(IServiceProvider services)
        {
            return new PasswordRecoveryViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<IEmailService>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<TimeAnalysisViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<SignInViewModel>>());
        }

        private static SignUpViewModel CreateSignUpViewModel(IServiceProvider services)
        {
            return new SignUpViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<IEmailService>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<TimeAnalysisViewModel>>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<SignInViewModel>>());
        }
    }
}