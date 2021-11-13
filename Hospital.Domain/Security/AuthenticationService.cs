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

        public AuthenticationService(IPasswordHasher passwordHasher, IGenericRepository<TUser> dataServices)
        {
            _passwordHasher = passwordHasher;
            _dataServices = dataServices;
        }

        //TODO: check include parameters for staff-department 
        //TODO: new Exception type: DbEntryNotFindException
        public async Task<TUser> Authenticate(long phoneNumber, string password)
        {
            var result = (await _dataServices.GetWhere(s => s.PhoneNumber == phoneNumber &&
                s.PasswordHash == _passwordHasher.GetPasswordHash(phoneNumber, password))).FirstOrDefault();

            return result;
        }

        public async Task<TUser> ChangePassword(long phoneNumber, string oldPassword, string newPassword)
        {
            string passwordHash = _passwordHasher.GetPasswordHash(phoneNumber, oldPassword);
            var user = (await _dataServices.GetWhere(s => s.PhoneNumber == phoneNumber && s.PasswordHash == passwordHash)).FirstOrDefault();

            if (user != null)
            {
                user.PasswordHash = _passwordHasher.GetPasswordHash(phoneNumber, newPassword);
                return await _dataServices.Update(user.Id, user);
            }
            else throw new Exception("номер или пароль неверны");
        }

        public Task<TUser> Register(TUser user, string password)
        {
            user.PasswordHash = _passwordHasher.GetPasswordHash(user.PhoneNumber, password);
            return _dataServices.Create(user);
        }
    }
}
