using Hospital.Domain.Model;
using Hospital.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Services
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public Staff CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;
        public DateTime LoggedTime { get; private set; }

        public async Task<Role> Login(long phonenumber, string password)
        {
            var user = await _authenticationService.Login(phonenumber, password);

            //TODO - create hasher, implements password check, put passwords to db
            if (user != null && user.PasswordHash == null)
            {
                CurrentUser = user;
                LoggedTime = DateTime.Now;
                return user.Role;
            }
            else
            {
                throw new UnauthorizedAccessException("номер или пароль неверны");
            }
        }

        public Task<bool> Register()
        {
            throw new NotImplementedException();
        }
    }
}
