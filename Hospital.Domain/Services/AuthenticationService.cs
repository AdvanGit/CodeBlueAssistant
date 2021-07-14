using Hospital.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Hospital.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataServices<Staff> _dataServices;

        public AuthenticationService(IDataServices<Staff> dataServices)
        {
            _dataServices = dataServices;
        }

        //TODO - check test for include parameters
        public async Task<Staff> Login(long phoneNumber, string password)
        {
            return await _dataServices.GetItemWithInclude(s => s.PhoneNumber == phoneNumber, s => s.Department);
        }

        public Task<bool> Register(long phoneNumber, string password, string confirmPassword)
        {
            throw new NotImplementedException();
        }
    }
}
