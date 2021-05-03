using System;
using System.Threading.Tasks;
using ActivityAnalysis.WPF.State.Authenticators;
using ActivityAnalysis.WPF.State.Navigators;

namespace ActivityAnalysis.WPF.Commands.Authentication
{
    public class LogoutCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LogoutCommand(IAuthenticator authenticator, IRenavigator signInRenavigator)
        {
            _authenticator = authenticator;
            _renavigator = signInRenavigator;
        }
        
        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _authenticator.Logout();
                _renavigator.Renavigate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}