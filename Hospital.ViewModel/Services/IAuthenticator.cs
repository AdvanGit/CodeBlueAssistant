using Hospital.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Hospital.ViewModel.Services
{
    public interface IAuthenticator
    {
        Staff CurrentUser { get; }

        bool IsLoggedIn { get; }

        DateTime LoggedTime { get; }

        Task<Role> Login(long phoneNumber, string password);
        Task<bool> Register();
    }
}
