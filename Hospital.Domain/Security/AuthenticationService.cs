using Hospital.Domain.Model;
using Hospital.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Domain.Security
{
    public class AuthenticationService<TUser> : IAuthenticationService<TUser> where TUser : User
    {
        private readonly IGenericRepository<TUser> _dataServices;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IPasswordHasher passwordHasher = null, IGenericRepository<TUser> dataServices = null)
        {
            _passwordHasher = passwordHasher;
            _dataServices = dataServices;
        }

        //TODO: check include parameters for staff-department 
        public async Task<TUser> Authenticate(long phoneNumber, string password)
        {
            var result = await _dataServices.GetWhere(s => s.PhoneNumber == phoneNumber);
            return result.FirstOrDefault(u => u.PasswordHash == _passwordHasher.GetPasswordHash(password));
        }

        public Task<TUser> Register(TUser user, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password must not be null");
            else if (password != confirmPassword) throw new Exception("Passwords not equals");
            else 
            {
                user.PasswordHash = _passwordHasher.GetPasswordHash(password);
                return _dataServices.Create(user);
            }
        }
    }
}
