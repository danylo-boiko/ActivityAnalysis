using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityAnalysis.Domain.Exceptions;
using ActivityAnalysis.Domain.Models;
using Microsoft.AspNet.Identity;

namespace ActivityAnalysis.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IActivityService _activityService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IUserService userService, IActivityService activityService, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _activityService = activityService;
            _passwordHasher = passwordHasher;
        }
        
        public async Task<Account> Login(string loginOrEmail, string password)
        {
            User storedUser = await _userService.GetByLoginOrEmail(loginOrEmail);
            
            if(storedUser == null)
            {
                throw new UserNotFoundException("No users with such login/email");
            }

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(storedUser.Password, password);

            if(passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException("This password is not valid for this account");
            }
            
            return new Account
            {
                AccountHolder = storedUser,
                Activities = await _activityService.GetUserActivities(storedUser.Id),
            }; 
        }

        public async Task<Account> Register(string email, string login, string password, string confirmPassword, string code, string generatedCode)
        {
            bool isLoginUsed = await _userService.IsLoginUsed(login);
            
            if (isLoginUsed)
            {
                throw new InvalidLoginException("An account with this login is already registered");
            }
            
            bool isEmailUsed = await _userService.IsEmailUsed(email);
            
            if(isEmailUsed)
            {
                throw new InvalidEmailException("An account with this mail is already registered");
            }
            
            if(password != confirmPassword)
            {
                throw new InvalidPasswordException("Passwords do not match.");
            }
            
            if (code != generatedCode)
            {
                throw new InvalidVerificationCodeException("The entered code does not match the code sent to the mail");
            }

            User newUser = new User(email, login, _passwordHasher.HashPassword(password));

            await _userService.Create(newUser);
            
            return new Account
            {
                AccountHolder = newUser,
                Activities = new List<Activity>(),
            };
        }

        public async Task<Account> PasswordRecovery(string loginOrEmail, string password, string confirmPassword, string code, string generatedCode)
        {
            User storedUser = await _userService.GetByLoginOrEmail(loginOrEmail);
            
            if(storedUser == null)
            {
                throw new UserNotFoundException("No users with such login/email");
            }
            
            if(password != confirmPassword)
            {
                throw new InvalidPasswordException("Passwords do not match.");
            }
            
            if (code != generatedCode)
            {
                throw new InvalidVerificationCodeException("The entered code does not match the code sent to the mail");
            }
            
            storedUser.Password = _passwordHasher.HashPassword(password);
            await _userService.Update(storedUser);
   
            return new Account
            {
                AccountHolder = storedUser,
                Activities = await _activityService.GetUserActivities(storedUser.Id),
            }; 
        }
    }
}