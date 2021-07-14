using Hospital.Domain.Model;
using System.Threading.Tasks;

namespace Hospital.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Register(long phoneNumber, string password, string confirmPassword);
        Task<Staff> Login(long phoneNumber, string password);
    }
}
